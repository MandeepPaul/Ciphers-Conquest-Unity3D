using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovements : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] private float _moveSpeed = 6.0f;
    [SerializeField] private float _jumpSpeed = 15.0f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _terminalVelocity = -10.0f;
    [SerializeField] private float _minimumFall = -1.5f;
    [SerializeField] private float _pushForce = 3.0f;

    private float _verticalSpeed;
    private CharacterController _characterController;
    public float _rotationSpeed = 15.0f;
    private ControllerColliderHit _contact;
    private Animator _anim;

    void Start() 
    {
        _characterController = GetComponent<CharacterController>();
        _verticalSpeed = _minimumFall;
        _anim = GetComponent<Animator>();
    }

    void Update() 
    {
        Vector3 movement = Vector3.zero;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0) 
        {
            Vector3 right = _target.right;//(1,0,0)
            Vector3 forward = Vector3.Cross(right, Vector3.up);
            movement = (right * horizontalInput) + (forward * verticalInput);

            movement *= _moveSpeed; 
            movement = Vector3.ClampMagnitude(movement, _moveSpeed);

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, _rotationSpeed * Time.deltaTime);
        }
        _anim.SetFloat("speed", movement.sqrMagnitude);//
        bool hitGround = false;
        RaycastHit hit;

        if (_verticalSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)) 
        {
            float check = (_characterController.height + _characterController.radius) / 1.9f;
            //print(check);
            hitGround = hit.distance <= check;
           // print(hit.distance+"    "+check);
        }


        if (hitGround) 
        {
            if (Input.GetButtonDown("Jump")) 
            {
                _verticalSpeed = _jumpSpeed;
            }
            else 
            {
                _verticalSpeed = _minimumFall;
                _anim.SetBool("isJumping", false);//
            }
        }
        else 
        {
            _verticalSpeed += _gravity * 5 * Time.deltaTime;

            if (_verticalSpeed < _terminalVelocity) 
            {
                _verticalSpeed = _terminalVelocity;
            }


            if (_contact != null) 
            {
                _anim.SetBool("isJumping", true);
            }

            if (_characterController.isGrounded) 
            {
                if (Vector3.Dot(movement, _contact.normal) < 0) 
                {
                    movement = _contact.normal * _moveSpeed;
                }
                else 
                {
                    movement += _contact.normal * _moveSpeed;
                }
            }
            
        }
        movement.y = _verticalSpeed;
        
        movement *= Time.deltaTime;
        _characterController.Move(movement);

    }

    public void setJump(float speed)
    {
        _jumpSpeed = speed;
    }

    void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        _contact = hit;
        Rigidbody rb = hit.collider.attachedRigidbody;
        if (rb != null && !rb.isKinematic)
        {
            rb.velocity = hit.moveDirection * _pushForce;
        }
    }

}
