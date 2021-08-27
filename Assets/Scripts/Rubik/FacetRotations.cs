using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FacetRotations : MonoBehaviour
{
    public GameObject[] Cells;

    private bool _selected = false;
    private GameObject _currentCell;
    private List<GameObject> _selectedCells;

    private Vector3 _direction;
    private Vector3 _axisRotate;
    private Quaternion _startRotate;
    private Quaternion _targetRotate;
    private bool _rotationProcess = false;
    private List<GameObject> _listTurn;
    private float test = 0.0f;

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

    void InputEvent()
    {
        if (_selected)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _axisRotate = Vector3.up;
                DefineRotation(90.0f);
                CreateListTurn();
                _rotationProcess = true;
            }
        }
    }

    void RotationProcess()
    {
        // _startRotate = Quaternion.Slerp(_startRotate, _targetRotate, 10.0f * Time.deltaTime);

        if (_rotationProcess)
        {
            foreach (GameObject turnCell in _listTurn)
            {
                float step = Mathf.Lerp(test, 90.0f, Time.deltaTime * 1.0f);
                test += step;
                turnCell.transform.RotateAround(Vector3.zero, _axisRotate, Time.deltaTime * 20.0f);
            
                float _dotRotation = Mathf.Abs(Quaternion.Dot(turnCell.transform.rotation, _targetRotate));

                Debug.Log(test);
            
                if (_dotRotation == 1.0f)
                {
                    _rotationProcess = false;
                }
            }
        }
    }

    void DefineRotation(float value)
    {
        if (_direction == gameObject.transform.up
            && _direction == -gameObject.transform.up)
        {
            
        }
        else
        {
            _startRotate = Quaternion.identity;
            _targetRotate = Quaternion.AngleAxis(value, _axisRotate);
        }
    }

    void CreateListTurn()
    {
        foreach (GameObject selectedCell in _selectedCells)
        {
            if (_axisRotate == Vector3.up)
            {
                if (_currentCell.transform.position.y == selectedCell.transform.position.y)
                {
                    _listTurn.Add(selectedCell);
                }
            }
        }
    }

    public void SetCurrentCell(GameObject cell)
    {
        if (_currentCell != null && _currentCell != cell)
        {
            OffSelections();
        }
        
        _currentCell = cell;
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
        _direction = _currentCell.GetComponent<CellProcessing>().GetDirection();
        
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
