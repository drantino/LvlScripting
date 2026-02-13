using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Scriptable Objects/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    public List<EnemySpawnData> enemies;
    private Dictionary<int, EnemySO> lookUp;
    public void BuildookUp()
    {
        lookUp = new Dictionary<int, EnemySO>();
        foreach (EnemySpawnData enemy in enemies)
        {
            lookUp.Add(enemy.enemyID, enemy.enemySO);
        }
    }
    public EnemySO Get(int ID)
    {
        if (lookUp == null)
        {
            BuildookUp();
        }
        return lookUp[ID];
    }

}
[Serializable]
public class EnemySpawnData
{
    public EnemySO enemySO;
    public int enemyID;
}
