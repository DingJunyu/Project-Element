using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TypeOfMagic
{
    public enum Type {
        water,
        earth,
        fire,
        wood,
        metal,
        noneType,
        type_amount,
    }

    public enum MyBool {
        myTrue,
        myFalse,
        myUnknown
    }

    public TypeOfMagic() {
        for (int i = 0; i < (int)Type.type_amount; i++) status[i] = 0f;
        damage = 0;
        capacity = 0;
        crBox = new ColorBox();
        best = new TwoNum();
        targetColor = crBox.GetThis((int)Type.noneType, 1f);
    }

    public TypeOfMagic(int thisType,float Damage,int capacity) {
        for (int i = 0; i < (int)Type.type_amount; i++) status[i] = 0f;
        damage = 0;
        capacity = 0;
        crBox = new ColorBox();
        best = new TwoNum();
        targetColor = crBox.GetThis((int)Type.noneType, 1f);

        status[thisType] += Damage;
        capacity += capacity;

        best = FindMax();
    }

    /*属性状態*/
    float[] status = new float[(int)Type.type_amount];
    int typeCount;
    const int Max_typeCount = 2;//属性数の最大値

    TwoNum best;

    public int ReferMax() { return best.a; }

    /*S&L関連*/
    public static readonly string[] name = 
        { "water", "earth", "fire", "wood", "metal", "none" };
    public static readonly string[] name_jp = {
          "水","土","火","木","金","空"};

    int capacity;
    public int ReferCapacity() { return capacity; }
    const int maxCapacity = 100;
    public bool CanIPutNewResIn() { return capacity < maxCapacity; }

    float damage;//ダメージ

    /*色関係*/
    ColorBox crBox;
    Color targetColor;

    /*属性関係チェック関数,myFalse:相生、myTrue相剋、myUnknown関係なし*/
    void InputRestraint(int a, float damage) {
        if (damage < 0)//関数の安全確認
            return;
        switch (a) {
            case (int)Type.water:
                status[(int)Type.water] += damage;
                status[(int)Type.wood] += 0.25f * damage;
                status[(int)Type.fire] = Minus(status[(int)Type.fire], damage * 0.25f);
                break;
            case (int)Type.earth:
                status[(int)Type.earth] += damage;
                status[(int)Type.metal] += 0.25f * damage;
                status[(int)Type.water] = Minus(status[(int)Type.water], damage * 0.25f);
                break;
            case (int)Type.fire:
                status[(int)Type.fire] += damage;
                status[(int)Type.earth] += 0.25f * damage;
                status[(int)Type.metal] = Minus(status[(int)Type.metal], damage * 0.25f);
                break;
            case (int)Type.wood:
                status[(int)Type.wood] += damage;
                status[(int)Type.fire] += 0.25f * damage;
                status[(int)Type.earth] = Minus(status[(int)Type.earth], damage * 0.25f);
                break;
            case (int)Type.metal:
                status[(int)Type.metal] += damage;
                status[(int)Type.water] += 0.25f * damage;
                status[(int)Type.wood] = Minus(status[(int)Type.wood], damage * 0.25f);
                break;
        }
    }

    float Minus(float now, float target) {
        return (now - target < 0f) ? 0 : now - target;//0より大きい答えを返す
    }

    class TwoNum {
        public int a = (int)Type.noneType;
        public int b = (int)Type.noneType;
    }

    TwoNum FindMax() {
        float max = 0;
        TwoNum mark = new TwoNum();
        for (int i = 0; i < (int)Type.type_amount; i++) {
            if (status[i] > max) {
                max = status[i];
                mark.a = i;
            }
        }
        max = 0;
        for(int i = 0;i < (int)Type.type_amount; i++){
            if (i == mark.a) continue;
            if (status[i] > max) {
                max = status[i];
                mark.b = i;
            }
        }

        return mark;
    }

    void SetColor() {
        targetColor = crBox.GetThis(best.a, status[best.a]/(status[best.a]+status[best.b]),
            best.b, status[best.b] / (status[best.a] + status[best.b]));
    }
    public Color ReferColor() {
        return targetColor;
    }

    //パッケージ化入力
    public bool InputNewAttribute(Material material) {
        float propotion = 1;
        if (capacity >= maxCapacity)
            return false;
        if (material.capacity + capacity >= maxCapacity) {
            propotion = (maxCapacity - capacity) / material.capacity;//もし上限を超えたら
        }

        InputRestraint(material.ReferThisType(), material.damage * propotion);
        capacity += material.capacity;
        if (capacity > maxCapacity)
            capacity = maxCapacity;

        best = FindMax();
        SetColor();

        return true;
    }

    public DamageContainer ReferDamage() {
        DamageContainer answer = new DamageContainer(best.a, status[best.a],
            best.b, status[best.b]);
        return answer;
    }

    public void Save(string code) {
        for (int i = 0; i < (int)Type.type_amount; i++) {
            PlayerPrefs.SetFloat(code + name[i], status[i]);
        }
        PlayerPrefs.SetInt(code + "capacity", capacity);
    }

    public void Load(string code) {
        for (int i = 0; i < (int)Type.type_amount; i++)
        {
            PlayerPrefs.GetFloat(code + name[i], status[i]);
        }
        PlayerPrefs.GetInt(code + "capacity", capacity);
    }

    public void Delete(string code) {
        for (int i = 0; i < (int)Type.type_amount; i++)
        {
            PlayerPrefs.DeleteKey(code + name[i]);
        }
        PlayerPrefs.DeleteKey(code + "capacity");
    }
}
