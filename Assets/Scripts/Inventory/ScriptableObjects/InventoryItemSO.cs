using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemSO", menuName = "InventorySystem/InventoryItemSO")]
public abstract class InventoryItemSO : ScriptableObject
{
    public string itemName;
    public string flavourText;
    public Sprite icon;

    public abstract InventoryItemData CreateRunttimeData();

}
