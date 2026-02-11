using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public List<Enemy> activeEnemies = new List<Enemy>();
    public EnemyDatabase enemyDatabase;
    public void Spawn(int enemyID, int HP)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];
        EnemySO enemySO = enemyDatabase.Get(enemyID);
        GameObject tmp = Instantiate(enemySO.Prefab, spawnPoint.position, Quaternion.identity);

        Enemy e = tmp.GetComponent<Enemy>();
        e.HP = HP;
        activeEnemies.Add(e);
        e.enemyID = enemyID;
        e.ATK = enemySO.ATK;
        e.DEF = enemySO.DEF;
    }
    public void ClearEnemies()
    {
        foreach (Enemy e in activeEnemies)
        {
            Destroy(e.gameObject);
        }
        activeEnemies.Clear();
    }
}
