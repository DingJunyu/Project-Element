using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
    public bool typeRandom;

    public  int capacity;
    private int thisType;
    public int ReferThisType() { return thisType; }
    public  float Damage;

    public bool choosed;

    public int setMyType;

    private void Start()
    {
        capacity = Random.Range(15, 50);
        Damage = (float)capacity * Random.Range(0f,1.6f);

        //範囲チェック付きます、正しいデータを付けた限りセットを有効化にする
        if (typeRandom || setMyType > (int)TypeOfMagic.Type.noneType ||
            setMyType < (int)TypeOfMagic.Type.water)
            thisType = Random.Range(0,5);
        else
            thisType = setMyType;
    }
}