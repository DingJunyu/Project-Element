using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryCollision : MonoBehaviour {
    public GameObject showMeTheKey;
    private GameObject realShowMeTheKey;

    private GameObject parent;
    public GameObject myParent() { return parent; }
    private GameManager gameManager;

    private void Start() {
        parent = transform.parent.gameObject;//親を指定する
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //アイテムとプレーヤーの間の物理演算は行わないため、
    //ここでトリガーを実現しました。
    //private void OnTriggerEnter2D(Collider2D collision) {
    //    if (collision.CompareTag("Player")) {
    //        if (realShowMeTheKey != default)
    //            return;

    //        if (!gameManager.AnObjectHere()) {//一回一個しか取れないので、制限しました
    //            realShowMeTheKey = Instantiate(showMeTheKey,
    //            parent.transform);
    //            parent.GetComponent<Material>().iCanTakeIt();
    //            gameManager.HereHasAnObject();
    //        }
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (realShowMeTheKey != default)
                return;

            if (!gameManager.AnObjectHere()) {//一回一個しか取れないので、制限しました
                realShowMeTheKey = Instantiate(showMeTheKey,
                parent.transform);
                parent.GetComponent<Material>().iCanTakeIt();
                gameManager.HereHasAnObject();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            DestroyUI();
        }
    }

    //消す時に生成したものを消す
    ~TryCollision() {
        DestroyUI();
    }

    private void DestroyUI() {//生成したUIを消す
        if (realShowMeTheKey != default) { 
                Destroy(realShowMeTheKey);
                gameManager.HereHasNoObject();
                parent.GetComponent<Material>().iCannotTakeIt();
        }
    }
}
