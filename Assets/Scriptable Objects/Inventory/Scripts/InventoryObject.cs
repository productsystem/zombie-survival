using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();
    public void AddItem(ItemObject _item, int _amount)
    {
        for(int i = 0 ; i < container.Count; i++)
        {
            if(container[i].item == _item)
            {
                container[i].AddAmount(_amount);
                
                return;
            }
        }
        container.Add(new InventorySlot( _item, _amount));
    }

    public void UseItem(ItemObject _item, int _amount, GameObject itemGame)
    {
        for (int i = container.Count - 1; i >= 0; i--)
        {
            if (container[i].item == _item)
            {
                container[i].AddAmount(-_amount);

                if(_item is FoodObject foodObject)
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().playerHealth+= foodObject.healVal;
                }
                if(_item is BatteryObject battery)
                {
                    GameObject.Find("FlashLight").GetComponent<Flashlight>().battery += battery.chargeVal;
                }
                if (container[i].amount <= 0)
                {
                    Destroy(itemGame);
                    container.Remove(container[i]);
                }
                return;
            }
        }


    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public InventorySlot( ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int val)
    {
        amount += val;
        if(amount < 0)
        amount = 0;
    }
}
