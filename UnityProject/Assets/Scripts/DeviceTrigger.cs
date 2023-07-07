using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] _targets;
    [SerializeField] private GameObject _keypad;

    void OnTriggerEnter(Collider other) 
    {
        foreach (GameObject target in _targets) 
        {
            if(target.tag == "Door3" && other.tag == "Player")  //Enable keypad when player approach Door3 and enable cursor
            {
                _keypad.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GameObject.Find("Player").GetComponent<MouseLook>().Enable = false;
                GameObject.Find("Player").transform.GetChild(2).gameObject.GetComponent<RayShooter>().activateWeapon(false); //Disable weapon
            }
            else
            {
                target.SendMessage("Activate");
            }
        }
    }

    void OnTriggerExit(Collider other) 
    {
        foreach (GameObject target in _targets) 
        {
            if(target.tag == "Door3" && other.tag == "Player") //Desable keypad when player approach Door3 and disable cursor
            {
                _keypad.transform.GetChild(4).gameObject.GetComponent<Keypad>().reset();;
                _keypad.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameObject.Find("Player").GetComponent<MouseLook>().Enable = true;
                GameObject.Find("Player").transform.GetChild(2).gameObject.GetComponent<RayShooter>().activateWeapon(true); //Enable weapon
            }
            else
            target.SendMessage("Deactivate");
        }
    }
}
