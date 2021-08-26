using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceProcessing : MonoBehaviour
{
    public CellProcessing Cell;

    private void OnMouseDown()
    {
        Cell.SwitchSelection(gameObject);
    }
}
