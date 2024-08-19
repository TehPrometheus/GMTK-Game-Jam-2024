using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> spawnLocationList;
    public List<GameObject> spawnersList;
    public float spawnInterval = 20f;
    private float accSec = 0f;
    private int spawnCount = 0;
    Vector3 GetRandomSpawnPos()
    {
        return spawnLocationList[Random.Range(0, spawnLocationList.Count - 1)].position;
    }

    GameObject GetRandomSpawner()
    {
        return spawnersList[Random.Range(0, spawnersList.Count - 1)];
    }

    void SpawnEnemySpawner()
    {
        Instantiate(GetRandomSpawner(), GetRandomSpawnPos(), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        accSec += Time.deltaTime;

        if (accSec > spawnInterval)
        {
            SpawnEnemySpawner();
            accSec = 0f;
            spawnCount++;
            if (spawnInterval > 12f && (spawnCount % 2 == 0))
                spawnInterval--;
        }
    }
}
