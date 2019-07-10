using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour {
    //公的訪問できる部分

    public bool typeRandom;
    public int typeNumber;

    public GameObject textBar;//textBarのデータを保存する
    private GameObject realTextBar;//実際生成したtextBar
    public GameObject rightClickMenu;
    private GameObject realRightClickMenu;

    private GameObject myPack;
    private bool inPack = false;
    public void putInPack() {
        inPack = true;
        transform.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    //多分、パックの中にテンプレートを使って全部有効にするので、
    //テンプレートの部分はすべてのメソッドを無効化にする
    public bool isThisTemplate = false;

    const float mouseOffsetOnX = 45f;
    const float mouseOffsetOnY = -30f;

    private bool canITakeIt;
    public void iCanTakeIt() { canITakeIt = true; }
    public void iCannotTakeIt() { canITakeIt = false; }


    private static readonly string[] saveName = {
        "capacity",
        "thisType",
        "damage",
        "shapeType",
        "stepType",
        "strength",
        "speed",
        "fog"
    };

    public void Save() {
        PlayerPrefs.SetInt(serialNum + saveName[0], capacity);
        PlayerPrefs.SetInt(serialNum + saveName[1], thisType);
        PlayerPrefs.SetFloat(serialNum + saveName[2], damage);
        PlayerPrefs.SetInt(serialNum + saveName[3], shapeType);
        PlayerPrefs.SetInt(serialNum + saveName[4], stepType);
        PlayerPrefs.SetInt(serialNum + saveName[5], strength);
        PlayerPrefs.SetFloat(serialNum + saveName[6], speed);
        if (fog)
            PlayerPrefs.SetInt(serialNum + saveName[7], 1);
    }

    public void Load() {
        capacity = PlayerPrefs.GetInt(serialNum + saveName[0]);
        thisType = PlayerPrefs.GetInt(serialNum + saveName[1]);
        damage = PlayerPrefs.GetFloat(serialNum + saveName[2]);
        shapeType = PlayerPrefs.GetInt(serialNum + saveName[3]);
        stepType = PlayerPrefs.GetInt(serialNum + saveName[4]);
        strength = PlayerPrefs.GetInt(serialNum + saveName[5]);
        speed = PlayerPrefs.GetFloat(serialNum + saveName[6]);
        if (PlayerPrefs.HasKey(serialNum + saveName[7]))
            fog = true;
    }

    public void GiveMeANewSerialCode(string code) {
        PlayerPrefs.DeleteKey(serialNum);
        serialNum = code;
        PlayerPrefs.SetInt(serialNum, (int)GameManager.saveData.material);
        Load();
    }

    public void DeleteThis() {
        for (int i = 0; i < 8; i++) {
            PlayerPrefs.DeleteKey(serialNum + saveName[i]);
        }
    }

    public void CopyFromThis(Material material) {
        this.shapeType = material.shapeType;
    }

    /*属性ダメージ部分*/
    private ColorBox crBox;

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

    /*メソッド部分*/
    public int ReferShapeType() { return shapeType; }
    public int ReferStepType() { return stepType; }
    public int ReferStrength() { return strength; }
    public float ReferSpeed() { return speed; }
    public bool ReferFog() { return fog; }

    public bool choosed;

    public int setMyType;
    public string thisName;

    private string serialNum;
    public string ReferSerialNum() { return serialNum; }

    //乱数を生成する基準
    enum randomStandard {
        better,
        medium,
        weak
    }

    public Material() {
        crBox = new ColorBox();
    }

    private void Awake() {
        capacity = Random.Range(15, 50);
        damage = (float)capacity * Random.Range(0f, 1.6f);

        //範囲チェック付きます、正しいデータを付けた限りセットを有効化にする
        if (typeRandom || setMyType > (int)TypeOfMagic.Type.noneType ||
            setMyType < (int)TypeOfMagic.Type.water)
            thisType = Random.Range(0, 5);
        else
            thisType = setMyType;

        SetShapeAndType();//属性に基づいてデータを生成する

        RandomString randomString = new RandomString();//ランダム文字列生成器
        myPack = GameObject.Find("MyPack");

        serialNum = randomString.GenerateCheckCode32();//シリアル番号を生成する
        while ((PlayerPrefs.HasKey(serialNum))) {//重複チャック
            serialNum = randomString.GenerateCheckCode32();
        }
        PlayerPrefs.SetInt(serialNum, (int)GameManager.saveData.material);//保存データを有効化にする
    }

    private void FixedUpdate() {
        PutThisItemIntoPack();//鞄に入れる
    }

    private void PutThisItemIntoPack() {
        if (canITakeIt && Input.GetKeyDown(KeyCode.E)) {
            if (GameObject.Find("MyPack").GetComponent<ItemPack>().
                InputNew(transform.gameObject)) {
            }
        }
    }

    void SetOneShape(int ranA, int ranB,
        int volA, int volB, int volC) {
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

    void SetShapeName(int str, int randSta) {
        int ranLe = 0;
        int ranRi = 5;

        //ランダムデータを生成する
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

        switch (randSta) {//指定した範囲内でランダムデータを生成する
            case (int)randomStandard.better: ranLe = 12; ranRi = 16; break;
            case (int)randomStandard.medium: ranLe = 8; ranRi = 12; break;
            case (int)randomStandard.weak: ranLe = 4; ranRi = 8; break;
        }

        stepType = str;
        strength = Random.Range(ranLe, ranRi);//強さを生成する
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
        //右クリックメニューを使っていない時に詳細を描画する
        if (!isThisTemplate && realRightClickMenu == default)
            ANewTextBar();
        //右クリックメニューを呼び出したら詳細画面を閉じる
        if (realRightClickMenu != default && realTextBar != default) {
            Destroy(realTextBar);
            realTextBar = default;
        }
        Click();//右クリック事件
    }

    private void ANewTextBar() {
        if (realTextBar != default)//メニューがすでにある時に生成しない
            return;

        realTextBar = textBar;//textBarは空きじゃないので

        //生成したＵＩをrealTextBarに記録する
        realTextBar = Instantiate(realTextBar,
            transform.position, Quaternion.identity);

        realTextBar.gameObject.transform.Find("Name").GetComponent<UnityEngine.UI.Text>().
            text = thisName;
        realTextBar.gameObject.transform.Find("Name").GetComponent<UnityEngine.UI.Text>().
            color = crBox.GetThis(thisType, 0.7f, (int)TypeOfMagic.Type.noneType, 0.3f);
        realTextBar.gameObject.transform.Find("Status").GetComponent<UnityEngine.UI.Text>().
            text = string.Format("効果量：{0:.00}", damage) + " " +
            string.Format("速度 {0:.00}", speed);
        realTextBar.gameObject.transform.Find("Type").GetComponent<UnityEngine.UI.Text>().
            text = TypeOfAttack.Shape.shapeTypeName[shapeType] + " " +
            TypeOfAttack.SpeedAndSteps.stepTypeName[stepType] + " " +
            strength;
    }

    private void OnMouseExit() {//マウスが出る時に詳細画面を消す
        if (!isThisTemplate) {
            Destroy(realTextBar);
            realTextBar = default;
        }
    }

    public void DeleteThisFromPack() {
        myPack.GetComponent<ItemPack>().DeleteAnItem();
    }

    ~Material() {
        if (realTextBar != default)
            Destroy(realTextBar);
    }

    private void Click() {
        if (GetComponent<Rigidbody2D>().isKinematic || inPack)
            if (Input.GetButtonDown("SubFire")) {
                if (realRightClickMenu != default)
                    return;
                //このメニューの親はアイテムではありません
                realRightClickMenu = Instantiate(rightClickMenu, transform);

                Vector3 vector3 = Input.mousePosition;

                vector3.x += mouseOffsetOnX;
                vector3.y += mouseOffsetOnY;

                realRightClickMenu.transform.position = vector3;//正しい座標を設置する
            }
    }
}