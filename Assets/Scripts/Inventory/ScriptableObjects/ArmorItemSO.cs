using UnityEngine;

[CreateAssetMenu(fileName = "ArmorItemSO", menuName = "InventorySystem/ArmorItemSO")]
public class ArmorItemSO : InventoryItemSO
{
    public int armorRating;

    public override InventoryItemData CreateRunttimeData()
    {
        return new ArmorItemData(this);
    }
}
