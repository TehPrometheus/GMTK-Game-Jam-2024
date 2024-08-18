using System;
using UnityEngine;

public class SpawnVirus : MonoBehaviour
{
    [Header("Enemy spawn variable")]
    public float spawnRadius = 0.57f;
    [Range(0f, 5f)]
    public float maxSpawnTime;
    private float spawnInterval;
    public GameObject enemy;
    public int enemyCapacity = 10; // The amount of enemies a spawner can spawn before they dissapear
    void Start()
    {
        spawnInterval = maxSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval -= Time.deltaTime;
        if (spawnInterval <= 0f)
        {
            SpawnEnemy();
            if (enemyCapacity <= 0)
                Destroy(gameObject);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, transform.position + (Vector3)(spawnRadius * UnityEngine.Random.insideUnitCircle), Quaternion.identity);
        spawnInterval = maxSpawnTime;
        enemyCapacity--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
