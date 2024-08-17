using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    // Levels
    [Range(0,10)]
    [SerializeField] int gluttonyLevel;
    [Range(0, 10)]
    [SerializeField] int speedLevel;
    [Range(0, 10)]
    [SerializeField] int immunityLevel;
    [Range(0, 10)]
    [SerializeField] int dashLevel;

    // Resources
    [Range(0, 100)]
    [SerializeField] int gluttonyResources;
    [Range(0, 100)]
    [SerializeField] int speedResources;
    [Range(0, 100)]
    [SerializeField] int immunityResources;
    [Range(0, 100)]
    [SerializeField] int dashResources;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddResources(int[] resources)
    {
        gluttonyResources += resources[0];
        speedResources += resources[1];
        immunityResources += resources[2];
        dashResources += resources[3];
        
    }
}
