using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class TwoDGameState : MonoBehaviour
{
    public static TwoDGameState Instance;
    public MapNavigation mapNavigation;
    public TwoDMapGameState mapgameState;
    public MainUIScript mainUIScript;
    public Transform mapParent;
    private EnemySpawner spawner;
    private MapChests chestScript;
    private int currentMapID;
    [SerializeField] private MapState currentMapState;

    public GameObject playerPrefab;
    public GameObject player;

    public bool[] treasureChests;

    public SaveData2D saveData;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        foreach (MapState mapState in mapgameState.mapStates)
        {
            mapState.InitalizeMDictionary();
        }
        InitializeMap(0);

    }
    public void StartNewGame()
    {
        player = Instantiate(playerPrefab);
        mapNavigation.player = player.transform;
        mapNavigation.GoToMap(0, 0);
        player.GetComponent<SpritCharScript>().HP = player.GetComponent<SpritCharScript>().MaxHP;
        for (int index = 0; index < treasureChests.Length; index++)
        {
            treasureChests[index] = false;
        }
        mainUIScript.ResetChestsUI();

        InventoryManager.instance.inventory = new();
        EquipmentManager.instance.ClearInventory();
        Debug.Log("StartNew");
    }
    public bool LoadSaveGame()
    {
        try
        {
            if (LoadSaveDate())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
    public void InitializeMap(int mapID_)
    {

        foreach (MapState mapState in mapgameState.mapStates)
        {
            if (mapState.mapID == mapID_)
            {
                currentMapState = mapState;
                BeginEnemySpawn(currentMapState);
                //initialize chest on map
                BeginChestSpawn(currentMapState);
                break;
            }
        }
    }
    public void BeginEnemySpawn(MapState map)
    {
        spawner = mapParent.GetComponentInChildren<EnemySpawner>();
        if (spawner != null)
        {
            foreach (EnemyState enemy in map.enemyStates)
            {
                if (enemy.currentHP > 0)
                {
                    spawner.Spawn(enemy.EnemyID, enemy.currentHP);
                }
            }
        }
    }
    public void BeginChestSpawn(MapState map)
    {
        chestScript = mapParent.GetComponentInChildren<MapChests>();
        
        if (chestScript != null && chestScript.chests.Count > 0)
        {
            foreach (TreasureChest chest in map.chestStates)
            {
                if (chest.defaultInventory)
                {
                    
                    chestScript.chests[chest.chestID].FillWithStartingInv();
                }
                else
                {
                    chestScript.chests[chest.chestID].containerInventory.Clear();
                    foreach (InventoryItemData itemData in chest.currentInventory)
                    {
                        chestScript.chests[chest.chestID].FillWithDataInventory(itemData);
                    }

                }
            }
        }
    }
    public void RestEnemies()
    {
        foreach (MapState m in mapgameState.mapStates)
        {
            foreach (EnemyState e in m.enemyStates)
            {
                e.currentHP = e.maxHP;
            }
        }
    }
    public void SaveGameState()
    {
        if (spawner != null)
        {
            List<Enemy> enemies = spawner.activeEnemies;
            foreach (Enemy enemy in enemies)
            {
                currentMapState.enemyDictionary[enemy.enemyID].currentHP = enemy.HP;
            }
        }

    }
    public void TreasureGet(int index)
    {
        treasureChests[index] = true;
        mainUIScript.TreasureUIGet(index);
    }
    public void EnableControls(bool isEnabled)
    {
        if (player != null)
        {
            //player.GetComponent<SpritCharScript>().enabled = isEnabled;
        }

    }
    public void SaveDataUpdate()
    {
        saveData = new SaveData2D();
        saveData.mapStates = mapgameState;
        saveData.treasureBools = treasureChests;
        saveData.currentMapIndex = mapNavigation.currentMapIndex;
        saveData.playerCurrentHP = player.GetComponent<SpritCharScript>().HP;
        saveData.currentInventoryState = new List<ItemState>();
        foreach (KeyValuePair<InventoryItemSO, InventoryItemData> item in InventoryManager.instance.inventory)
        {
            ItemState tmp = new ItemState();
            tmp.itemSO = item.Key;
            tmp.quantity = item.Value.quantity;
            saveData.currentInventoryState.Add(tmp);
        }
        saveData.currentEquipmentState = new EquipmentState();
        saveData.currentEquipmentState.equipmentArray = new InventoryItemSO[EquipmentManager.instance.equipmentDictionary.Count];
        int index = 0;
        foreach (KeyValuePair<ItemType, InventoryItemData> equipment in EquipmentManager.instance.equipmentDictionary)
        {
            saveData.currentEquipmentState.equipmentArray[index] = InventoryManager.instance.inventory.FirstOrDefault(x => x.Value == equipment.Value).Key;
            index++;
        }
    }
    [ContextMenu("JSON save")]
    public void SaveData()
    {
        SaveDataUpdate();
        if (!Directory.Exists(Application.persistentDataPath + "/Assets/Resources"))
        {
            try
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Assets/Resources");
            }
            catch { Debug.LogWarning("Failed to create path."); }
        }
        string filePath = Application.persistentDataPath + "/Assets/Resources/save.json";
        string json = JsonUtility.ToJson(saveData);
        try
        {
            File.WriteAllText(filePath, json);
            Debug.Log($"Saved at {filePath}");
        }
        catch
        {
            Debug.Log("Failed to save to location.");
        }
    }
    [ContextMenu("JSON Load")]
    public bool LoadSaveDate()
    {
        string file = Application.persistentDataPath + "/Assets/Resources/save.json";
        if (File.Exists(file))
        {
            try
            {
                string json = File.ReadAllText(file);

                saveData = JsonUtility.FromJson<SaveData2D>(json);
                Debug.Log("Loaded");
            }
            catch
            {
                Debug.Log("Fail to load.");
            }


            mapgameState = saveData.mapStates;
            foreach (MapState mapState in mapgameState.mapStates)
            {
                mapState.InitalizeMDictionary();
            }
            treasureChests = saveData.treasureBools;
            for (int index = 0; index < treasureChests.Length; index++)
            {
                if (treasureChests[index])
                {
                    MainUIScript.instance.TreasureUIGet(index);
                }
            }
            player = Instantiate(playerPrefab);
            mapNavigation.player = player.transform;
            mapNavigation.GoToMap(saveData.currentMapIndex, 0);
            player.GetComponent<SpritCharScript>().HP = saveData.playerCurrentHP;

            InventoryManager.instance.inventory = new();

            foreach (ItemState loadedData in saveData.currentInventoryState)
            {
                try
                {
                    InventoryManager.instance.AddItem(loadedData.itemSO);
                    InventoryManager.instance.inventory[loadedData.itemSO].quantity = loadedData.quantity;
                }
                catch { }

            }

            EquipmentManager.instance.equipmentDictionary = new Dictionary<ItemType, InventoryItemData>();
            EquipmentManager.instance.InitalizeEquipment();

            foreach (InventoryItemSO loadedData in saveData.currentEquipmentState.equipmentArray)
            {
                try
                {
                    EquipmentManager.instance.EquipItem(InventoryManager.instance.inventory[loadedData]);
                }
                catch { }
            }

            return true;
        }
        else
        {
            Debug.LogError("Save file not found.");
            return false;
        }
    }
    public void ReturnToMainMenu()
    {
        try
        {
            MapNavigation.Instance.ClearMap();
        }
        catch { }
        Destroy(player);
    }
    public void PlayerKilled()
    {
        MapNavigation.Instance.ClearMap();
        MainUIScript.instance.OpenGameOverPanel();
    }
}

[Serializable]
public class MapState
{
    public int mapID;
    public List<EnemyState> enemyStates;
    [NonSerialized] public Dictionary<int, EnemyState> enemyDictionary;
    public List<TreasureChest> chestStates;
    public void InitalizeMDictionary()
    {
        enemyDictionary = new Dictionary<int, EnemyState>();
        foreach (EnemyState enemy in enemyStates)
        {
            enemyDictionary.Add(enemy.EnemyID, enemy);
        }
    }
}

[Serializable]
public class EnemyState
{
    public int EnemyID;
    public int currentHP;
    public int maxHP;
}
[Serializable]
public class TwoDMapGameState
{
    public List<MapState> mapStates;
}
[Serializable]
public class ItemState
{
    public InventoryItemSO itemSO;
    public int quantity;
}
[Serializable]
public class EquipmentState
{
    public InventoryItemSO[] equipmentArray;
}
//[Serializable]
//public class ChestState
//{
//    public bool defaultItems;
//    public List<InventoryItemData> itemList;
//}
[Serializable]
public class SaveData2D
{
    public TwoDMapGameState mapStates;
    public bool[] treasureBools;
    public int currentMapIndex;
    public int playerCurrentHP;
    public List<ItemState> currentInventoryState;
    public EquipmentState currentEquipmentState;
}


