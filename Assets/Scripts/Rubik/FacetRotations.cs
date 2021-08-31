using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FacetRotations : MonoBehaviour
{
    public GameObject[] Cells;
    public float SpeedRotating = 20.0f;

    private bool _selected = false;
    private GameObject _currentCell;
    private Vector3 _currentCellPosition;
    private List<GameObject> _selectedCells;

    private Vector3 _direction;
    private Vector3 _axisRotate;
    private bool _rotationProcess = false;
    private int _rezusRotation = 1;
    private List<GameObject> _listTurn;
    private float _currentAngle;

    private void Start()
    {
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
        if (_selected && !_rotationProcess)
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

    void DefineRotate(Vector3 axisRotate, int rezusRotate)
    {
        _currentAngle = 0.0f;
        _rezusRotation = rezusRotate;

        if (_direction == Vector3.forward)
        {
            _axisRotate = axisRotate;
        }
        else if (_direction == -Vector3.forward)
        {
            if (axisRotate == Vector3.right)
            {
                _rezusRotation = -_rezusRotation;
            }
            
            _axisRotate = axisRotate;
        }
        else if (_direction == Vector3.right)
        {
            if (axisRotate == Vector3.right)
            {
                _axisRotate = Vector3.forward;
                _rezusRotation = -_rezusRotation;
            }
            else
            {
                _axisRotate = axisRotate;
            }
        }
        else if (_direction == -Vector3.right)
        {
            if (axisRotate == Vector3.right)
            {
                _axisRotate = Vector3.forward;
            }
            else
            {
                _axisRotate = axisRotate;
            }
        }
        else if (_direction == Vector3.up)
        {
            if (axisRotate == Vector3.right)
            {
                _axisRotate = axisRotate;
            }
            else
            {
                _axisRotate = Vector3.forward;
                _rezusRotation = -_rezusRotation;
            }
        }
        else if (_direction == -Vector3.up)
        {
            if (axisRotate == Vector3.right)
            {
                _axisRotate = axisRotate;
            }
            else
            {
                _axisRotate = Vector3.forward;
            }
        }

        CreateListTurn();
        
        _rotationProcess = true;
    }

    void RotationProcess()
    {
        if (_rotationProcess)
        {
            float step = SpeedRotating * Time.deltaTime;
                
            foreach (GameObject turnCell in _listTurn)
            {
                if (_currentAngle + step > 90.0f)
                {
                    turnCell.transform.RotateAround(Vector3.zero, _axisRotate, (90.0f - _currentAngle) * _rezusRotation);
                    turnCell.transform.position = new Vector3(Mathf.Round(turnCell.transform.position.x),
                                                                Mathf.Round(turnCell.transform.position.y),
                                                                Mathf.Round(turnCell.transform.position.z));
                }
                else
                {
                    turnCell.transform.RotateAround(Vector3.zero, _axisRotate, step * _rezusRotation);
                }
            }
            
            if (_currentAngle + step > 90.0f)
            {
                ReSelected();
                _listTurn.Clear();
                _rotationProcess = false;
            }
            
            _currentAngle += step;
        }
    }

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

    void CreateListTurn()
    {
        foreach (GameObject selectedCell in _selectedCells)
        {
            if (_axisRotate == Vector3.up || _axisRotate == -Vector3.up)
            {
                if (_currentCell.transform.position.y == selectedCell.transform.position.y)
                {
                    _listTurn.Add(selectedCell);
                }
            }
            else if (_axisRotate == Vector3.right || _axisRotate == -Vector3.right)
            {
                if (_currentCell.transform.position.x == selectedCell.transform.position.x)
                {
                    _listTurn.Add(selectedCell);
                }
            }
            else if (_axisRotate == Vector3.forward || _axisRotate == -Vector3.forward)
            {
                if (_currentCell.transform.position.z == selectedCell.transform.position.z)
                {
                    _listTurn.Add(selectedCell);
                }
            }
        }
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
}
