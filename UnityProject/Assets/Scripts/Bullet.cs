using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 35.0f;
    [SerializeField] private int _damage = 1;

    void Update() 
    {
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) 
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        
        if (player != null) 
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            player.Hurt(_damage);
        }
        
        Destroy(this.gameObject, 2f);
    }
}
