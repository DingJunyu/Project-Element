using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryCollision : MonoBehaviour {
    public GameObject showMeTheKey;
    private GameObject realShowMeTheKey;

    private GameObject parent;

    private void Start() {
        parent = transform.parent.gameObject;
    }

    //アイテムとプレーヤーの間の物理演算は行わないため、
    //ここでトリガーを実現しました。
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (realShowMeTheKey != default)
                return;

            realShowMeTheKey = Instantiate(showMeTheKey,
                parent.transform);

            parent.GetComponent<Material>().iCanTakeIt();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (realShowMeTheKey != default) { 
                Destroy(realShowMeTheKey);
                parent.GetComponent<Material>().iCannotTakeIt();
            }
        }
    }
}
