using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    private GameObject _bullet;
    private float _speed = 1.5f;
    private float _obstacleRange = 5.0f;
    private bool _isAlive;
    private Animator _anim;

    void Start()
    {
        _isAlive = true;
        _anim = GetComponent<Animator>();
    }

    void Update()
    {

            transform.Translate(0, 0, _speed * Time.deltaTime);


            Ray ray = new Ray(new Vector3(transform.position.x, 1.0f, transform.position.z), transform.forward);
            //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
            
            RaycastHit hit;

            _anim.SetBool("isWalking", true);

            if (Physics.SphereCast(ray, 0.75f, out hit)) 
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>()) 
                {
                    if (_bullet == null) 
                    {
                        _bullet = Instantiate(_bulletPrefab);
                        _bullet.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _bullet.transform.position = new Vector3(_bullet.transform.position.x, 1.0f, _bullet.transform.position.y);
                        
                        _bullet.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < _obstacleRange) 
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }

            if(!_isAlive)
            _anim.SetBool("isWalking", false);
    }

    public void SetAlive(bool alive) 
    {
        _isAlive = alive;
    }


}
