using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    public void ReactToHit() 
    {
        StartCoroutine(Die());

        Wander behavior = GetComponent<Wander>();

        if (behavior != null) 
        {
            behavior.SetAlive(false);
        }
    }

    private IEnumerator Die() 
    {
        transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

}
