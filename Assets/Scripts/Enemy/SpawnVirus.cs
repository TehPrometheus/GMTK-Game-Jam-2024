using System;
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
    private UIManager uiManager;
    public event Action<int> virusCountChanged;
    void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        spawnTime = maxSpawnTime;
        virusCountChanged += uiManager.AdjustSicknessLevel;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0f)
        {
            Instantiate(virus, transform.position + (Vector3)(spawnRadius * UnityEngine.Random.insideUnitCircle), Quaternion.identity);
            spawnTime = maxSpawnTime;
            amountOfVirusses++;
            virusCountChanged?.Invoke(amountOfVirusses);
        }
    }
    public void VirusDied(int points)
    {
        amountOfVirusses--;
        virusCountChanged?.Invoke(amountOfVirusses);
    }
}
