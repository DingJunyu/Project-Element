using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPack : MonoBehaviour {
    /*すべてのアイテムをここに置くか。。。*/
    public GameObject itemTemplate;

    private SpriteRenderer plateMesh;//鞄の背景

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

    private bool isPackOpened = false;
    public bool isPackOpen() { return isPackOpened; }
    private bool statusChanged = true;

    public ItemPack() {
        itemHere = new GameObject[maxItemNum];
        testTubesSerial = new string[maxItemNum];

        isAEmptyPack = true;
        itemNum = 0;
    }

    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("AmountInMyPack")) isAEmptyPack = false;

        if (!isAEmptyPack) Load();//データがある時読み込む

        plateMesh = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        CheckOrder();
        CheckStatus();
    }

    public void DeleteAnItem() {
        itemNum--;
    }

    private void CheckStatus() {
        gameObject.GetComponent<Renderer>().enabled = isPackOpened;
        for (int i = 0; i < itemNum; i++) {
            if (itemHere[i] != default)
                itemHere[i].gameObject.GetComponent<Renderer>().enabled = isPackOpened;
        }
    }

    private void CheckOrder() {
        if (Input.GetKeyDown(KeyCode.B)) {
            isPackOpened = !isPackOpened;
            statusChanged = true;
        }
    }

    private void Save() {
        DeletePack();
        PlayerPrefs.SetInt("AmountInMyPack", itemNum);

        for (int i = 0; i < itemNum; i++) {
            itemHere[i].GetComponent<Material>().Save();//データを保存する

            PlayerPrefs.SetString("ItemInpack" + i,
                itemHere[i].GetComponent<Material>().ReferSerialNum());//番号を保存する
            PlayerPrefs.SetInt("ItemInPackNum" + i,
                itemHere[i].GetComponent<Material>().typeNumber);
        }
        Debug.Log("Save Success!");
    }

    private void Load() {
        if (!PlayerPrefs.HasKey("AmountInMyPack"))//ノーデータの時にそのまま終わる
            return;

        itemNum = PlayerPrefs.GetInt("AmountInMyPack", itemNum);
        for (int i = 0; i < itemNum; i++) {
            //新しいオブジェクトを生成する
            itemHere[i] = Instantiate(itemTemplate.GetComponent<ItemList>().
                ReferThisItem(PlayerPrefs.GetInt("ItemInPackNum" + i)),
                transform);
            //保存されたデータを読み込む
            itemHere[i].GetComponent<Material>().
                GiveMeANewSerialCode(PlayerPrefs.GetString("ItemInpack" + i));
            //保存されたデータを読み込む
            itemHere[i].GetComponent<Material>().putInPack();//鞄に入れる(rigidbodyを無効化にする
            itemHere[i].transform.localScale = new Vector2(1f, 1f);
            itemHere[i].transform.localPosition =
                 new Vector3((i % MaxOnRow) * nextX - 1.0f,
                 -(i / MaxOnRow) * nextY + 1.0f, -.1f);
        }
        Debug.Log("Load Success!");
    }

    public void DeletePack() {
        PlayerPrefs.DeleteKey("ItemMyPack");
        for (int i = 0; i < itemNum; i++) {
            PlayerPrefs.DeleteKey("ItemInpack" + i);
            PlayerPrefs.DeleteKey("ItemInPackNum" + i);
            itemHere[i].GetComponent<Material>().DeleteThis();//保存したもののデータを全部消す
        }
        PlayerPrefs.DeleteKey("AmountInMyPack");
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

    private void CheckAndSetPos() {//鞄の中の位置を調整する
        for (int i = 0; i < itemNum; i++) {
            itemHere[i].transform.localPosition =
               new Vector3((i % MaxOnRow) * nextX - plateMesh.size.x / 2.6f,
               (i / MaxOnRow) * nextY + plateMesh.size.y / 2.6f, -.1f);
            if (itemHere[i] == null)
                itemHere[i] = default;
        }
    }
}
