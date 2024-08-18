using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    // Levels
    [Header("Levels")]
    [Range(0,10)]
    public int gluttonyLevel;
    [Range(0, 10)]
    public int speedLevel;
    [Range(0, 10)]
    public int immunityLevel;
    [Range(0, 10)]
    public int dashLevel;

    
    
    // Resources
    [Header("Resources")]
    [Range(0, 100)]
    [SerializeField] int gluttonyResources;
    [Range(0, 100)]
    [SerializeField] int speedResources;
    [Range(0, 100)]
    [SerializeField] int immunityResources;
    [Range(0, 100)]
    [SerializeField] int dashResources;
    [Range(0, 100)]
    public int resourcesNeededToLevel = 100;
    public int maxLevel = 10;



    public enum ResourceType{
        gluttony,
        speed, 
        immunity, 
        dash
    }



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
        AddResource(ResourceType.gluttony, resources[0]);
        AddResource(ResourceType.speed, resources[1]);
        AddResource(ResourceType.immunity, resources[2]);
        AddResource(ResourceType.dash, resources[3]);
        
        
    }
    public void AddResource(ResourceType resourceType, int resource)
    {
        if(resourceType == ResourceType.gluttony && gluttonyLevel<maxLevel)
        {
            gluttonyResources += resource;
            if(gluttonyResources >= resourcesNeededToLevel)
            {
                gluttonyLevel++;
                gluttonyResources -= resourcesNeededToLevel;
        
            }
           
        }
        else if (resourceType == ResourceType.speed && speedLevel < maxLevel)
        {
            speedResources += resource;
            if(speedResources >= resourcesNeededToLevel)
            {
                speedLevel++;
                speedResources -= resourcesNeededToLevel;

            }
        }
        else if (resourceType == ResourceType.immunity && immunityLevel < maxLevel)
        {
            immunityResources += resource;
            if (immunityResources >= resourcesNeededToLevel)
            {
                immunityLevel++;
                immunityResources -= resourcesNeededToLevel;

            }
        }
        else if (resourceType == ResourceType.dash && dashLevel < maxLevel)
        {
            dashResources += resource;
            if (dashResources >= resourcesNeededToLevel)
            {
                dashLevel++;
                dashResources -= resourcesNeededToLevel;

            }
        }
        


    }
}
