using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTubePack : MonoBehaviour
{
    private int tubeNum;
    private const int maxTubeNum = 12;
    
    

    // Start is called before the first frame update
    void Start()
    {
        tubeNum = PlayerPrefs.GetInt("tubeNum");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
