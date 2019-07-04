using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//移動できるものの具体的な操作はここでやります。
public class CreatureController : MonoBehaviour
{

    public float speed = 5f;
    public float jumpPower = 300f;

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

    private bool onTheGround = false;
    private bool nextOnTheGround = true;
    protected bool playerMoving = true;
    protected int status = (int)CreatureStatus.none;

    private int jumpCount = 0;
    protected bool CanIJump(int maxJumpTime) { return jumpCount < maxJumpTime; }
    protected bool CanIJump() { return jumpCount < 1; }

    protected void StandardStart() {//継承先に必ず呼び出す！
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        myAnimator.SetBool("OnTheGround", true);
    }

    protected void StandardUpdate() {
        CheckStatus();

        Move();

        SetAnimationStatus();

        ClearStatus();
    }

    public void SetGround() {
        nextOnTheGround = true;
    }

    public void RemoveOnTheGround() {
        onTheGround = false;
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
    }

    private void Move() {
        switch(status){
            case (int)CreatureStatus.moveToRight:
                MoveOnX(true);
                playerMoving = true;
                break;
            case (int)CreatureStatus.moveToLeft:
                MoveOnX(false);
                playerMoving = true;
                break;
            case (int)CreatureStatus.jump:
                Jump(200f);
                playerMoving = false;
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
        if (onTheGround)//床の上に居る限り、減速する
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

        if (oldPos.x != transform.position.x && playerMoving)//横移動チェック
            myAnimator.SetBool("Move", true);
        else
            myAnimator.SetBool("Move", false);

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
    }
}
