using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Dictionary<ItemType, InventoryItemData> equipmentDictionary = new();
    public static EquipmentManager instance;
    public event Action<Dictionary<ItemType, InventoryItemData>> onEquip;
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
        equipmentDictionary.Add(ItemType.HELM, null);
        equipmentDictionary.Add(ItemType.ARMOR, null);
        equipmentDictionary.Add(ItemType.SHIELD, null);
        equipmentDictionary.Add(ItemType.WEAPON, null);

    }
    public void EquipItem(InventoryItemData itemToEquip)
    {
        if (itemToEquip is ArmorItemData armor)
        {
            equipmentDictionary[armor.itemType] = armor;
            Debug.Log(equipmentDictionary[armor.itemType].itemName + " was equipped");
        }
        else if (itemToEquip is WeaponItemData weapon)
        {
            equipmentDictionary[weapon.itemType] = weapon;
            Debug.Log(equipmentDictionary[weapon.itemType].itemName + " was equipped");
        }
        onEquip?.Invoke(equipmentDictionary);
    }
}
