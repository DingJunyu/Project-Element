using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class TestTube : MonoBehaviour
{
    // Data
    private TypeOfMagic Type = new TypeOfMagic();
    private Transform reagent;

    private ColorBox crBox = new ColorBox();

    private float numRemained = 100f;
    private float numUsed = 0f;

    private string serialNum;
    public string ReferSerialNum() { return serialNum; }
    private bool readFromFile = false;

    private bool changed = true;
    private bool used = false;
    public void useThis() { used = true; }

    private int test_FlameCount;//フレームを統計する物
    void CountFlame() {//フレームを統計する(60フレームまで)
        test_FlameCount++;
        test_FlameCount %= 60;
    }

    private void Start()
    {
        RandomString randomString = new RandomString();

        reagent = this.transform.Find("reagent").transform;
        serialNum = randomString.GenerateCheckCode32();
        while ((PlayerPrefs.HasKey(serialNum))) {
            serialNum = randomString.GenerateCheckCode32();
        }
        PlayerPrefs.SetInt(serialNum, 1);
    }

    //読み込むの時に使う
    public void GiveMeANewSerialCode(string SerialNum) {
        if (readFromFile)//読み込むは一回しかできません
            return;
        PlayerPrefs.DeleteKey(serialNum);
        serialNum = SerialNum;
        PlayerPrefs.SetInt(serialNum, 1);
    }

    private void Update()
    {
        CountFlame();
        CheckColor();
        if (changed) { Save(); }
        if (used) { DeleteThis(); }//使い終わったらマネージャーの方で削除してください
    }

    private void CheckColor() {
        if (!changed)
            return;
        reagent.GetComponent<Renderer>().material.color =
           Type.ReferColor();
        changed = false;
    }

    private void RandomColor_Test() {//テスト用関数ーーランダムに色を変更する
        if (test_FlameCount == 0)
            reagent.GetComponent<Renderer>().material.color = crBox.RandColor();
    }

    //データ操作
    void Save() {
        Type.Save(serialNum);
    }

    void Load() {
        Type.Load(serialNum);
    }

    public void DeleteThis() {
        PlayerPrefs.DeleteKey(serialNum);
        Type.Delete(serialNum);
    }

    //ゲーム操作
    public bool InputANewMaterial(Material material) {
        bool success;
        success = Type.InputNewAttribute(material);//新しいものを入れる
        if (success) {
            changed = true;
        }
        return success;
    }
}
