using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemSO", menuName = "InventorySystem/InventoryItemSO")]
public abstract class InventoryItemSO : ScriptableObject
{
    public string itemName;
    public int ItemID;
    public string flavourText;
    public Sprite icon;
    public ItemType itemType;

    public abstract InventoryItemData CreateRunttimeData();

}
public enum ItemType
{
    HELM,
    ARMOR,
    SHIELD,
    WEAPON,
    OTHER
}