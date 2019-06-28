using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
    public bool typeRandom;

    public GameObject textBar;//textBarのデータを保存する
    private GameObject realTextBar;//実際生成したtextBar

    private ColorBox crBox;

    /*属性ダメージ部分*/
    public int capacity;
    private int thisType;
    public int ReferThisType() { return thisType; }
    public float damage;

    /*ダメージ形部分*/
    private int shapeType;
    private int stepType;
    private int strength;
    private float speed;
    private bool fog = false;

    public int ReferShapeType() { return shapeType; }
    public int ReferStepType() { return stepType; }
    public int ReferStrength() { return strength; }
    public float ReferSpeed() { return speed; }
    public bool ReferFog() { return fog; }

    public bool choosed;

    public int setMyType;
    public string thisName;

    enum randomStandard {
        better,
        medium,
        weak
    }

    public Material() {
        crBox = new ColorBox();
    }

    private void Start() {
        capacity = Random.Range(15, 50);
        damage = (float)capacity * Random.Range(0f,1.6f);

        //範囲チェック付きます、正しいデータを付けた限りセットを有効化にする
        if (typeRandom || setMyType > (int)TypeOfMagic.Type.noneType ||
            setMyType < (int)TypeOfMagic.Type.water)
            thisType = Random.Range(0,5);
        else
            thisType = setMyType;

        SetShapeAndType();//属性に基づいてデータを生成する
    }

    void SetOneShape(int ranA,int ranB,
        int volA,int volB,int volC) {
        int randNum = Random.Range(0, 100);
        /*ここの数字はすべて確率であり*/
        if (randNum < ranA) {
            SetShapeName((int)TypeOfAttack.Shape.ShapeType.rayOfLight,
                volA);
        }
        else if (randNum < ranB) {
            SetShapeName((int)TypeOfAttack.Shape.ShapeType.radialLight,
                volB);
        }
        else {
            SetShapeName((int)TypeOfAttack.Shape.ShapeType.annulus,
                volC);
        }
    }

    void SetShapeName(int str,int randSta) {
        int ranLe = 0;
        int ranRi = 5;

        switch (randSta) {
            case (int)randomStandard.better: ranLe = 12; ranRi = 16; break;
            case (int)randomStandard.medium: ranLe = 8; ranRi = 12; break;
            case (int)randomStandard.weak: ranLe = 4; ranRi = 8; break;
        }

        shapeType = str;
        strength = Random.Range(ranLe, ranRi);
    }

    void SetOneStep(int ranA, int ranB,
       int volA, int volB, int volC) {
        int randNum = Random.Range(0, 100);
        /*ここの数字はすべて確率であり*/
        if (randNum < ranA) {
            SetTypeName((int)TypeOfAttack.Shape.ShapeType.rayOfLight,
                volA);
        }
        else if (randNum < ranB) {
            SetTypeName((int)TypeOfAttack.Shape.ShapeType.radialLight,
                volB);
        }
        else {
            SetTypeName((int)TypeOfAttack.Shape.ShapeType.annulus,
                volC);
        }
    }


    void SetTypeName(int str, int randSta) {
        int ranLe = 0;
        int ranRi = 5;

        switch (randSta) {
            case (int)randomStandard.better: ranLe = 12; ranRi = 16; break;
            case (int)randomStandard.medium: ranLe = 8; ranRi = 12; break;
            case (int)randomStandard.weak: ranLe = 4; ranRi = 8; break;
        }

        stepType = str;
        strength = Random.Range(ranLe, ranRi);
    }

    private void SetShapeAndType() {
        switch (thisType) {
            case (int)TypeOfMagic.Type.water: ANewWaterMaterial(); break;
            case (int)TypeOfMagic.Type.wood: ANewWoodMaterial(); break;
            case (int)TypeOfMagic.Type.fire: ANewFireMaterial(); break;
            case (int)TypeOfMagic.Type.metal: ANewMetalMaterial(); break;
            case (int)TypeOfMagic.Type.earth: ANewEarthMaterial(); break;
        }
    }

    private void ANewWaterMaterial() {//水タイプの初期化
        speed = Random.Range(0.8f, 1.4f);

        SetOneShape(20, 40, (int)randomStandard.weak,
             (int)randomStandard.medium,
             (int)randomStandard.better);
        SetOneStep(10, 30, (int)randomStandard.weak,
             (int)randomStandard.medium,
             (int)randomStandard.better);

        if (Random.Range(0, 100) > 40)
            fog = true;
    }

    private void ANewWoodMaterial() {//木タイプの初期化
        speed = Random.Range(0.8f, 1.4f);

        SetOneShape(50, 90, (int)randomStandard.better,
             (int)randomStandard.medium,
             (int)randomStandard.weak);
        SetOneStep(70, 90, (int)randomStandard.better,
             (int)randomStandard.weak,
             (int)randomStandard.weak);
    
    }

    private void ANewFireMaterial() {//火タイプの初期化
        speed = Random.Range(0.8f, 1.4f);

        SetOneShape(20, 80, (int)randomStandard.weak,
           (int)randomStandard.better,
           (int)randomStandard.weak);
        SetOneStep(80, 100, (int)randomStandard.better,
             (int)randomStandard.weak,
             (int)randomStandard.weak);

        if (Random.Range(0, 100) > 80)
            fog = true;
    }

    private void ANewMetalMaterial() {//金タイプの初期化
        speed = Random.Range(0.8f, 1.4f);

        SetOneShape(60, 90, (int)randomStandard.better,
           (int)randomStandard.medium,
           (int)randomStandard.weak);
        SetOneStep(80, 100, (int)randomStandard.better,
             (int)randomStandard.weak,
             (int)randomStandard.weak);
    }

    private void ANewEarthMaterial() {//土タイプの初期化
        speed = Random.Range(0.8f, 1.4f);

        SetOneShape(30, 60, (int)randomStandard.medium,
           (int)randomStandard.medium,
           (int)randomStandard.better);
        SetOneStep(30, 90, (int)randomStandard.medium,
             (int)randomStandard.better,
             (int)randomStandard.weak);
    }

    private void OnMouseOver() {
        if (realTextBar != default)
            return;

        realTextBar = textBar;//textBarは空きじゃないので

        //生成したＵＩをrealTextBarに記録する
        realTextBar = Instantiate(realTextBar, 
            transform.position, Quaternion.identity);

        realTextBar.gameObject.transform.Find("Name").GetComponent<UnityEngine.UI.Text>().
            text = thisName;
        realTextBar.gameObject.transform.Find("Name").GetComponent<UnityEngine.UI.Text>().
            color = crBox.GetThis(thisType, 0.7f,(int)TypeOfMagic.Type.noneType,0.3f);
        realTextBar.gameObject.transform.Find("Status").GetComponent<UnityEngine.UI.Text>().
            text = string.Format("効果量：{0:.00}", damage) + " " +
            string.Format("速度 {0:.00}", speed);
        realTextBar.gameObject.transform.Find("Type").GetComponent<UnityEngine.UI.Text>().
            text = TypeOfAttack.Shape.shapeTypeName[shapeType] + " " +
            TypeOfAttack.SpeedAndSteps.stepTypeName[stepType] + " " +
            strength ;
    }

    private void OnMouseExit() {
        Destroy(realTextBar);
        realTextBar = default;  
    }
}