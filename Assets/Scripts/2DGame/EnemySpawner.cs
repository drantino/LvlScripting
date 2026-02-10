using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public List<Enemy> activeEnemies = new List<Enemy>();
    public void Spawn(EnemyState enemyState, int HP)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];
        GameObject tmp = Instantiate(enemyState.enemySO.Prefab, spawnPoint.position, Quaternion.identity);

        Enemy e = tmp.GetComponent<Enemy>();
        e.HP = HP;
        e.enemyID = enemyState.EnemyID;
        e.ATK = enemyState.enemySO.ATK;
        e.DEF = enemyState.enemySO.DEF;
    }
}
