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

    public void InitalizeButton(InventoryItemData inventoryItemData)
    {
        itemName.text = inventoryItemData.itemName;
        flavourText.text = inventoryItemData.flavourText;
        quanityDisplay.text = inventoryItemData.quantity.ToString();
        icon.sprite = inventoryItemData.icon;
        itemData = inventoryItemData;
        GetComponent<Button>().onClick.AddListener(ButtonClick);
    }
    public void ButtonClick()
    {
        EquipmentManager.instance.EquipItem(itemData);
    }
}
