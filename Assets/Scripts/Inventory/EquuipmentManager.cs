using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Dictionary<ItemType, InventoryItemData> equipmentDictionary = new();
    public static EquipmentManager instance;
    public event Action<Dictionary<ItemType, InventoryItemData>> onEquip, onClearEquipment;
    public int equipmentDEF
    {
        get
        {
            int def = 0;
            if (equipmentDictionary[ItemType.HELM] != null)
            {
                def += (equipmentDictionary[ItemType.HELM] as ArmorItemData).armorRating;
            }
            if (equipmentDictionary[ItemType.SHIELD] != null)
            {
                def += (equipmentDictionary[ItemType.SHIELD] as ArmorItemData).armorRating;
            }
            if (equipmentDictionary[ItemType.ARMOR] != null)
            {
                def += (equipmentDictionary[ItemType.ARMOR] as ArmorItemData).armorRating;
            }

            return def;
        }
    }
    public int equipmentATK
    {
        get
        {
            int atk = 0;
            if (equipmentDictionary[ItemType.WEAPON] != null)
            {
                atk += (equipmentDictionary[ItemType.WEAPON] as WeaponItemData).weaponStrength;
            }

            return atk;
        }
    }

    //public Action ;
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
        MainUIScript.instance.UpdateCharHud();
        onEquip?.Invoke(equipmentDictionary);
    }
    public void ClearInventory()
    {
        equipmentDictionary.Clear();
        InitalizeEquipment();
        onClearEquipment?.Invoke(equipmentDictionary);
    }
}
