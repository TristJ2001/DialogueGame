using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void RemoveItemAction(string value);
    public static event RemoveItemAction OnRemoveItemAction;
    
    public delegate void ItemAddedAction(string input);
    public static event ItemAddedAction OnItemAdded;
    
    public static Inventory _instance { get; private set; }

    private HashMap<string, GameObject> _inventory = new HashMap<string, GameObject>();
    
    public void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
    }

    public int GetSize()
    {
        return _inventory.Size;
    }

    public void Add(string itemName, GameObject itemGameObject)
    {
        itemGameObject.SetActive(false);
        _inventory.Add(itemName, itemGameObject);
        OnItemAdded?.Invoke(itemName);
    }

    public GameObject Remove(string itemName)
    {
        if (!_inventory.HasKey(itemName))
        {
            return null;
        }

        
        GameObject removedObject = _inventory.GetValue((itemName));
        OnRemoveItemAction?.Invoke(itemName);
        _inventory.Remove(itemName);

        return removedObject;
    }

    public bool HasItem(string itemName)
    {
        if (_inventory.HasKey(itemName))
        {
            return true;
        }

        return false;
    }
}
