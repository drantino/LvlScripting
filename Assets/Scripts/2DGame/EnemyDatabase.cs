using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Scriptable Objects/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    public List<EnemySO> enemies;
    private Dictionary<int, EnemySO> lookUp;
    public void BuildookUp()
    {
        lookUp = new Dictionary<int, EnemySO>();
        foreach (EnemySO enemy in enemies)
        {
            lookUp.Add(enemy.enemyID, enemy);

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
