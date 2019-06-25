using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTube : MonoBehaviour
{
    // Data
    private TypeOfMagic Type = new TypeOfMagic();
    private Transform reagent;

    private ColorBox crBox = new ColorBox();

    private float numRemained = 100f;
    private float numUsed = 0f;

    private int test_FlameCount;//フレームを統計する物
    void CountFlame() {//フレームを統計する(60フレームまで)
        test_FlameCount++;
        test_FlameCount %= 60;
    }

    private void Start()
    {
        reagent = this.transform.Find("reagent").transform;
    }

    private void Update()
    {
        CountFlame();
        //if (test_FlameCount == 0)
        //    reagent.GetComponent<Renderer>().material.color = crBox.RandColor();
        reagent.GetComponent<Renderer>().material.color =
            crBox.GetThis((int)TypeOfMagic.Type.fire, 0.6f, (int)TypeOfMagic.Type.metal, 0.4f);
    }
}
