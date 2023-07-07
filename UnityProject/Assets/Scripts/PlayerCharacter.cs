using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float _maxHitPoints = 10;
    [SerializeField] private HitPoints _hitPoints;
    [SerializeField] private float _startingHitPoints = 5;
    [SerializeField] private HealthBar _healthBarPrefab;
    [SerializeField] Inventory _inventoryPrefab;
    [SerializeField] private GameObject _gunPrefab;
    private Inventory _inventory;
    private HealthBar _healthBar;
    
    void Awake() 
    {
        _hitPoints.Value = _startingHitPoints;
        _healthBar = Instantiate(_healthBarPrefab);
        _healthBar.Character = this;
        _inventory = Instantiate(_inventoryPrefab);
    }

    void Update()
    {
        if(this.transform.position.y < -1.0f || _hitPoints.Value == 0)
        {
            Transform parent, child;
            child = this.transform.GetChild(2);
            parent = GameObject.Find("AfterDead").transform;

            child.SetParent(parent.transform);
            child.position = parent.position;

            parent.GetChild(0).gameObject.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameObject.Find("Player").GetComponent<MouseLook>().Enable = false;
            this.gameObject.GetComponent<RayShooter>().activateWeapon(false);

            Destroy(this.gameObject);
        }
    }
    
    public float MaxHitPoints{
        get{
            return _maxHitPoints;
        }
        set {
            _maxHitPoints = value;
        }
    }

    public void Hurt(int damage) 
    {
        _hitPoints.Value -= damage;
        print("Health: " + _hitPoints.Value);
    }

    public void SavePlayer() 
    {
        GameManager.SharedInstance.Health = _hitPoints.Value;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PickUp")) 
        {

            ItemData hitObject = collision.gameObject.GetComponent<PickUp>().Item;

            if (hitObject != null) 
            {
                print("Hit: " + hitObject.ObjectName);
                bool shouldDisappear = false;

                switch (hitObject.Type) 
                {
                    case ItemData.ItemType.Weapon: 
                    {
                        shouldDisappear = _inventory.AddItem(hitObject);

                        AudioSource audio = GetComponent<AudioSource>();
                        audio.Play();
                    }
                    break;
                    case ItemData.ItemType.Health:
                    shouldDisappear = _inventory.AddItem(hitObject);
                    shouldDisappear = AdjustHitPoints(hitObject.Quantity);
                    break;
                    case ItemData.ItemType.Pill:
                    shouldDisappear = _inventory.AddItem(hitObject);
                    this.gameObject.GetComponent<RelativeMovements>().setJump(35f);
                    break;
                }
                
                if (shouldDisappear)
                    collision.gameObject.SetActive(false);

                // if(weapon && unlock)
                // {
                //     AudioSource audio = GetComponent<AudioSource>();
                //     audio.Play();

                //     Transform cam = this.gameObject.transform.GetChild(2);
                //     GameObject gun = collision.gameObject;
                    
                //     if(cam != null)
                //     {
                //         gun.transform.position = cam.GetChild(0).transform.position;
                //         gun.transform.SetParent(cam.transform);
                //         gun.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                //         //gun.GetComponent<RigidBody>().Kinematic = true;
                //     }

                //     unlock = false;

                // }
            }
        }
        else if(collision.gameObject.CompareTag("NPC2"))
        {
            GameObject.Find("Message").SetActive(true);
            print("Level Complete!");
        }
    }

    public bool AdjustHitPoints(int amount) 
    {

        if (_hitPoints.Value < _maxHitPoints) 
        {
            _hitPoints.Value = _hitPoints.Value + amount;
            print("Adjusted HP by: " + amount + ". New value: " + _hitPoints.Value);
            return true;
        }

        return false;
    }

}
