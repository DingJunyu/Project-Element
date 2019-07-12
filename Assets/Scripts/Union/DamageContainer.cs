    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageContainer
{
    public DamageContainer(int tA, float tAD, int tB, float tBD) {
        typeA = tA; typeB = tB;
        typeADam = tAD; typeBDam = tBD;
    }

    public int typeA { get; }
    public int typeB { get; }

    public float typeADam { get; }
    public float typeBDam { get; }
}
