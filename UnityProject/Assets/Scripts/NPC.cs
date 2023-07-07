using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private float _radius = 1.5f;
    private int count;
    private bool trigger = false;

    void Update()
    {
         Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);
         count = hitColliders.Length;

        foreach (Collider hitCollider in hitColliders)
        {
            Vector3 hitPosition = hitCollider.transform.position;
            hitPosition.y = transform.position.y;
            Vector3 direction = hitPosition - transform.position;

            if (Vector3.Dot(transform.forward, direction.normalized) > .5f &&  count > 2 && !trigger)
            {
                trigger = true;
                //hitCollider.SendMessage("StartDialogue", SendMessageOptions.DontRequireReceiver);
                GameObject.Find("Dialogue").GetComponent<Dialogue>().StartDialogue();
            }
        }
        
    }

}
