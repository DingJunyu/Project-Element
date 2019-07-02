using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnityEngine.EventSystems.EventTrigger))]
public class RightClickMenu : MonoBehaviour, IPointerExitHandler
{
    private int amount = 0;

    public bool outOfMenu = true;

    RectTransform plate;
    int frameCount = 1;
    const int waitFrame = 5;
    bool getNextFrameCount = true;
    void Count() {
        if (!getNextFrameCount)
            return;
        frameCount++;
        if (frameCount == waitFrame)
            getNextFrameCount = false;
    }

    GameObject myRealParent;

    const float width = 100f;
    const float heightPerButton = 40f;
    const float nextButtonY = 30f;
    const float startY = 60f;
    const float defaultHeight = 200f;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;

    // Start is called before the first frame update
    void Awake() {
        //初期化する時に親を指定する
        //この後の初期化によって、親が変更します。
        //初期化順番が変わる可能性もあるが、このscriptは必ずMovableUIの前に置いてください
        myRealParent = transform.parent.gameObject;
        SetPlateSize();
        SetButtonPos();
    }

    private void CountButtonNum() {
        if (button1 != default) amount++;
        if (button2 != default) amount++;
        if (button3 != default) amount++;
        if (button4 != default) amount++;
        if (button5 != default) amount++;
        if (button6 != default) amount++;
        if (button7 != default) amount++;
    }

    private void SetPlateSize() {
        CountButtonNum();
        plate = transform.Find("Plate").GetComponent<RectTransform>();
        plate.sizeDelta = new Vector2(width, amount * heightPerButton);
    }

    private void SetButtonPos() {
        int counter = 0;
        if (button1 != default) SetThisButton(ref button1, counter++);
        if (button2 != default) SetThisButton(ref button2, counter++);
        if (button3 != default) SetThisButton(ref button3, counter++);
        if (button4 != default) SetThisButton(ref button4, counter++);
        if (button5 != default) SetThisButton(ref button5, counter++);
        if (button6 != default) SetThisButton(ref button6, counter++);
        if (button7 != default) SetThisButton(ref button7, counter++);
    }

    private void SetThisButton(ref GameObject thisButton, int num) {
        thisButton = Instantiate(thisButton, transform);
        if (num != 0)
            thisButton.transform.localPosition = new Vector3(0f,
                startY * startY / defaultHeight - nextButtonY * num,
                0f);
        else
            thisButton.transform.localPosition = new Vector3(0f,
                startY * startY / defaultHeight,
                0f);
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnPointerExit(PointerEventData pointData) {
        Destroy(transform.gameObject);
    }

    public void DestoryParent() {
        Destroy(myRealParent);
    }
}
