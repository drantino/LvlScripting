using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContainer : MonoBehaviour
{
    private Dictionary<InventoryItemSO, InventoryItemData> containerInventory = new();
    public List<InventoryItemSO> startingInventory = new();
    public InventoryManager playerInventoryManager;
    private void Start()
    {
        foreach (InventoryItemSO item in startingInventory)
        {
            if (!containerInventory.TryAdd(item, item.CreateRunttimeData()))
            {
                containerInventory[item].quantity++;
            }
        }
    }
    public void AddItemTOContainer(InventoryItemSO itemToAdd_)
    {
        playerInventoryManager.RemoveItem(itemToAdd_);
        if (!containerInventory.TryAdd(itemToAdd_, itemToAdd_.CreateRunttimeData()))
        {
            containerInventory[itemToAdd_].quantity++;
        }
    }
    public void AddItemToPlayerInventory(InventoryItemSO itemToRemove_)
    {
        if (containerInventory.TryGetValue(itemToRemove_, out InventoryItemData data))
        {

            if (containerInventory[itemToRemove_].quantity > 1)
            {
                containerInventory[itemToRemove_].quantity--;
            }
            else
            {
                containerInventory.Remove(itemToRemove_);
            }
        }
    }
}
