using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour 
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _maxZ = 10.0f;
    [SerializeField] private float _minZ = -10.0f;
    private int _direction = 1;

    void Update() 
    {
        transform.Translate(0, 0, _direction * _speed * Time.deltaTime);
        bool bounced = false;
        
        if (transform.position.z > _maxZ || transform.position.z < _minZ) 
        {
            _direction = -_direction;
            bounced = true;
        }
        if (bounced) 
        {
            transform.Translate(0, 0, _direction * _speed *
            Time.deltaTime);
        }
    }
}
