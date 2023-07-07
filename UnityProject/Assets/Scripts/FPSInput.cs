using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSInput : MonoBehaviour 
{
    [SerializeField] private float _speed = 6.0f;
    [SerializeField] private float _gravity = -9.8f;
    private CharacterController _charController;

    void Start() 
    {
        _charController = GetComponent<CharacterController>();
    }

    void Update() 
    {
        float deltaX = Input.GetAxis("Horizontal") * _speed;
        float deltaZ = Input.GetAxis("Vertical") * _speed;
        //float deadZone = 0.1f;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        movement = Vector3.ClampMagnitude(movement, _speed);

        movement.y = _gravity;
        movement *= Time.deltaTime;

        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
    }
}
