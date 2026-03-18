using UnityEngine;

public class WeaponItemData : InventoryItemData
{
    public int weaponStrength;
    public int weaponDurability;

    public WeaponItemData(WeaponItemSO config_)
    {
        this.config = config_;
        itemID = config_.ItemID;
        this.flavourText = config_.flavourText;
        this.weaponStrength = config_.weaponStrength;
        this.itemName = config_.itemName;
        this.icon = config_.icon;
        this.weaponDurability = config_.weaponDurability;
        itemType = config_.itemType;
        quantity = 1;
    }
}
