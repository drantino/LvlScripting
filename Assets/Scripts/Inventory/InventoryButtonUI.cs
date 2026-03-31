using TMPro;
using UnityEngine;
using UnityEngine.UI;   

public class InventoryButtonUI : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text flavourText;
    public Image icon;
    public TMP_Text quanityDisplay;
    public InventoryItemData itemData;
    public TMP_Text statRatingTxt;

    public void InitalizeButton(InventoryItemData inventoryItemData)
    {
        itemName.text = inventoryItemData.itemName;
        flavourText.text = inventoryItemData.flavourText;
        quanityDisplay.text = inventoryItemData.quantity.ToString();
        icon.sprite = inventoryItemData.icon;
        itemData = inventoryItemData;
        if (itemData.itemType == ItemType.WEAPON)
        {
            statRatingTxt.text = ((itemData as WeaponItemData).weaponStrength + ":Weapon Str").ToString();
        }
        else if (itemData.itemType == ItemType.HELM || itemData.itemType == ItemType.ARMOR || itemData.itemType == ItemType.SHIELD)
        {
            statRatingTxt.text = ((itemData as ArmorItemData).armorRating + ":Armor Def").ToString();
        }
        else
        {
            statRatingTxt.text = null;
        }
        GetComponent<Button>().onClick.AddListener(ButtonClick);
    }
    public void ButtonClick()
    {
        EquipmentManager.instance.EquipItem(itemData);
    }
}
