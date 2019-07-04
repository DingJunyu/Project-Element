using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ここは状態変化の操作しかやりません
public class MyCharacterController : CreatureController
{
    public int maxJumpTime=2;

    // Start is called before the first frame update
    void Start() {
        StandardStart();
    }

    // Update is called once per frame
    void Update() {
        GetOrder();
        StandardUpdate();
    }

    private void FixedUpdate() {
        
    }

    private void GetOrder() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            status = (int)CreatureStatus.moveToRight;
            ChangeDirectOnX(true);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            status = (int)CreatureStatus.moveToLeft;
            ChangeDirectOnX(false);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (CanIJump(maxJumpTime))
                status = (int)CreatureStatus.jump;
        }
    }
}
