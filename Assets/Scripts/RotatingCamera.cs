using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    public float SpeedRotation = 1.0f;
    
    private bool _mouseR = false;
    private Vector3 _mousePosition;
    private float _oldAngleX;
    private float _oldAngleY;

    void Update()
    {
        InputEvent();
        Rotating();
    }

    void Rotating()
    {
        if (_mouseR)
        {
            float _currentAngleX = (_mousePosition.y - Input.mousePosition.y);
            float _currentAngleY = -(_mousePosition.x - Input.mousePosition.x);
            
            gameObject.transform.RotateAround(Vector3.zero, gameObject.transform.right, (_currentAngleX - _oldAngleX) * SpeedRotation * Time.deltaTime);
            gameObject.transform.RotateAround(Vector3.zero, gameObject.transform.up, (_currentAngleY - _oldAngleY) * SpeedRotation * Time.deltaTime);

            _oldAngleX = _currentAngleX;
            _oldAngleY = _currentAngleY;
        }
    }

    void InputEvent()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _mouseR = true;
            _mousePosition = Input.mousePosition;
            _oldAngleX = 0.0f;
            _oldAngleY = 0.0f;
        }
        
        if (Input.GetMouseButtonUp(1))
        {
            _mouseR = false;
        }
    }
}
