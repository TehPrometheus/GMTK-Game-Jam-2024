using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> spawnLocations = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {

    }

    Vector3 GetRandomSpawnPos()
    {
        return spawnLocations[Random.Range(0, spawnLocations.Count - 1)].position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
