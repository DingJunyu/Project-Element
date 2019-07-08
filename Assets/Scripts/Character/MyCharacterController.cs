using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アニメーションの操作は各子クラスでやる
public class MyCharacterController : CreatureController {
    public int maxJumpTime = 2;

    public GameObject attackCollision;
    private GameObject realAttackCollision;

    protected override void ChildStart() {//startと同様 
    }

    protected override void ChildUpdate() {//updateと同様
    }

    protected override void Inif() {
        myAnimator.SetBool("OnTheGround", true);
    }

    protected override void GetOrder() {
        status = (int)CreatureStatus.none;
        //押したキーで状態を渡す
        if (!AmIAttacking()) {
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
        if (Input.GetKeyDown(KeyCode.A)) {
            if (!AmIAttacking()) {
                LetMeAttack();
                realAttackCollision = Instantiate(attackCollision, transform);
            }
        } 
    }

    protected override void CheckChildStatus() {
        if (!AmIAttacking() && realAttackCollision != default) {
            Destroy(realAttackCollision);
        }
    }

    //********************************************
    //動画関連
    //********************************************
    protected override void SetAnimationStatus() {
        if (!attacking) {
            if (oldPos.y < transform.position.y && status == (int)CreatureStatus.jump) {
                myAnimator.SetBool("OnTheGround", false);
                myAnimator.SetBool("Jump", true);
            }
            if (oldPos.y > transform.position.y && !nextOnTheGround) {
                myAnimator.SetBool("Jump", false);
                myAnimator.SetBool("Falling", true);
            }
        }
        if (nextOnTheGround) {//FootTriggerから信号を受けると床に居る状態を設置する
            myAnimator.SetBool("Falling", false);
            myAnimator.SetBool("OnTheGround", true);
            onTheGround = true;
            nextOnTheGround = false;
        }

        //床の上に居る限り、移動動画の演出を行う
        if (oldPos.x != transform.position.x && onTheGround)//横移動チェック
            myAnimator.SetBool("Move", true);
        else
            myAnimator.SetBool("Move", false);

        if (attacking) {
            CheckMyAttacking();
            ResetForceOnX();
            ResetAllMovingStatus();
            myAnimator.SetBool("Attack", true);
        }
        else {
            myAnimator.SetBool("Attack", false);
        }
    }

    private void CheckMyAttacking() {
        attackFrameCounter++;
        if (attackFrameCounter < maxAttackFrame)
            return;
        myAnimator.SetBool("OnTheGround", true);//動画状態をリセット
        attackFrameCounter = 0;
        attacking = false;
    }

    private void ResetAllMovingStatus() {
        myAnimator.SetBool("OnTheGround", true);
        myAnimator.SetBool("Jump", false);
        myAnimator.SetBool("Falling", false);
        myAnimator.SetBool("Move", false);
    }
}
