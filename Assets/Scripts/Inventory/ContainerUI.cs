using System.Collections.Generic;
using UnityEngine;

public class ContainerUI : MonoBehaviour
{
    public InventoryManager targetInventory;
    public GameObject buttonPrefab;
    public Transform contentParent;
    public Transform containerParent;
    private List<GameObject> uiButtons = new();

    public void InitalizeUI(InventoryContainer container_)
    {
        Dictionary<InventoryItemSO, InventoryItemData> inventoryRef = targetInventory.inventory;
        Dictionary<InventoryItemSO, InventoryItemData> containerRef = container_.containerInventory;


        foreach (InventoryItemData item in inventoryRef.Values)
        {
            GameObject tmp = Instantiate(buttonPrefab, contentParent);
            tmp.GetComponent<InventoryButtonUI>().InitalizeButton(item);
            uiButtons.Add(tmp);
        }
        foreach (InventoryItemData item in containerRef.Values)
        {
            GameObject tmp = Instantiate(buttonPrefab, contentParent);
            tmp.GetComponent<InventoryButtonUI>().InitalizeButton(item);
            uiButtons.Add(tmp);
        }

    }
    public void UpdateContainerUI(InventoryContainer containeir_)
    {
        foreach(GameObject button in uiButtons)
        {
            Destroy(button);
        }
        uiButtons.Clear();
    }
}
