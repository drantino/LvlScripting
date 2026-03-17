using UnityEngine;

public class ChestContainerIneractable : MonoBehaviour,IInteractable
{
    public InventoryContainer container;
    public void Interact(GameObject sentGameObject)
    {
        MainUIScript.instance.containerUIPanel.GetComponent<ContainerUI>().InitalizeUI(container);
        container.playerInventoryManager = MainUIScript.instance.containerUIPanel.GetComponent<ContainerUI>().targetInventory;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        container = GetComponent<InventoryContainer>();
    }

}
