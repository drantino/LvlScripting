using System;
using UnityEngine;
[Serializable]
public abstract class InventoryItemData
{
    public InventoryItemSO config;
    public int quantity;
    public string itemName;
    public string flavourText;
    public Sprite icon;
    public ItemType itemType;
}
