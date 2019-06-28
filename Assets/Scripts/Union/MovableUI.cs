using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableUI : MonoBehaviour
{
    public bool moveWithObject = false;
    public bool moveWithMouse = true;

    private Canvas canvas;
    private Camera mainCamera;
    private RectTransform plateMesh;

    private Vector2 size;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        plateMesh = transform.Find("Plate").GetComponent<RectTransform>();
        transform.SetParent(canvas.transform);//UIに所属する

        size = canvas.GetComponent<RectTransform>().sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveWithMouse)
            MoveWithMouse();
    }

    void MoveWithMouse() {
        Vector2 pos;
        Vector2 realVec;


        //座標を計算する
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            Input.mousePosition, null, out pos);

        pos.x += plateMesh.sizeDelta.x / 2;
        pos.y -= plateMesh.sizeDelta.y / 2;

        /*ここの戻り値なんだが、マニュアルんお戻り値と違ってるので一応直しました。*/
        realVec = mainCamera.WorldToScreenPoint(pos);

        if (realVec.x/20 > mainCamera.pixelWidth) { 
            pos.x -= plateMesh.sizeDelta.x;
        }
        if (realVec.y/20 < -mainCamera.pixelHeight/2) {
            pos.y += plateMesh.sizeDelta.y;
        }

        //座標を更新する
        transform.GetComponent<RectTransform>().localPosition =
            pos;
    }
}
