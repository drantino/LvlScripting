using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public void Spawn(EnemySO enemyData, int HP)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];
        GameObject tmp = Instantiate(enemyData.Prefab, spawnPoint.position, Quaternion.identity);

        Enemy e = tmp.GetComponent<Enemy>();
        e.HP = HP;
        e.ATK = enemyData.ATK;
        e.DEF = enemyData.DEF;
    }
}
