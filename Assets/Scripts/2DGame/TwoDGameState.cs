using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TwoDGameState : MonoBehaviour
{
    public static TwoDGameState Instance;
    public List<MapState> mapStates = new List<MapState>();
    public Transform mapParent;
    private EnemySpawner spawner;
    private int currentMapID;
    private MapState currentMapState;
    private void Start()
    {
        foreach (MapState mapState in mapStates)
        {
            mapState.InitalizeMDictionary();
        }
        InitializeMap(0);
    }
    public void InitializeMap(int mapID_)
    {
        MapState targetMap = null;
        foreach (MapState mapState in mapStates)
        {
            if (mapState.mapID == mapID_)
            {
                targetMap = mapState;
                BeginEnemySpawn(targetMap);
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
                spawner.Spawn(enemy, enemy.currentHP);
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
}

[Serializable]
public class MapState
{
    public int mapID;
    public List<EnemyState> enemyStates;
    public Dictionary<int, EnemyState> enemyDictionary;
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
    public EnemySO enemySO;
}
