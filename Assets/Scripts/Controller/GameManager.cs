using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum saveData { 
        material,
        testTube
    }

    Material materialChoosed;
    TestTube testTubeChoosed;

    public GameObject choosedA;
    public GameObject choosedB;

    public GameObject pack;

    /*単独のファイルにまとめる*/
    //必要のは：倉庫(200)、パック(50)、試験管パック(12)

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Click();
        if (CanIFusion())
            Fusion();
    }

    public void SetChooseA(GameObject gameObject) {
        choosedA = gameObject;
    }

    public void SetChooseB(GameObject gameObject) {
        choosedA = gameObject;
    }

    private bool CanIFusion() {
        return (choosedA != default && choosedB != default);
    }

    private void Fusion() {
        if (choosedB.GetComponent<TestTube>().
            InputANewMaterial(choosedA.GetComponent<Material>()))
            Destroy(choosedA);//使った素材を消す
        choosedA = default;//選択を無効化にする
        choosedB = default;//選択を無効化にする
    }

    /******************************************************/
    /*マウス事件*/
    /******************************************************/
    private void Click() {//クリック事件はここで解決
        PutMaterialIntoTube();
    }

    private void PutMaterialIntoTube() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = 
                Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero);

            if (hit.collider != null) {
                if (hit.collider.gameObject.tag == "Item_Material"){
                    choosedA = hit.collider.gameObject;
                }
                if (hit.collider.gameObject.tag == "tube") {
                    if (choosedA != default) 
                    choosedB = hit.collider.gameObject;
                }
            }
            else{//選択をクリア
                choosedA = default;
                choosedB = default;
            }
        }
    }
}
