﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour {
    ColorBox CrBox;

    public float red = 35f;
    public float green = 155f;
    public float blue = 86f;

    const float pos = 12.5f;

    private GameObject myRealParent;

    private Transform thisFilter;
    private float multiRate;
    const float standardScreenX = 800;

    public BarController() {
        CrBox = new ColorBox();
    }

    private void Awake() {
        myRealParent = transform.parent.parent.gameObject;
    }

    private void Start() {
        SetColor();
        SetSize();
    }

    private void SetColor() {
        transform.GetComponent<Image>().color = 
            CrBox.SetColorWithRGB(red, green, blue);
    }

    private void SetSize() {
        multiRate = Screen.width / standardScreenX * 2;
        transform.parent.transform.localScale = new Vector2(multiRate, multiRate);
    }

    //入力されたデータからＨＰバーのステータスを設置します
    public void SetPercentage(int quant,int maxQuant) {
        transform.localPosition = new Vector2(
            (((float)maxQuant - (float)quant) / (float)maxQuant * -pos) * multiRate/2,
            0);
    }
}
