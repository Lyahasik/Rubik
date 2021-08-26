using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellProcessing : MonoBehaviour
{
    public FacetRotations Rubik;
    public GameObject Cube;
    
    private GameObject _faceSelection;
    private Vector3 _direction;
    
    private Material _material;

    void Start()
    {
        _material = Cube.GetComponent<Renderer>().material;
    }

    public void SwitchSelection(GameObject face)
    {
        if (_faceSelection == face)
        {
            _faceSelection = null;
            Rubik.OffSelections();
        }
        else
        {
            _material.SetColor("_Color", new Color(0.647778f, 0.0f, 1.0f, 1.0f));
            Rubik.OffSelections();
            _faceSelection = face;
            _direction = _faceSelection.transform.forward;
            Rubik.SetCurrentCell(gameObject);
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

    public Vector3 GetDirection()
    {
        return _direction;
    }
}
