using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] bool spawn;
    [SerializeField] bool spawned;

    // Update is called once per frame
    void Update()
    {
        if (spawn && ! spawned)
        {
            spawned = true;
            SpawnObj();
        }
    }

    public void SpawnObj()
    {

    }
}
