using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TwoDGameState : MonoBehaviour
{
    public static TwoDGameState Instance;
    //public List<MapState> mapStates = new List<MapState>();
    public TwoDMapGameState mapgameState;
    public Transform mapParent;
    private EnemySpawner spawner;
    private int currentMapID;
    private MapState currentMapState;

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
        foreach (EnemyState enemy in map.enemyStates)
        {
            if (enemy.currentHP > 0)
            {
                spawner.Spawn(enemy.EnemyID, enemy.currentHP);
            }

        }
    }
    public void RestEnemies()
    {
        foreach(MapState m in mapgameState.mapStates)
        {
            foreach(EnemyState e in m.enemyStates)
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
            foreach(Enemy enemy in enemies)
            {
                currentMapState.enemyDictionary[enemy.enemyID].currentHP = enemy.HP;
            }
        }

    }
    public void TreasureGet(int index)
    {
        treasureChests[index] = true;
    }
    public void SaveDataUpdate()
    {
        saveData = new SaveData2D();
        saveData.mapStates = mapgameState;
        saveData.treasureBools = treasureChests;
    }
}

[Serializable]
public class MapState
{
    public int mapID;
    public List<EnemyState> enemyStates;
    [NonSerialized]public Dictionary<int, EnemyState> enemyDictionary;
    public void InitalizeMDictionary()
    {
        enemyDictionary = new Dictionary<int, EnemyState>();
        foreach(EnemyState enemy in enemyStates)
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
}


