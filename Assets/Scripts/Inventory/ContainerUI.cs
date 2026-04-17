using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ContainerUI : MonoBehaviour
{
    public GameObject containerUI;
    public InventoryManager targetInventory;
    public GameObject buttonPrefab;
    public Transform contentParent;
    public Transform containerParent;
    private List<GameObject> uiButtons = new();

    public InventoryContainer debugContainer;

    private void Start()
    {
        
    }
    [ContextMenu("InitalizeDebugUI")]
    public void InitalizeDebugUI()
    {
        InitalizeUI(debugContainer);
    }


    public void InitalizeUI(InventoryContainer container_)
    {
        containerUI.SetActive(true);
        Dictionary<InventoryItemSO, InventoryItemData> inventoryRef = targetInventory.inventory;
        Dictionary<InventoryItemSO, InventoryItemData> containerRef = container_.containerInventory;


        foreach (InventoryItemData item in inventoryRef.Values)
        {
            GameObject tmp = Instantiate(buttonPrefab, contentParent);
            tmp.GetComponent<ContainerBuutton>().InitalizeButton(item,container_,false);
            uiButtons.Add(tmp);
        }
        foreach (InventoryItemData item in containerRef.Values)
        {
            GameObject tmp = Instantiate(buttonPrefab, containerParent);
            tmp.GetComponent<ContainerBuutton>().InitalizeButton(item, container_, true);
            uiButtons.Add(tmp);
        }
        container_.onContainerUpdate += UpdateContainerUI;
    }
    public void UpdateContainerUI(InventoryContainer container_)
    {
        foreach(GameObject button in uiButtons)
        {
            Destroy(button);
        }
        uiButtons.Clear();
        Dictionary<InventoryItemSO, InventoryItemData> inventoryRef = targetInventory.inventory;
        Dictionary<InventoryItemSO, InventoryItemData> containerRef = container_.containerInventory;

        foreach (InventoryItemData item in inventoryRef.Values)
        {
            GameObject tmp = Instantiate(buttonPrefab, contentParent);
            tmp.GetComponent<ContainerBuutton>().InitalizeButton(item, container_, false);
            uiButtons.Add(tmp);
        }
        foreach (InventoryItemData item in containerRef.Values)
        {
            GameObject tmp = Instantiate(buttonPrefab, containerParent);
            tmp.GetComponent<ContainerBuutton>().InitalizeButton(item, container_, true);
            uiButtons.Add(tmp);
        }
    }
    public void CloseContainerUI()
    {
        foreach (GameObject button in uiButtons)
        {
            Destroy(button);
        }
        uiButtons.Clear();
        containerUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
