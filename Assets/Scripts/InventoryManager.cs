using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;


// another way for the remove item function is to represent each item in the world with a unique item holder in the inventory so when we click the remove button it automatically knows what item to be taken off the List
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;
    public Toggle EnableRemove;

    private void Awake()
    {
        Instance = this;
    }


    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        // Clear the inventory 
        foreach (Transform inventoryitem in ItemContent)
        {
            Destroy(inventoryitem.gameObject);
        }


        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            RemoveItem removeItemScript = obj.transform.Find("ItemInfo").GetComponent<RemoveItem>();


            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (removeItemScript != null)
            {
                removeItemScript.item = item;
            }
        }

    }

    public void EnableItemsRemove()
    {
        foreach (Transform item in ItemContent)
            item.Find("RemoveButton").gameObject.SetActive(EnableRemove.isOn);
    }
}