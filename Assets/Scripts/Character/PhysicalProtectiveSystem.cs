using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProtectiveSystem : MonoBehaviour {

    float[] defence;

    public PhysicalProtectiveSystem() {
        defence = new float[(int)TypeOfMagic.Type.type_amount];
        for (int i = 0; i < (int)TypeOfMagic.Type.type_amount; i++)
            defence[i] = 0;
    }

    public void InifDefenceContainer(float water,
        float earth, float fire, float wood, float metal, float noneType) {
        defence[(int)TypeOfMagic.Type.water] = water;
        defence[(int)TypeOfMagic.Type.earth] = earth;
        defence[(int)TypeOfMagic.Type.fire] = fire;
        defence[(int)TypeOfMagic.Type.wood] = wood;
        defence[(int)TypeOfMagic.Type.metal] = metal;
        defence[(int)TypeOfMagic.Type.noneType] = noneType;
    }

    public void SetThis(TypeOfMagic.Type type, float amount) {
        defence[(int)type] = amount;
    }

    public void PlusThis(TypeOfMagic.Type type, float amount) {
        defence[(int)type] += amount;
    }

    //バランス調整の時にこちらの計算式に注目！！！
    public float reserveDamage(DamageContainer damage) {
        float damageIReserved = 0;

        damageIReserved = defence[damage.typeA] - damage.typeADam;
        damageIReserved += defence[damage.typeB] - damage.typeBDam;

        /*まだ決めてないけど、この後修正値を渡すかもしれない*/

        return damageIReserved;
    }
}
