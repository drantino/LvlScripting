using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUIController : MonoBehaviour
{
    public List<EquipmentUISlot> equipmentUISlots = new();
    public Dictionary<ItemType, Image> equipmentUIDictonary = new();

    private void Start()
    {
        foreach(var slot in equipmentUISlots)
        {
            equipmentUIDictonary.Add(slot.equipmentType, slot.uiImage);
        }
        EquipmentManager.instance.onEquip += UpdateUI;
        EquipmentManager.instance.onClearEquipment += ClearEquipmentUI;
    }
    public void UpdateUI(Dictionary<ItemType, InventoryItemData> equipment)
    {
        foreach(ItemType equipmentSlot in equipment.Keys)
        {
            if (equipment[equipmentSlot] != null)
            {
                equipmentUIDictonary[equipmentSlot].sprite = equipment[equipmentSlot].icon;
                Color tmp = equipmentUIDictonary[equipmentSlot].color;
                tmp.a = 1;
                equipmentUIDictonary[equipmentSlot].color = tmp;
            }
        }
    }
    public void ClearEquipmentUI(Dictionary<ItemType, InventoryItemData> equipment)
    {
        foreach (ItemType equipmentSlot in equipment.Keys)
        {            
                equipmentUIDictonary[equipmentSlot].sprite = null;
        }
    }
}
[Serializable]
public class EquipmentUISlot
{
    public ItemType equipmentType;
    public Image uiImage;
}