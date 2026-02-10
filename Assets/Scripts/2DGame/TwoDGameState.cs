using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TwoDGameState : MonoBehaviour
{
    public List<MapState> mapStates = new List<MapState>();
    public Transform mapParent;
    private EnemySpawner spawner;
    public void InitializeMap(int mapID_)
    {
        MapState targetMap = null;
        foreach(MapState mapState in mapStates)
        {
            if(mapState.mapID == mapID_)
            {
                targetMap = mapState;
                BeginEnemySpawn(targetMap);
                break;
            }
        }
    }
    public void BeginEnemySpawn(MapState map)
    {
        spawner = mapParent.GetComponent<EnemySpawner>();
        foreach(EnemyState enemy in map.enemyStates)
        {
            spawner.Spawn(enemy.enemySO, enemy.currentHP);
        }
    }
}

[Serializable]
public class MapState
{
    public int mapID;
    public List<EnemyState> enemyStates;
}

[Serializable]
public class EnemyState
{
    public int EnemyID;
    public int currentHP;
    public EnemySO enemySO;
}
