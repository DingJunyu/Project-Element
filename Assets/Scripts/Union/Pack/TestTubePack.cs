﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTubePack : MonoBehaviour {
    public GameObject testTubeTemplate;

    private int tubeNum;
    private const int maxTubeNum = 12;
    public bool ReferSpace() { return tubeNum < maxTubeNum; }

    private const float nextX = 0.5f;
    private const int MaxOnRow = 6;
    private const float nextY = 0.5f;

    bool isAEmptyPack;//

    GameObject[] testTubes;

    string[] testTubesSerial;

    public TestTubePack() {
        testTubes = new GameObject[maxTubeNum];
        testTubesSerial = new string[maxTubeNum];

        isAEmptyPack = true;
    }

    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("MyPack")) isAEmptyPack = false;

        if (!isAEmptyPack) Load();//データがある時読み込む

        SetANewPack(6);
    }

    // Update is called once per frame
    void Update() {

    }

    private void SetANewPack(int num) {
        for (int i = 0; i < num; i++) {
            //新しいオブジェクトを生成する
            testTubes[i] = Instantiate(testTubeTemplate,
                transform);
            testTubes[i].GetComponent<TestTube>().SetSize(2.5f);
            testTubes[i].transform.localPosition =
                new Vector3((i % MaxOnRow) * nextX, (i / MaxOnRow) * nextY, 0f);
        }
    }

    private void Save() {
        PlayerPrefs.SetInt("AmountInMyPack", tubeNum);

        for (int i = 0; i < tubeNum; i++) {
            testTubes[i].GetComponent<TestTube>().Save();//データを保存する

            PlayerPrefs.SetString("Inpack" + i, testTubesSerial[i]);//番号を保存する
        }
    }

    private void Load() {
        if (!PlayerPrefs.HasKey("AmountInMyPack"))//ノーデータの時にそのまま終わる
            return;

        tubeNum = PlayerPrefs.GetInt("AmountInMyPack", tubeNum);
        for (int i = 0; i < tubeNum; i++) {
            //新しいオブジェクトを生成する
            testTubes[i] = Instantiate(testTubeTemplate,
                transform);
            //保存されたデータを読み込む
            testTubes[i].GetComponent<TestTube>().
                GiveMeANewSerialCode(PlayerPrefs.GetString("Inpack" + i));
            testTubes[i].GetComponent<TestTube>().SetSize(2.5f);
            testTubes[i].transform.localPosition =
                new Vector3((i % MaxOnRow) * nextX, (i / MaxOnRow) * nextY, 0f);
        }
    }

    public void DeletePack() {
        PlayerPrefs.DeleteKey("MyPack");
        for (int i = 0; i < tubeNum; i++) {
            testTubes[i].GetComponent<TestTube>().DeleteThis();
        }
        PlayerPrefs.DeleteKey("AmountInMyPack");
    }

    public void Close() {
        gameObject.SetActive(false);
    }

    public void Open() {
        gameObject.SetActive(true);
    }

    public void InputNew() {

    }
}
