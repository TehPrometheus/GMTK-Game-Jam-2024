using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVirus : MonoBehaviour
{
    [Header("Enemy spawn variable")]
    public float spawnRadius;
    public float spawnAmount;
    [Range(0f, 0.5f)]
    public float maxSpawnTime;
    private float spawnTime;
    public GameObject virus;
    public int amountOfVirusses;
    void Start()
    {
        spawnTime = maxSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if(spawnTime <= 0f)
        {
            Instantiate(virus,transform.position+(Vector3)(spawnRadius*Random.insideUnitCircle),Quaternion.identity);
            spawnTime = maxSpawnTime;
            amountOfVirusses++;
        }
    }
    public void VirusDied(int points)
    {
        amountOfVirusses--;
    }
}
