using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerBuutton : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text flavourText;
    public Image icon;
    public TMP_Text quanityDisplay;
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
