using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContainer : MonoBehaviour
{
    public Dictionary<InventoryItemSO, InventoryItemData> containerInventory = new();
    public List<InventoryItemSO> startingInventory = new();
    public InventoryManager playerInventoryManager;
    public event Action<InventoryContainer> onContainerUpdate;
    private void Start()
    {
        
    }
    public void FillWithStartingInv()
    {
        Debug.Log("Fill");
        containerInventory.Clear();
        foreach (InventoryItemSO item in startingInventory)
        {
            if (!containerInventory.TryAdd(item, item.CreateRunttimeData()))
            {
                containerInventory[item].quantity++;
            }
        }
    }
    public void FillWithDataInventory(InventoryItemData itemToAdd_)
    {
        containerInventory.TryAdd(itemToAdd_.config, itemToAdd_.config.CreateRunttimeData());
        containerInventory[itemToAdd_.config].quantity = itemToAdd_.quantity;
    }
    public void AddItemTOContainer(InventoryItemSO itemToAdd_)
    {
        playerInventoryManager.RemoveItem(itemToAdd_);
        if (!containerInventory.TryAdd(itemToAdd_, itemToAdd_.CreateRunttimeData()))
        {
            containerInventory[itemToAdd_].quantity++;
        }
        onContainerUpdate?.Invoke(this);
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
        playerInventoryManager.AddItem(itemToRemove_);
        onContainerUpdate?.Invoke(this);
    }
}
