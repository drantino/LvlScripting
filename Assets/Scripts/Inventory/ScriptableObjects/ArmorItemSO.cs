using UnityEngine;

[CreateAssetMenu(fileName = "ArmorItemSO", menuName = "InventorySystem/ArmorItemSO")]
public class ArmorItemSO : InventoryItemSO
{
    public int armorRating;
    public ArmorSlot armorSlot;

    public override InventoryItemData CreateRunttimeData()
    {
        return new ArmorItemData(this);
    }
}
public enum ArmorSlot
{
    HELM, 
    Chest, 
    legs, 
    Boots
}