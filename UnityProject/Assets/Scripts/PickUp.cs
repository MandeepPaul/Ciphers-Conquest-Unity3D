using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(AudioSource))]
public class PickUp : MonoBehaviour
{
    [SerializeField] private ItemData _item;
    
    public ItemData Item {
        get {
        return _item;
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if(other.tag == "Player" && _item.ObjectName == "Weapon")
    //     {
    //         AudioSource audio = GetComponent<AudioSource>();
    //         audio.Play();
    //     }
    // }

}
