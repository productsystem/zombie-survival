using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Battery,
    Equipment,
    Ammunition,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public InventoryObject inventory;
    public GameObject prefab;
    public ItemType type;

    [TextArea(15,20)]
    public string description;

}
