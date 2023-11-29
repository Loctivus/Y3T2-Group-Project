using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Door : Interactable
{
    public VoidEventChannelSO eventChannel;

    [SerializeField] MeshRenderer[] mr;
    BoxCollider col;


    public GameObject enemyToSpawn;
    //public List<GameObject> enemies = new List<GameObject>();

    [Tooltip("Keep this number either equal to or lower than the number of spawn points")]
    public int numOfEnemies;
    public List<Transform> spawnPoints = new List<Transform>();
    public List<Transform> usableSpawns = new List<Transform>();

    void Start()
    {
        
    }


    private void Awake()
    {
        usableSpawns = spawnPoints.ToList();
       
        //mr = GetComponent<MeshRenderer>();
        col = GetComponent<BoxCollider>();
    }

    public override void Interacted()
    {
        SpawnEnemies();
        DoorOpened();
    }

    void DoorOpened()
    {

        foreach (MeshRenderer m in mr)
        {
            m.enabled = false;
        }
        col.enabled = false;
    }

    void DoorClosed()
    {

        foreach (MeshRenderer m in mr)
        {
            m.enabled = true;
        }
        col.enabled = true;
        usableSpawns = spawnPoints.ToList();
       
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < numOfEnemies; i++)
        {
            int spawnIndex = Random.Range(0, usableSpawns.Count);
            {
                GameObject spawnedEnemy = Instantiate(enemyToSpawn, usableSpawns[spawnIndex].position, usableSpawns[spawnIndex].rotation);
                //enemies.Add(spawnedEnemy);
                usableSpawns.RemoveAt(spawnIndex);
                StartCoroutine(ResetCounter());

            }
        }
    }


    IEnumerator ResetCounter()
    {
        yield return new WaitForSeconds(10f);
        DoorClosed();
    }

}
