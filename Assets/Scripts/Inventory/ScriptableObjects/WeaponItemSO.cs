using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItemSO", menuName = "InventorySystem/WeaponItemSO")]
public class WeaponItemSO : InventoryItemSO
{
    public int weaponStrength;
    public int weaponDurability;

    public override InventoryItemData CreateRunttimeData()
    {
        return new WeaponItemData(this);
    }
}
