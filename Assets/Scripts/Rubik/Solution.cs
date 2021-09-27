using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solution : MonoBehaviour
{
    public Queue<string> QueueSteps;
    
    private FacetRotations _rubikRotations;

    private bool _solution = false;

    private void Start()
    {
        QueueSteps = new Queue<string>();
        _rubikRotations = GetComponent<FacetRotations>();
    }

    public void StartSolution()
    {
        if (!_solution)
        {
            _solution = true;

            NextStep();
        }
    }

    public bool IsSolution()
    {
        return _solution;
    }

    public void NextStep()
    {
        if (QueueSteps.Count > 0)
        {
            ChangeFacetRotate(QueueSteps.Dequeue());
        }
        else
        {
            _solution = false;
        }
    }

    public void AddStep(string nameStep)
    {
        QueueSteps.Enqueue(nameStep);
    }
    
    void ChangeFacetRotate(string nameRotate)
    {
        if (nameRotate == "F")
        {
            _rubikRotations.FacingRight(Vector3.right, 1, new Vector3(1, 0, 1));
        }
        else if (nameRotate == "F'")
        {
            _rubikRotations.FacingRight(Vector3.right, -1, new Vector3(1, 0, 1));
        }
        else if (nameRotate == "B")
        {
            _rubikRotations.FacingRight(Vector3.right, -1, new Vector3(1, 0, -1));
        }
        else if (nameRotate == "B'")
        {
            _rubikRotations.FacingRight(Vector3.right, 1, new Vector3(1, 0, -1));
        }
        else if (nameRotate == "R")
        {
            _rubikRotations.FacingForward(Vector3.right, -1, new Vector3(1, 0, 1));
        }
        else if (nameRotate == "R'")
        {
            _rubikRotations.FacingForward(Vector3.right, 1, new Vector3(1, 0, 1));
        }
        else if (nameRotate == "L")
        {
            _rubikRotations.FacingForward(Vector3.right, 1, new Vector3(-1, 0, 1));
        }
        else if (nameRotate == "L'")
        {
            _rubikRotations.FacingForward(Vector3.right, -1, new Vector3(-1, 0, 1));
        }
        else if (nameRotate == "U")
        {
            _rubikRotations.FacingForward(Vector3.up, 1, new Vector3(0, 1, 1));
        }
        else if (nameRotate == "U'")
        {
            _rubikRotations.FacingForward(Vector3.up, -1, new Vector3(0, 1, 1));
        }
        else if (nameRotate == "D")
        {
            _rubikRotations.FacingForward(Vector3.up, -1, new Vector3(0, -1, 1));
        }
        else if (nameRotate == "D'")
        {
            _rubikRotations.FacingForward(Vector3.up, 1, new Vector3(0, -1, 1));
        }
    }
}
