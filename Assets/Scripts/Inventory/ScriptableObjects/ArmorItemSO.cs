using UnityEngine;

[CreateAssetMenu(fileName = "ArmorItemSO", menuName = "InventorySystem/ArmorItemSO")]
public class ArmorItemSO : InventoryItemSO
{
    public int armorRating;
    public EquipmentSlot equipmentSlot;

    public override InventoryItemData CreateRunttimeData()
    {
        return new ArmorItemData(this);
    }
}
public enum EquipmentSlot
{
    HELM, 
    CHEST, 
    LEGS, 
    BOOTS,
    WEAPON
}