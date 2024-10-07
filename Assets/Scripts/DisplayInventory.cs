using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    private Animator animator;
    public InventoryObject inventory;
    public int xStart;
    public int yStart;
    public int xSpaceItems;
    public int cols;
    public int ySpaceItems;
    private bool alreadyOn;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    void Start()
    {
        alreadyOn = false;
        CreateDisplay();
        animator = GetComponent<Animator>();
        animator.SetBool("inventoryOn", false);
    }
    void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        for(int i = 0 ; i < inventory.container.Count; i++)
            {
                if(itemsDisplayed.ContainsKey(inventory.container[i]))
                {
                    itemsDisplayed[inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
                }
                else
                {
                    var obj = Instantiate(inventory.container[i].item.prefab, Vector2.zero, Quaternion.identity, transform);
                    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
                    itemsDisplayed.Add(inventory.container[i], obj);
                }
            }
        if(Input.GetKeyDown(KeyCode.I) && alreadyOn == false)
        {
            animator.SetBool("inventoryOn", true);
            alreadyOn = true;
        }
        else if(Input.GetKeyDown(KeyCode.I) && alreadyOn == true)
        {
            animator.SetBool("inventoryOn", false);
            alreadyOn = false;
        }
    }

    public void CreateDisplay()
    {
        for(int i = 0; i < inventory.container.Count; i++)
        {
            var obj = Instantiate(inventory.container[i].item.prefab, Vector2.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
            itemsDisplayed.Add(inventory.container[i], obj);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(xStart + (xSpaceItems * (i % cols)),yStart + ( -ySpaceItems * (i/cols)), 0f);
    }
}
