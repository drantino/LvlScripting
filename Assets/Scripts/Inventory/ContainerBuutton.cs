using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerBuutton : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text flavourText;
    public Image icon;
    public TMP_Text quanityDisplay;
    public TMP_Text statRatingTxt;
    public InventoryItemData itemData;
    private InventoryContainer inventoryContainer;
    private bool isContainerButton;

    public void InitalizeButton(InventoryItemData itemData_,InventoryContainer container_, bool isContainerButton_)
    {
        itemData = itemData_;
        isContainerButton = isContainerButton_;
        inventoryContainer = container_;

        itemName.text = itemData.itemName;
        flavourText.text = itemData.flavourText;
        quanityDisplay.text = itemData.quantity.ToString();
        icon.sprite = itemData.icon;
        //add rating if right type
        if(itemData.itemType == ItemType.WEAPON)
        {
            statRatingTxt.text = ((itemData as WeaponItemData).weaponStrength + ":Weapon Str").ToString();
        }
        else if(itemData.itemType == ItemType.HELM || itemData.itemType == ItemType.ARMOR || itemData.itemType == ItemType.SHIELD)
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
        if (isContainerButton)
        {
            inventoryContainer.AddItemToPlayerInventory(itemData.config);
            return;
        }
        inventoryContainer.AddItemTOContainer(itemData.config);
    }
}
