using System.Collections;
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

    public BarController() {
        CrBox = new ColorBox();
    }

    private void Awake() {
        myRealParent = transform.parent.parent.gameObject;
    }

    private void Start() {
        SetColor();
    }

    private void SetColor() {
        transform.GetComponent<Image>().color = 
            CrBox.SetColorWithRGB(red, green, blue);
    }

    //入力されたデータからＨＰバーのステータスを設置します
    public void SetPercentage(int quant,int maxQuant) {
        transform.localPosition = new Vector2(
            ((float)maxQuant - (float)quant) / (float)maxQuant * -pos,
            0);
    }
}
