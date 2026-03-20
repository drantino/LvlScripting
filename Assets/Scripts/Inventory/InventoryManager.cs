using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<InventoryItemSO, InventoryItemData> inventory = new Dictionary<InventoryItemSO, InventoryItemData>();
    public InventoryItemSO[] tmp;
    public static InventoryManager instance;
    public InventoryItemData[] inventoryItemRefrenceList;
    public Dictionary<int, InventoryItemData> inventoryItemReferenceDictionary = new Dictionary<int, InventoryItemData>();
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }       
        foreach (InventoryItemSO item in tmp)
        {
            AddItem(item);
        }

    }

    public void AddItem(InventoryItemSO itemToAdd_)
    {
        if(!inventory.TryAdd(itemToAdd_,itemToAdd_.CreateRunttimeData()))
        {
            inventory[itemToAdd_].quantity++;
        }
    }
    public void RemoveItem(InventoryItemSO itemToRemove_)
    {
        //if(inventory.TryGetValue(itemToRemove_ , ))

        if (inventory[itemToRemove_].quantity > 1)
        {
            inventory[itemToRemove_].quantity--;
            return;
        }
        inventory.Remove(itemToRemove_);
    }
}
