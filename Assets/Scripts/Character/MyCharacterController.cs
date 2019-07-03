using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : CreatureController
{
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        GetOrder();
    }

    private void GetOrder() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            MoveOnX(true);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            MoveOnX(false);
        }
    }
}
