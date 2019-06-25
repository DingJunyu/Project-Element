using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorBox
{
    public ColorBox() {
        rand = new System.Random();
        standardCr = new Color[6];
        SetColor();
    }

    System.Random rand;

    Color[] standardCr;

    public Color RandColor() {
        return new Color(GetCrNum((float)rand.Next(255)),
            GetCrNum((float)rand.Next(255)),
            GetCrNum((float)rand.Next(255)));
    }

    public Color SetColorWithRGB(float r,float g,float b) {
        return new Color(GetCrNum(r),
            GetCrNum(g),
            GetCrNum(b));
    }

    public Color GetThis(int typeA, float pA)
    {
        Color color = new Color();

        color = standardCr[typeA] * pA;

        return color;
    }

    public Color GetThis(int typeA, float pA, int typeB, float pB) {
        Color color = new Color();

        color = standardCr[typeA] * pA + standardCr[typeB] * pB;

        return color;
    }

    float GetCrNum(float target) {
        return target / 255f;
    }

    void SetColor() {
        standardCr[(int)TypeOfMagic.Type.earth] = new Color(GetCrNum(202f), GetCrNum(111f), GetCrNum(30f));
        standardCr[(int)TypeOfMagic.Type.water] = new Color(GetCrNum(52f), GetCrNum(152f), GetCrNum(219f));
        standardCr[(int)TypeOfMagic.Type.fire] = new Color(GetCrNum(231f), GetCrNum(76f), GetCrNum(60f));
        standardCr[(int)TypeOfMagic.Type.metal] = new Color(GetCrNum(40f), GetCrNum(55f), GetCrNum(71f));
        standardCr[(int)TypeOfMagic.Type.wood] = new Color(GetCrNum(88f), GetCrNum(214f), GetCrNum(141f));
        standardCr[(int)TypeOfMagic.Type.noneType] = new Color(GetCrNum(255f), GetCrNum(255f), GetCrNum(255f));
    }
}
