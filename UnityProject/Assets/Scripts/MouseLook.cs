using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    public enum RotationAxes 
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    [SerializeField] private RotationAxes _axes = RotationAxes.MouseXAndY;
    [SerializeField] private float _horizontalSpeed = 9.0f;
    [SerializeField] private float _verticalSpeed = 9.0f;
    [SerializeField] private float _minVertical = -45.0f;
    [SerializeField] private float _maxVertical = 45.0f;
    private float _verticalRotation = 0;
    private bool _enable = true;

    void Update() 
    {
        if(_enable)
        {
            if (_axes == RotationAxes.MouseX) 
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * _horizontalSpeed, 0);
            }
            else if (_axes == RotationAxes.MouseY) 
            {
                _verticalRotation -= Input.GetAxis("Mouse Y") * _verticalSpeed;
                _verticalRotation = Mathf.Clamp(_verticalRotation, _minVertical, _maxVertical);

                float horizontalRot = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(_verticalRotation, horizontalRot, 0);
            }
            else 
            {
                _verticalRotation -= Input.GetAxis("Mouse Y") * _verticalSpeed;
                _verticalRotation = Mathf.Clamp(_verticalRotation, _minVertical, _maxVertical);

                float delta = Input.GetAxis("Mouse X") * _horizontalSpeed;
                float horizontalRot = transform.localEulerAngles.y + delta;
                
                transform.localEulerAngles = new Vector3(_verticalRotation, horizontalRot, 0);
            }
        }
    }

    public bool Enable{
        get{
            return _enable;
        }

        set{
            _enable = value;
        }
    }
}