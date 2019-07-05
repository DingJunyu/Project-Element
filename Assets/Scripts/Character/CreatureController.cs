using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//移動できるものの具体的な操作はここでやります。
public class CreatureController : MonoBehaviour {

    public float speed = 5f;
    public float jumpPower = 200f;

    protected enum CreatureStatus {
        none,
        moveToRight,
        moveToLeft,
        jump,
        falling
    }

    protected bool facingRight;
    protected Rigidbody2D myRigidbody;

    protected Vector3 oldPos;
    protected Animator myAnimator;

    /*移動関連*/
    private bool onTheGround = false;
    private bool nextOnTheGround = true;
    protected bool playerMoving = true;
    protected int status = (int)CreatureStatus.none;
    public void SetGround() {
        nextOnTheGround = true;
    }

    public void RemoveOnTheGround() {
        onTheGround = false;
    }

    private int jumpCount = 0;//すでにジャンプした回数（床に戻るとリセット）
    protected bool CanIJump(int maxJumpTime) { return jumpCount < maxJumpTime; }
    protected bool CanIJump() { return jumpCount < 1; }//基本は一回しかジャンプできない

    /*攻撃関連*/
    public bool unMovable = false;//これはattackingでコントロールする
    public bool attacking;//外に渡す入口
    protected void LetMeAttack() { attacking = true; }
    protected void StopAttacking() { attacking = false; }
    protected bool AmIAttacking() { return attacking; }
    int attackFrameCounter = 0;
    const int maxAttackFrame = 300;
    private void CheckMyAttacking() {
        attackFrameCounter++;
        if (attackFrameCounter < maxAttackFrame)
            return;
        myAnimator.SetBool("OnTheGround", false);//動画状態をリセット
        attackFrameCounter = 0;
        attacking = false;
    }

    /*標準初期化*/
    protected void StandardStart() {//継承先に必ず呼び出す！
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        myAnimator.SetBool("OnTheGround", true);
    }

    /*標準Update*/
    protected void StandardUpdate() {
        CheckStatus();//状態更新

        Move();//移動

        SetAnimationStatus();//動画状態更新

        ClearStatus();//後始末
    }

    //*********************************************
    //移動関連
    //*********************************************
    private void CheckStatus() {
        if (!Enum.IsDefined(typeof(CreatureStatus), status)) {
            status = (int)CreatureStatus.none;
        }
        if (onTheGround)
            jumpCount = 0;

        unMovable = attacking;//攻撃状態による移動動画の状態を変化
        if (attacking)
            CheckMyAttacking();
    }

    private void Move() {
        switch (status) {
            case (int)CreatureStatus.moveToRight:
                MoveOnX(true);
                playerMoving = true;
                break;
            case (int)CreatureStatus.moveToLeft:
                MoveOnX(false);
                playerMoving = true;
                break;
            case (int)CreatureStatus.jump:
                Jump(jumpPower);
                onTheGround = false;//FootTriggerではバッグが出る可能性がある
                nextOnTheGround = false;
                jumpCount++;
                break;
            case (int)CreatureStatus.none:
                ResetForceOnX();
                break;
        }
    }

    private void ClearStatus() {
        if (oldPos.y == transform.position.y)
            nextOnTheGround = false;
        oldPos = transform.position;
        status = (int)CreatureStatus.none;
    }

    protected void Jump(float strength) {
        myRigidbody.AddForce(new Vector2(0, strength));
    }

    protected void MoveOnX(bool right) {
        myRigidbody.velocity =
            new Vector2((right ? speed : -speed),
            myRigidbody.velocity.y);
    }

    protected void ResetForceOnX() {
        if (onTheGround || attacking)//床の上に居る限り、減速する
            myRigidbody.velocity = new Vector2(0,
                myRigidbody.velocity.y);
    }

    protected void ChangeDirectOnX(bool right) {
        Vector2 theScale = transform.localScale;

        theScale.x = right ? 1 : -1;
        transform.localScale = theScale;
    }

    //********************************************
    //動画関連
    //********************************************
    private void SetAnimationStatus() {

        if (oldPos.y < transform.position.y && status == (int)CreatureStatus.jump) {
            myAnimator.SetBool("OnTheGround", false);
            myAnimator.SetBool("Jump", true);
        }
        if (oldPos.y > transform.position.y && !nextOnTheGround) {
            myAnimator.SetBool("Jump", false);
            myAnimator.SetBool("Falling", true);
        }
        if (nextOnTheGround) {//FootTriggerから信号を受けると床に居る状態を設置する
            myAnimator.SetBool("Falling", false);
            myAnimator.SetBool("OnTheGround", true);
            onTheGround = true;
        }

        //床の上に居る限り、移動動画の演出を行う
        if (oldPos.x != transform.position.x && onTheGround)//横移動チェック
            myAnimator.SetBool("Move", true);
        else
            myAnimator.SetBool("Move", false);

        if (attacking) {
            ResetForceOnX();
            ResetAllMovingStatus();
            myAnimator.SetBool("Attack", true);
        }
        else {
            myAnimator.SetBool("Attack", false);
        }
    }

    private void ResetAllMovingStatus() {
        myAnimator.SetBool("OnTheGround", false);
        myAnimator.SetBool("Jump", false);
        myAnimator.SetBool("Falling", false);
        myAnimator.SetBool("Move", false);
    }
}
