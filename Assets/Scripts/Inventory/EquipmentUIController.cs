using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUIController : MonoBehaviour
{
    public List<EquipmentUISlot> equipmentUISlots = new();
    public Dictionary<EquipmentSlot, Image> equipmentUIDictonary = new();

    private void Start()
    {
        foreach(var slot in equipmentUISlots)
        {
            equipmentUIDictonary.Add(slot.equipmentType, slot.uiImage);
        }
        EquipmentManager.instance.onEquip += UpdateUI;
    }
    public void UpdateUI(Dictionary<EquipmentSlot, InventoryItemData> equipment)
    {
        foreach(EquipmentSlot equipmentSlot in equipment.Keys)
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
}
[Serializable]
public class EquipmentUISlot
{
    public EquipmentSlot equipmentType;
    public Image uiImage;
}