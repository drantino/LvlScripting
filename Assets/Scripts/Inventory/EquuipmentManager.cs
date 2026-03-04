using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Dictionary<EquipmentSlot, InventoryItemData> equipmentDictionary = new();
    public static EquipmentManager instance;
    public event Action<Dictionary<EquipmentSlot, InventoryItemData>> onEquip;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        InitalizeEquipment();
    }
    public void InitalizeEquipment()
    {
        equipmentDictionary.Add(EquipmentSlot.HELM, null);
        equipmentDictionary.Add(EquipmentSlot.CHEST, null);
        equipmentDictionary.Add(EquipmentSlot.LEGS, null);
        equipmentDictionary.Add(EquipmentSlot.BOOTS, null);
        equipmentDictionary.Add(EquipmentSlot.WEAPON, null);

    }
    public void EquipItem(InventoryItemData itemToEquip)
    {
        if (itemToEquip is ArmorItemData armor)
        {
            equipmentDictionary[armor.equipmentSlot] = armor;
            Debug.Log(equipmentDictionary[armor.equipmentSlot].itemName + " was equipped");
        }
        else if (itemToEquip is WeaponItemData weapon)
        {
            equipmentDictionary[weapon.equipmentSlot] = weapon;
            Debug.Log(equipmentDictionary[weapon.equipmentSlot].itemName + " was equipped");
        }
        onEquip?.Invoke(equipmentDictionary);
    }
}
