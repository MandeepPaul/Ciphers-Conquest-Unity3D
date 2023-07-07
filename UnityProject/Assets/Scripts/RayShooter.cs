using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RayShooter : MonoBehaviour
{
    [SerializeField] private GameObject _impactEffect;
    [SerializeField] private float _impactForce = 100f;
    private ItemData[] _items = new ItemData[3];
    private Camera _cam;
    private bool weaponActivated = false;

    void Start() 
    {
        _cam = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _items = GameObject.FindGameObjectWithTag("inventory").GetComponent<Inventory>().getItems();
    }

    void Update() 
    {
        for(int i = 0; i < _items.Length && !weaponActivated; i++)
        {
            if(_items[i] != null && _items[i].ObjectName == "Weapon")
            weaponActivated = true;
        }

        if (Input.GetMouseButtonDown(0) && weaponActivated) //Activating shooting.
        {
            Vector3 point = new Vector3(_cam.pixelWidth / 2, _cam.pixelHeight/2, 0);
            Ray ray = _cam.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                
                if (target != null) 
                {
                    target.ReactToHit();
                }

                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * _impactForce);
                }

                Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));//Making particles come outwards.
            }

            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }

    public void activateWeapon(bool trigger)
    {
        weaponActivated = trigger;
    }

    void OnGUI() 
    {
        if(weaponActivated)
        {
            int size = 20;
            float posX = _cam.pixelWidth/2 - size/4;
            float posY = _cam.pixelHeight/2 - size/2;
            GUI.Label(new Rect(posX, posY, size, size), "+");
        }
    }

}
