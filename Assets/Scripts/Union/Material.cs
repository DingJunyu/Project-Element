using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
    public bool typeRandom;

    public readonly int capacity;
    public readonly int thisType;
    public readonly float Damage;

    System.Random rand = new System.Random();

    public int setMyType = (int)TypeOfMagic.Type.noneType;

    public Material() {
        capacity = rand.Next(0,50);
        Damage = (float)capacity * (float)rand.Next(16)/10f;

        //範囲チェック付きます、正しいデータを付けた限りセットを有効化にする
        if (typeRandom || setMyType > (int)TypeOfMagic.Type.noneType ||
            setMyType < (int)TypeOfMagic.Type.water)
            thisType = rand.Next(5);
        else
            thisType = setMyType;
    }

    private void Start()
    {

    }
}