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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
