using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FacetRotations : MonoBehaviour
{
    public GameObject[] Cells;
    public float SpeedRotating = 20.0f;

    private Solution _solution;
    
    private bool _selected = false;
    private GameObject _currentCell;
    private Vector3 _currentCellPosition;
    private List<GameObject> _selectedCells;

    private Vector3 _direction;
    private Vector3 _axisRotate;
    private bool _rotationProcess = false;
    private int _rezusRotate = 1;
    private List<GameObject> _listTurn;
    private float _currentAngle;

    private void Start()
    {
        _solution = GetComponent<Solution>();
        _selectedCells = new List<GameObject>();
        _listTurn = new List<GameObject>();
    }

    void Update()
    {
        InputEvent();
        RotationProcess();
    }

    public bool IsRotationProcess()
    {
        return _rotationProcess;
    }

    void InputEvent()
    {
        if (_selected
            && !_solution.IsSolution()
            && !_rotationProcess)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                DefineRotate(Vector3.up, 1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                DefineRotate(Vector3.up, -1);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                DefineRotate(Vector3.right, -1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                DefineRotate(Vector3.right, 1);
            }
        }
    }

    #region ProcessingFacet
    
    void DefineRotate(Vector3 axisRotate, int rezusRotate)
    {
        if (_direction == Vector3.forward)
        {
            FacingForward(axisRotate, rezusRotate, _currentCell.transform.position);
        }
        else if (_direction == -Vector3.forward)
        {
            FacingBack(axisRotate, rezusRotate, _currentCell.transform.position);
        }
        else if (_direction == Vector3.right)
        {
            FacingRight(axisRotate, rezusRotate, _currentCell.transform.position);
        }
        else if (_direction == -Vector3.right)
        {
            FacingLeft(axisRotate, rezusRotate, _currentCell.transform.position);
        }
        else if (_direction == Vector3.up)
        {
            FacingUp(axisRotate, rezusRotate, _currentCell.transform.position);
        }
        else if (_direction == -Vector3.up)
        {
            FacingDown(axisRotate, rezusRotate, _currentCell.transform.position);
        }
    }

    public void FacingForward(Vector3 axisRotate, int rezusRotate, Vector3 cellPosition)
    {
        _currentAngle = 0.0f;
        _axisRotate = axisRotate;
        _rezusRotate = rezusRotate;

        if (_axisRotate == Vector3.right)
        {
            RotateRL(cellPosition);
        }
        else
        {
            RotateUD(cellPosition);
        }

        CreateListTurn(cellPosition);
        _rotationProcess = true;
    }

    void FacingBack(Vector3 axisRotate, int rezusRotate, Vector3 cellPosition)
    {
        _currentAngle = 0.0f;
        _axisRotate = axisRotate;
        _rezusRotate = (axisRotate == Vector3.right) ? -rezusRotate : rezusRotate;

        if (_axisRotate == Vector3.right)
        {
            _rezusRotate = -rezusRotate;
            RotateRL(cellPosition);
        }
        else
        {
            _rezusRotate = rezusRotate;
            RotateUD(cellPosition);
        }

        CreateListTurn(cellPosition);
        _rotationProcess = true;
    }

    public void FacingRight(Vector3 axisRotate, int rezusRotate, Vector3 cellPosition)
    {
        _currentAngle = 0.0f;
        
        if (axisRotate == Vector3.right)
        {
            _axisRotate = Vector3.forward;
            _rezusRotate = -rezusRotate;
            RotateFB(cellPosition);
        }
        else
        {
            _axisRotate = axisRotate;
            _rezusRotate = rezusRotate;
            RotateUD(cellPosition);
        }

        CreateListTurn(cellPosition);
        _rotationProcess = true;
    }

    void FacingLeft(Vector3 axisRotate, int rezusRotate, Vector3 cellPosition)
    {
        _currentAngle = 0.0f;
        _rezusRotate = rezusRotate;

        if (axisRotate == Vector3.right)
        {
            _axisRotate = Vector3.forward;
            RotateFB(cellPosition);
        }
        else
        {
            _axisRotate = axisRotate;
            RotateUD(cellPosition);
        }

        CreateListTurn(cellPosition);
        _rotationProcess = true;
    }

    void FacingUp(Vector3 axisRotate, int rezusRotate, Vector3 cellPosition)
    {
        _currentAngle = 0.0f;
        if (axisRotate == Vector3.right)
        {
            _axisRotate = axisRotate;
            _rezusRotate = rezusRotate;
            RotateRL(cellPosition);
        }
        else
        {
            _axisRotate = Vector3.forward;
            _rezusRotate = -rezusRotate;
            RotateFB(cellPosition);
        }

        CreateListTurn(cellPosition);
        _rotationProcess = true;
    }

    void FacingDown(Vector3 axisRotate, int rezusRotate, Vector3 cellPosition)
    {
        _currentAngle = 0.0f;
        _rezusRotate = rezusRotate;
        
        if (axisRotate == Vector3.right)
        {
            _axisRotate = axisRotate;
            RotateRL(cellPosition);
        }
        else
        {
            _axisRotate = Vector3.forward;
            RotateFB(cellPosition);
        }

        CreateListTurn(cellPosition);
        _rotationProcess = true;
    }
    
    #endregion ProcessingFacet
    
    #region DefineFacet

    void RotateFB(Vector3 cellPosition)
    {
        if (cellPosition.z == 1.0f)
        {
            RememberF(_rezusRotate);
        }
        else if (cellPosition.z == -1.0f)
        {
            RememberB(_rezusRotate);
        }
        else
        {
            RememberF(-_rezusRotate);
            RememberB(-_rezusRotate);
        }
    }

    void RotateRL(Vector3 cellPosition)
    {
        if (cellPosition.x == 1.0f)
        {
            RememberR(_rezusRotate);
        }
        else if (cellPosition.x == -1.0f)
        {
            RememberL(_rezusRotate);
        }
        else
        {
            RememberR(-_rezusRotate);
            RememberL(-_rezusRotate);
        }
    }

    void RotateUD(Vector3 cellPosition)
    {
        if (cellPosition.y == 1.0f)
        {
            RememberU(_rezusRotate);
        }
        else if (cellPosition.y == -1.0f)
        {
            RememberD(_rezusRotate);
        }
        else
        {
            RememberU(-_rezusRotate);
            RememberD(-_rezusRotate);
        }
    }
    
    #endregion DefineFacet
    
    #region RememberFacet

    void RememberF(int rezusRotate)
    {
        if (!_solution.IsSolution())
        {
            _solution.AddStep((rezusRotate == -1) ? "F" : "F'");
        }
    }
    
    void RememberB(int rezusRotate)
    {
        if (!_solution.IsSolution())
        {
            _solution.AddStep((rezusRotate == 1) ? "B" : "B'");
        }
    }

    void RememberR(int rezusRotate)
    {
        if (!_solution.IsSolution())
        {
            _solution.AddStep((rezusRotate == -1) ? "R" : "R'");
        }
    }
    
    void RememberL(int rezusRotate)
    {
        if (!_solution.IsSolution())
        {
            _solution.AddStep((rezusRotate == 1) ? "L" : "L'");
        }
    }

    void RememberU(int rezusRotate)
    {
        if (!_solution.IsSolution())
        {
            _solution.AddStep((rezusRotate == 1) ? "U" : "U'");
        }
    }
    
    void RememberD(int rezusRotate)
    {
        if (!_solution.IsSolution())
        {
            _solution.AddStep((rezusRotate == -1) ? "D" : "D'");
        }
    }
    
    #endregion RememberFacet

    #region Rotation
    
    void RotationProcess()
    {
        if (_rotationProcess)
        {
            float step = SpeedRotating * Time.deltaTime;
                
            foreach (GameObject turnCell in _listTurn)
            {
                if (_currentAngle + step > 90.0f)
                {
                    turnCell.transform.RotateAround(Vector3.zero, _axisRotate, (90.0f - _currentAngle) * _rezusRotate);
                    turnCell.transform.position = new Vector3(Mathf.Round(turnCell.transform.position.x),
                                                                Mathf.Round(turnCell.transform.position.y),
                                                                Mathf.Round(turnCell.transform.position.z));
                }
                else
                {
                    turnCell.transform.RotateAround(Vector3.zero, _axisRotate, step * _rezusRotate);
                }
            }
            
            if (_currentAngle + step > 90.0f)
            {
                _listTurn.Clear();
                _rotationProcess = false;
                
                if (!_solution.IsSolution())
                {
                    ReSelected();
                }
            }
            
            _currentAngle += step;
        }
        else if (_solution.IsSolution())
        {
            _solution.NextStep();
        }
    }

    void CreateListTurn(Vector3 cellPosition)
    {
        foreach (GameObject cell in Cells)
        {
            if (_axisRotate == Vector3.up || _axisRotate == -Vector3.up)
            {
                if (cellPosition.y == cell.transform.position.y)
                {
                    _listTurn.Add(cell);
                }
            }
            else if (_axisRotate == Vector3.right || _axisRotate == -Vector3.right)
            {
                if (cellPosition.x == cell.transform.position.x)
                {
                    _listTurn.Add(cell);
                }
            }
            else if (_axisRotate == Vector3.forward || _axisRotate == -Vector3.forward)
            {
                if (cellPosition.z == cell.transform.position.z)
                {
                    _listTurn.Add(cell);
                }
            }
        }
    }
    
    #endregion Rotation

    #region Selection
    
    void ReSelected()
    {
        SetCurrentCell(NewCurrentCell(), _direction);
    }

    GameObject NewCurrentCell()
    {
        GameObject newCell = _currentCell;
        
        for (int i = 0; i < Cells.Length; i++)
        {
            if (Cells[i].transform.position == _currentCellPosition)
            {
                newCell = Cells[i];
            }
        }

        return newCell;
    }

    public void SetCurrentCell(GameObject cell, Vector3 direction)
    {
        if (_currentCell != null && _currentCell != cell)
        {
            OffSelections();
        }
        
        _currentCell = cell;
        _currentCellPosition = _currentCell.transform.position;
        _direction = direction;
        IdentifyFacet();
        _selected = true;
    }

    public void OffSelections()
    {
        foreach (GameObject selectedCell in _selectedCells)
        {
            selectedCell.GetComponent<CellProcessing>().OffSelection();
        }
        _selectedCells.Clear();
        _selected = false;
    }

    void IdentifyFacet()
    {
        if (_direction == gameObject.transform.forward
            || _direction == -gameObject.transform.forward)
        {
            for (int i = 0; i < Cells.Length; i++)
            {
                if (Cells[i].transform.position.x == _currentCell.transform.position.x
                    || Cells[i].transform.position.y == _currentCell.transform.position.y)
                {
                    Cells[i].GetComponent<CellProcessing>().OnSelection();
                    _selectedCells.Add(Cells[i]);
                }
            }
        }
        else if (_direction == gameObject.transform.right
                 || _direction == -gameObject.transform.right)
        {
            for (int i = 0; i < Cells.Length; i++)
            {
                if (Cells[i].transform.position.z == _currentCell.transform.position.z
                    || Cells[i].transform.position.y == _currentCell.transform.position.y)
                {
                    Cells[i].GetComponent<CellProcessing>().OnSelection();
                    _selectedCells.Add(Cells[i]);
                }
            }
        }
        else if (_direction == gameObject.transform.up
                 || _direction == -gameObject.transform.up)
        {
            for (int i = 0; i < Cells.Length; i++)
            {
                if (Cells[i].transform.position.z == _currentCell.transform.position.z
                    || Cells[i].transform.position.x == _currentCell.transform.position.x)
                {
                    Cells[i].GetComponent<CellProcessing>().OnSelection();
                    _selectedCells.Add(Cells[i]);
                }
            }
        }
    }
    
    #endregion Selection
}
