using UnityEngine;

public abstract class InventoryItemData
{
    public InventoryItemSO config;
    public int itemID;
    public int quantity;
    public string itemName;
    public string flavourText;
    public Sprite icon;
    public ItemType itemType;
}
