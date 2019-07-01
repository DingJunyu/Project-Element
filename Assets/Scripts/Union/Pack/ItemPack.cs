using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPack : MonoBehaviour
{
    /*すべてのアイテムをここに置くか。。。*/
    public GameObject itemTemplate;

    private int itemNum;
    private const int maxTubeNum = 12;
    public bool ReferSpace() { return itemNum < maxTubeNum; }

    private const float nextX = 0.5f;
    private const int MaxOnRow = 6;
    private const float nextY = 0.5f;

    bool isAEmptyPack;//

    GameObject[] testTubes;

    string[] testTubesSerial;

    public ItemPack() {
        testTubes = new GameObject[maxTubeNum];
        testTubesSerial = new string[maxTubeNum];

        isAEmptyPack = true;
    }

    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("MyPack")) isAEmptyPack = false;

        if (!isAEmptyPack) Load();//データがある時読み込む

        itemNum = 2;
        testTubes[0] = Instantiate(itemTemplate, transform);
        testTubes[0].transform.SetParent(transform);
        testTubes[0].transform.localPosition = new Vector3(1f, 0f, 0f);

        testTubes[1] = Instantiate(itemTemplate,
            transform.position, Quaternion.identity);
        testTubes[1].transform.SetParent(transform);
        testTubes[1].transform.localPosition = new Vector3(2f, 0f, 0f);
    }

    // Update is called once per frame
    void Update() {

    }

    private void Save() {
        PlayerPrefs.SetInt("AmountInMyPack", itemNum);

        for (int i = 0; i < itemNum; i++)
        {
            testTubes[i].GetComponent<Material>().Save();//データを保存する

            PlayerPrefs.SetString("ItemInpack" + i, testTubesSerial[i]);//番号を保存する
        }
    }

    private void Load() {
        if (!PlayerPrefs.HasKey("ItemAmountInMyPack"))//ノーデータの時にそのまま終わる
            return;

        itemNum = PlayerPrefs.GetInt("ItemAmountInMyPack", itemNum);
        for (int i = 0; i < itemNum; i++)
        {
            //新しいオブジェクトを生成する
            testTubes[i] = Instantiate(itemTemplate,
                transform);
            //保存されたデータを読み込む
            testTubes[i].GetComponent<Material>().
                GiveMeANewSerialCode(PlayerPrefs.GetString("ItemInpack" + i));
            testTubes[1].transform.localPosition =
                new Vector3((i % MaxOnRow) * nextX, (i / MaxOnRow) * nextY, 0f);
        }
    }

    public void DeletePack() {
        PlayerPrefs.DeleteKey("ItemMyPack");
        for (int i = 0; i < itemNum; i++)
        {
            testTubes[i].GetComponent<Material>().DeleteThis();
        }
        PlayerPrefs.DeleteKey("ItemAmountInMyPack");
    }

    public void Close() {
        gameObject.SetActive(false);
    }

    public void Open() {
        gameObject.SetActive(true);
    }

    public void InputNew() { 

    }

    private void CheckAndSetPos() {
        for (int i = 0; i < itemNum; i++) {
            testTubes[i].transform.localPosition =
               new Vector3((i % MaxOnRow) * nextX, (i / MaxOnRow) * nextY, 0f);
        }
    }
}
