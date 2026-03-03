using UnityEngine;

public class ArmorItemData:InventoryItemData
{
    public int armorRating;
    public ArmorSlot armorSlot;

    public ArmorItemData(ArmorItemSO config_)
    {
        this.config = config_;
        this.flavourText = config_.flavourText;
        this.itemName = config_.itemName;
        this.icon = config_.icon;
        armorRating = config_.armorRating;
        armorSlot = config_.armorSlot;
    }
}
