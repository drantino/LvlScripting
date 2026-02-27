using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TwoDGameState : MonoBehaviour
{
    public static TwoDGameState Instance;
    public MapNavigation mapNavigation;
    public TwoDMapGameState mapgameState;
    public MainUIScript mainUIScript;
    public Transform mapParent;
    private EnemySpawner spawner;
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
        Debug.Log("StartNew");
    }
    public bool LoadSaveGame()
    {
        try
        {
            LoadSaveDate();
            return true;
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
    public void SaveDataUpdate()
    {
        saveData = new SaveData2D();
        saveData.mapStates = mapgameState;
        saveData.treasureBools = treasureChests;
        saveData.currentMapIndex = mapNavigation.currentMapIndex;
        saveData.playerCurrentHP = player.GetComponent<SpritCharScript>().HP;
    }
    [ContextMenu("JSON save")]
    public void SaveData()
    {
        SaveDataUpdate();
        string filePath = "Assets/Resources/save.json";
        string json = JsonUtility.ToJson(saveData);
        try
        {
            File.WriteAllText(filePath, json);
            Debug.Log($"Saved at {filePath}");
        }
        catch
        {
            Debug.Log("Failed to save.");
        }
    }
    [ContextMenu("JSON Load")]
    public void LoadSaveDate()
    {
        string file = "Assets/Resources/save.json";
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
        }
        else
        {
            Debug.LogError("Save file not found.");
        }
        mapgameState = saveData.mapStates;
        foreach (MapState mapState in mapgameState.mapStates)
        {
            mapState.InitalizeMDictionary();
        }
        treasureChests = saveData.treasureBools;
        for(int index = 0; index < treasureChests.Length; index++)
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
public class SaveData2D
{
    public TwoDMapGameState mapStates;
    public bool[] treasureBools;
    public int currentMapIndex;
    public int playerCurrentHP;
}


