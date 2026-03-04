using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventoryManager targetInventory;
    public GameObject buttonPrefab;
    public Transform contentParent;

    [ContextMenu("Init UI")]
    public void InitUI()
    {
        Dictionary<InventoryItemSO, InventoryItemData> inventoryRef = targetInventory.inventory;

        foreach(InventoryItemData item in inventoryRef.Values )
        {
            GameObject tmp = Instantiate(buttonPrefab, contentParent);
            tmp.GetComponent<InventoryButtonUI>().InitalizeButton(item);
        }
    }
}
