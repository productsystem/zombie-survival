using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerClickHandler
{
    public ItemObject item;
    public InventoryObject inventory;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null && inventory != null)
        {
            inventory.UseItem(item, 1, gameObject);
        }
    }
}
