using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class TestTube : MonoBehaviour {
    // Data
    private TypeOfMagic Type;
    private TypeOfAttack AttackType;
    private Transform reagent;

    public GameObject textBar;//textBarのデータを保存する
    private GameObject realTextBar;//実際生成したtextBar

    private ColorBox crBox;

    private float numRemained = 0f;
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

    public TestTube() {
        Type = new TypeOfMagic();
        AttackType = new TypeOfAttack();
        crBox = new ColorBox();
    }

    private void Start() {
        RandomString randomString = new RandomString();

        reagent = this.transform.Find("reagent").transform;
        serialNum = randomString.GenerateCheckCode32();
        while ((PlayerPrefs.HasKey(serialNum))) {
            serialNum = randomString.GenerateCheckCode32();
        }
        PlayerPrefs.SetInt(serialNum, (int)GameManager.saveData.testTube);
    }

    //読み込むの時に使う
    public void GiveMeANewSerialCode(string SerialNum) {
        if (readFromFile)//読み込むは一回しかできません
            return;
        PlayerPrefs.DeleteKey(serialNum);
        serialNum = SerialNum;
        PlayerPrefs.SetInt(serialNum, 1);
    }

    private void Update() {
        CountFlame();
        CheckColor();
        if (changed) { Save(); }
        if (used) { DeleteThis(); }//使い終わったらマネージャーの方で削除してください
        SetReagent();
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
    public void Save() {
        Type.Save(serialNum);
    }

    private void Load() {
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
        if (!success)
            return success;

        numRemained = Type.ReferCapacity();
        AttackType.InputNewMaterials(material);//入れるの場合だけ入る

        return success;
    }

    public void SetSize(float multi) {
        transform.localScale = new Vector3(multi, multi, 1);
    }

    private void OnMouseOver() {
        ANewTextBar();
    }

    private void ANewTextBar() {
        if (realTextBar != default)
            return;

        realTextBar = textBar;//textBarは空きじゃないので

        //生成したＵＩをrealTextBarに記録する
        realTextBar = Instantiate(realTextBar,
            transform.position, Quaternion.identity);

        /*名前部分*/
        realTextBar.gameObject.transform.Find("Name").GetComponent<UnityEngine.UI.Text>().
            text = "試験管";
        realTextBar.gameObject.transform.Find("Name").GetComponent<UnityEngine.UI.Text>().
            color = crBox.GetThis(Type.ReferMax(), 0.7f, (int)TypeOfMagic.Type.noneType,
            0.3f);

        realTextBar.gameObject.transform.Find("Status").GetComponent<UnityEngine.UI.Text>().
            text = TypeOfMagic.name_jp[Type.ReferDamage().typeA] +
            string.Format("{0:.0}", Type.ReferDamage().typeADam) + " " +
            TypeOfMagic.name_jp[Type.ReferDamage().typeB] +
            string.Format("{0:.0}", Type.ReferDamage().typeBDam) + " " +
            string.Format("速度 {0:.0}", AttackType.ReferSpeed());

        realTextBar.gameObject.transform.Find("Type").GetComponent<UnityEngine.UI.Text>().
            text = AttackType.ReferAttackType() +
            (AttackType.ReferFog() ? "霧" : "");

        realTextBar.gameObject.transform.Find("Other").GetComponent<UnityEngine.UI.Text>().
            text = string.Format("{0:.0}/{0:.0}", numRemained - numUsed, numRemained);
    }

    private void OnMouseExit() {
        Destroy(realTextBar);
        realTextBar = default;
    }

    private void SetReagent() {
        float per = Type.ReferCapcityPercentage();
        reagent.localScale = new Vector3(1f, per, 1f);
        //画像が変更されない限り、ここの数字を変更する必要がない
        //y座標:起始座標＋パーセンテージに応じて変化値
        if (per <= 0.2f) {
            reagent.localPosition = new Vector3(0f, -0.16f - (0.2f - per) * 0.2f, 0f);
        }
        else if (per != 1f) {
            reagent.localPosition = new Vector3(0f, -0.005f - (1f - per) * 0.2f, 0f);
        }
        else
            reagent.localPosition = new Vector3(0f, 0f, 0f);
    }

    ~TestTube() {
        if (realTextBar != default)
            Destroy(realTextBar);
    }
}
