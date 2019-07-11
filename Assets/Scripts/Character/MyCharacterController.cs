using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アニメーションの操作は各子クラスでやる
public class MyCharacterController : CreatureController {
    public int maxJumpTime = 2;

    public GameObject attackCollision;
    private ItemPack myPack;
    private GameObject realAttackCollision;

    protected override void ChildStart() {//startと同様 
        myPack = GameObject.Find("MyPack").GetComponent<ItemPack>();
    }

    protected override void ChildUpdate() {//updateと同様
    }

    protected override void Inif() {
        myAnimator.SetBool("OnTheGround", true);
    }

    protected override void GetOrder() {
        status = (int)CreatureStatus.none;
        //押したキーで状態を渡す

        if (Input.GetButton("Right")) {
            status = (int)CreatureStatus.moveToRight;
            if (!facingRight)
                attacking = false;
            ChangeDirectOnX(true);
        }
        if (Input.GetButton("Left")) {
            status = (int)CreatureStatus.moveToLeft;
            if (facingRight)//攻撃の途中で方向を変更すれば攻撃を止める
                attacking = false;
            ChangeDirectOnX(false);
        }
        if (Input.GetButtonDown("Jump")) {
            if (CanIJump(maxJumpTime))
                status = (int)CreatureStatus.jump;
        }

        if (Input.GetButtonDown("Fire") && !myPack.isPackOpen()) {
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
            if (status == (int)CreatureStatus.jump) {
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
        if (onTheGround) {
            myAnimator.SetBool("Falling", false);
        }

        //床の上に居る限り、移動動画の演出を行う
        if ((status==(int)CreatureStatus.moveToLeft ||
            status == (int)CreatureStatus.moveToRight)  && 
            onTheGround)//横移動チェック
            myAnimator.SetBool("Move", true);
        else
            myAnimator.SetBool("Move", false);

        if (attacking) {
            CheckMyAttacking();
            myAnimator.SetBool("Attack", true);
        }
        else {
            myAnimator.SetBool("Attack", false);
        }
    }

    //Risk Avoidance
    //攻撃は300フレーム以上に続けたら強制終了
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

    protected override void OtherProcessWhenDead() {
        
    }
}
