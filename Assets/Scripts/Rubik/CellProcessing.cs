using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellProcessing : MonoBehaviour
{
    public GameObject Rubik;
    
    public GameObject Cube;
    
    private FacetRotations _rubikRotations;
    
    private GameObject _faceSelection;
    
    private Material _material;

    void Start()
    {
        _material = Cube.GetComponent<Renderer>().material;
        _rubikRotations = Rubik.GetComponent<FacetRotations>();
    }

    public void SwitchSelection(GameObject face)
    {
        if (!_rubikRotations.IsRotationProcess())
        {
            if (_faceSelection == face)
            {
                _faceSelection = null;
                _rubikRotations.OffSelections();
            }
            else
            {
                _material.SetColor("_Color", new Color(0.647778f, 0.0f, 1.0f, 1.0f));
                _rubikRotations.OffSelections();
                _faceSelection = face;
                _rubikRotations.SetCurrentCell(gameObject, _faceSelection.transform.forward);
            }
        }
    }

    public void OnSelection()
    {
        _material.SetColor("_Color", new Color(0.647778f, 0.0f, 1.0f, 1.0f));
    }

    public void OffSelection()
    {
        _faceSelection = null;
        _material.SetColor("_Color", Color.black);
    }
}
