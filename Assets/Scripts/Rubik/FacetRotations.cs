using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacetRotations : MonoBehaviour
{
    public GameObject[] Cells;

    private GameObject _currentCell;
    private List<GameObject> _selectedCells;

    private Vector3 _direction;

    private void Start()
    {
        _selectedCells = new List<GameObject>();
    }

    void Update()
    {
        
    }

    public void SetCurrentCell(GameObject cell)
    {
        if (_currentCell != null && _currentCell != cell)
        {
            OffSelections();
        }
        
        _currentCell = cell;
        IdentifyFacet();
    }

    public void OffSelections()
    {
        foreach (GameObject selectedCell in _selectedCells)
        {
            selectedCell.GetComponent<CellProcessing>().OffSelection();
        }
        _selectedCells.Clear();
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
