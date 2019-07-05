using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPack : MonoBehaviour
{
    /*すべてのアイテムをここに置くか。。。*/
    public GameObject itemTemplate;

    private SpriteRenderer plateMesh;

    private int itemNum;
    private const int maxItemNum = 12;
    public bool ReferSpace() { return itemNum < maxItemNum; }

    private const float nextX = 0.3f;
    private const int MaxOnRow = 6;
    private const float nextY = 0.3f;
    private const float startX = -1f;
    private const float startY = 0.9f;

    bool isAEmptyPack;//

    GameObject[] itemHere;

    string[] testTubesSerial;

    public ItemPack() {
        itemHere = new GameObject[maxItemNum];
        testTubesSerial = new string[maxItemNum];

        isAEmptyPack = true;
        itemNum = 0;
    }

    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("MyPack")) isAEmptyPack = false;

        if (!isAEmptyPack) Load();//データがある時読み込む

        plateMesh = GetComponent<SpriteRenderer>();

        //itemNum = 2;
        //itemHere[0] = Instantiate(itemTemplate, transform);
        //itemHere[0].transform.SetParent(transform);
        //itemHere[0].transform.localPosition = new Vector3(1f, 0f, 0f);

        //itemHere[1] = Instantiate(itemTemplate,
        //    transform.position, Quaternion.identity);
        //itemHere[1].transform.SetParent(transform);
        //itemHere[1].transform.localPosition = new Vector3(2f, 0f, 0f);
    }

    // Update is called once per frame
    void Update() {

    }

    private void Save() {
        PlayerPrefs.SetInt("AmountInMyPack", itemNum);

        for (int i = 0; i < itemNum; i++)
        {
            itemHere[i].GetComponent<Material>().Save();//データを保存する

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
            itemHere[i] = Instantiate(itemTemplate,
                transform);
            //保存されたデータを読み込む
            itemHere[i].GetComponent<Material>().
                GiveMeANewSerialCode(PlayerPrefs.GetString("ItemInpack" + i));
            itemHere[1].transform.localPosition =
                new Vector3((i % MaxOnRow) * nextX, (i / MaxOnRow) * nextY, 0f);
        }
    }

    public void DeletePack() {
        PlayerPrefs.DeleteKey("ItemMyPack");
        for (int i = 0; i < itemNum; i++)
        {
            itemHere[i].GetComponent<Material>().DeleteThis();
        }
        PlayerPrefs.DeleteKey("ItemAmountInMyPack");
    }

    public void Close() {
        gameObject.SetActive(false);
    }

    public void Open() {
        gameObject.SetActive(true);
    }

    public bool InputNew(GameObject material) {
        int number = 0;
        itemNum++;
        for (int i = 0; i < maxItemNum; i++) {
            if (itemHere[i] == default) {
                number = i; break;
            }
            if (i == maxItemNum - 1) {//空いてるところがない場合
                return false;
            }
        }
        itemHere[number] = material;
        itemHere[number].transform.parent = transform;
        itemHere[number].GetComponent<Rigidbody2D>().isKinematic = true;//rigidbodyを無効化
        itemHere[number].transform.localScale = new Vector2(1f, 1f);

        CheckAndSetPos();

        return true;
    }

    private void CheckAndSetPos() {
        for (int i = 0; i < itemNum; i++) {
            itemHere[i].transform.localPosition =
               new Vector3((i % MaxOnRow) * nextX - plateMesh.size.x / 2.6f,
               (i / MaxOnRow) * nextY + plateMesh.size.y / 2.6f, 0f);
            if (itemHere[i] == null)
                itemHere[i] = default;
        }
    }
}
