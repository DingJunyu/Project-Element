using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{

    public float red = 255;
    public float green = 255;
    public float blue = 255;


    // Start is called before the first frame update
    void Start()
    {
        ColorBox crBox = new ColorBox();
        GetComponent<Renderer>().material.color = crBox.SetColorWithRGB(red, green, blue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
