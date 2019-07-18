using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum saveData {
        material,
        testTube
    }

    Material materialChoosed;
    TestTube testTubeChoosed;

    public GameObject choosedA;
    public GameObject choosedB;
    public void SetChooseA(GameObject gameObject) {
        choosedA = gameObject;
    }

    public void SetChooseB(GameObject gameObject) {
        choosedA = gameObject;
    }
    private bool CanIFusion() {
        return (choosedA != default && choosedB != default);
    }

    public GameObject pack;

    public bool isHereAnObject = false;
    public void HereHasAnObject() { isHereAnObject = true; }
    public void HereHasNoObject() { isHereAnObject = false; }
    public bool AnObjectHere() { return isHereAnObject; }

    public GameObject player;

    /*単独のファイルにまとめる*/
    //必要のは：倉庫(200)、パック(50)、試験管パック(12)

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Click();
        if (CanIFusion())
            Fusion();
    }

    /*調合*/
    private void Fusion() {
        if (choosedB.GetComponent<TestTube>().
            InputANewMaterial(choosedA.GetComponent<Material>())) {
            Destroy(choosedA);//使った素材を消す
            choosedA.GetComponent<Material>().DeleteThisFromPack();
        }
        choosedA = default;//選択を無効化にする
        choosedB = default;//選択を無効化にする
    }

    /******************************************************/
    /*マウス事件*/
    /******************************************************/
    private void Click() {//クリック事件はここで解決
        ChooseOne();
    }


    private void ChooseOne() {//選択
        if (Input.GetButtonDown("Fire")) {
            RaycastHit2D hit =
                Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero);

            if (hit.collider != null) {
                if (hit.collider.gameObject.tag == "Item_Material") {
                    //鞄に居る限り選べます
                    if (!hit.collider.gameObject.GetComponent<Material>().ReferInPack())
                        return;
                    ResetChooseA();//選択状態をリセット
                    choosedA = hit.collider.gameObject;
                    choosedA.GetComponent<OutLineStatus>().SetChoose();
                }

                if (hit.collider.gameObject.tag == "Item_Trigger") {
                    //鞄に居る限り選べます
                    if (!hit.collider.gameObject.GetComponent<TryCollision>().
                        myParent().gameObject.GetComponent<Material>().ReferInPack())
                        return;
                    ResetChooseA();//選択状態をリセット
                    choosedA = hit.collider.gameObject.GetComponent<TryCollision>().
                        myParent().gameObject;
                    choosedA.GetComponent<OutLineStatus>().SetChoose();
                }

                if (hit.collider.gameObject.tag == "tube") {
                    if (choosedA != default)
                        choosedB = hit.collider.gameObject;
                }
            }
            else {//選択をクリア
                ResetChooseA();
                choosedB = default;
            }
        }
    }

    private void ResetChooseA() {
        if (choosedA != default) {
            choosedA.GetComponent<OutLineStatus>().SetUnChoose();
        }
        choosedA = default;
    }
}
