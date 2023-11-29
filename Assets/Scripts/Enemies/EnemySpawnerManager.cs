using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{

    public GameObject enemyToSpawn;
    public List<GameObject> enemies = new List<GameObject>();
    [Tooltip("Keep this number either equal to or lower than the number of spawn points")]
    public int numOfEnemies;
    public List<Transform> spawnPoints = new List<Transform>();
    public List<Transform> usableSpawns = new List<Transform>();


    void Start()
    {
        usableSpawns = spawnPoints;
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < numOfEnemies; i++)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            {
                GameObject spawnedEnemy = Instantiate(enemyToSpawn, usableSpawns[spawnIndex].position, usableSpawns[spawnIndex].rotation);
                enemies.Add(spawnedEnemy);
                usableSpawns.RemoveAt(spawnIndex);

            }
        }
    }
}
