using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    private const int _numSlots = 3;
    private Image[] _itemImages = new Image[_numSlots];
    private ItemData[] _items = new ItemData[_numSlots];
    private GameObject[] _slots = new GameObject[_numSlots];

    public void Start() {
        CreateSlots();
    }

    public void CreateSlots() 
    {
        if (_slotPrefab != null) 
        {
            for (int i = 0; i < _numSlots; i++) 
            {
                GameObject newSlot = Instantiate(_slotPrefab);

                newSlot.name = "ItemSlot_" + i;                                             //Changed slot prefab name
                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);    //Rendering InventorySLots and setting it a parent to a slot.

                _slots[i] = newSlot;                                                        //newSlot to an array.
                _itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();       //ItemImage
            }
        }
    }

    public bool AddItem(ItemData itemToAdd) 
    {
        for (int i = 0; i < _items.Length; i++) 
        {
            if (_items[i] != null && _items[i].Type == itemToAdd.Type && itemToAdd.IsStackable == true) 
            {
                _items[i].Quantity = _items[i].Quantity + 1;

                displayQuantity(i);

                return true;
            }

            if (_items[i] == null) 
            {
                _items[i] = Instantiate(itemToAdd);
                _items[i].Quantity = 1;

                displayQuantity(i);

                _itemImages[i].sprite = itemToAdd.Sprite;                                   //Adding sprite to the ItemImage that was empty before.
                _itemImages[i].enabled = true;                                              //Enabling the ItemImage GameObject.

                return true;
            }
        }
        return false;
    }

    public Slot getSlot(int index)
    {
        return _slots[index].gameObject.GetComponent<Slot>();
    }

    public ItemData[] getItems()
    {
        return _items;
    }

    private void displayQuantity(int i)
    {
        Slot slotScript = _slots[i].gameObject.GetComponent<Slot>();
        Text quantityText = slotScript.QtyText;

        quantityText.enabled = true;
        quantityText.text = _items[i].Quantity.ToString();
    }
}
