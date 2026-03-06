using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public InventoryManager targetInventory;
    public GameObject buttonPrefab;
    public Transform contentParent;

    public void InitUI()
    {
        Dictionary<InventoryItemSO, InventoryItemData> inventoryRef = targetInventory.inventory;

        ClearContentChildren();

        foreach (InventoryItemData item in inventoryRef.Values)
        {
            GameObject tmp = Instantiate(buttonPrefab, contentParent);
            tmp.GetComponent<InventoryButtonUI>().InitalizeButton(item);
        }
    }
    public void InitalizeInvUIByType(int slotType)
    {
        Dictionary<InventoryItemSO, InventoryItemData> inventoryRef = targetInventory.inventory;
        ClearContentChildren();
        
        switch (slotType)
        {
            case 0:
                {
                    foreach (InventoryItemData item in inventoryRef.Values)
                    {
                        if (item.itemType == ItemType.HELM)
                        {
                            GameObject tmp = Instantiate(buttonPrefab, contentParent);
                            tmp.GetComponent<InventoryButtonUI>().InitalizeButton(item);
                        }
                    }
                    break;
                }
            case 1:
                {
                    foreach (InventoryItemData item in inventoryRef.Values)
                    {
                        if (item.itemType == ItemType.ARMOR)
                        {
                            GameObject tmp = Instantiate(buttonPrefab, contentParent);
                            tmp.GetComponent<InventoryButtonUI>().InitalizeButton(item);
                        }
                    }
                    break;
                }
            case 2:
                {
                    foreach (InventoryItemData item in inventoryRef.Values)
                    {
                        if (item.itemType == ItemType.SHIELD)
                        {
                            GameObject tmp = Instantiate(buttonPrefab, contentParent);
                            tmp.GetComponent<InventoryButtonUI>().InitalizeButton(item);
                        }
                    }
                    break;
                }
            case 3:
                {
                    foreach (InventoryItemData item in inventoryRef.Values)
                    {
                        if (item.itemType == ItemType.WEAPON)
                        {
                            GameObject tmp = Instantiate(buttonPrefab, contentParent);
                            tmp.GetComponent<InventoryButtonUI>().InitalizeButton(item);
                        }
                    }
                    break;
                }
            case 4:
                {
                    foreach (InventoryItemData item in inventoryRef.Values)
                    {
                        if (item.itemType == ItemType.OTHER)
                        {
                            GameObject tmp = Instantiate(buttonPrefab, contentParent);
                            tmp.GetComponent<InventoryButtonUI>().InitalizeButton(item);
                        }
                    }
                    break;
                }
            case 5:
                {
                    foreach (InventoryItemData item in inventoryRef.Values)
                    {
                        if (item.itemType == ItemType.HELM)
                        {
                            GameObject tmp = Instantiate(buttonPrefab, contentParent);
                            tmp.GetComponent<InventoryButtonUI>().InitalizeButton(item);
                        }
                    }
                    break;
                }
        }
    }
    public void ClearContentChildren()
    {
        if (contentParent.childCount > 0)
        {
            for (int index = contentParent.childCount - 1; index >= 0; index--)
            {
                Destroy(contentParent.GetChild(index).gameObject);
            }
        }
    }
}
