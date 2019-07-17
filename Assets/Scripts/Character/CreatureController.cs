using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//移動できるものの具体的な操作はここでやります。
public abstract class CreatureController : MonoBehaviour {

    public float speed = 5f;
    public float jumpPower = 250f;

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
    protected bool onTheGround = false;
    protected bool nextOnTheGround = true;
    protected bool playerMoving = true;
    protected int status = (int)CreatureStatus.none;
    public void SetGround() {
        nextOnTheGround = true;
    }

    public void RemoveOnTheGround() {
        onTheGround = false;
    }

    protected int jumpCount = 0;//すでにジャンプした回数（床に戻るとリセット）
    protected bool CanIJump(int maxJumpTime) { return jumpCount < maxJumpTime; }
    protected bool CanIJump() { return jumpCount < 1; }//基本は一回しかジャンプできない

    /*攻撃関連*/
    public bool attacking;//外に渡す入口
    protected void LetMeAttack() { attacking = true; }
    protected void StopAttacking() { attacking = false; }
    protected bool AmIAttacking() { return attacking; }
    protected int attackFrameCounter = 0;
    protected const int maxAttackFrame = 300;

    private void Start() {
        StandardStart();
        ChildStart();
    }

    /*標準初期化*/
    protected void StandardStart() {//継承先に必ず呼び出す！
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        Inif();
    }

    protected abstract void ChildStart();

    private void Update() {
        StandardUpdate();
        ChildUpdate();
        StandardLateUpdate();
    }

    /*標準Update*/
    protected void StandardUpdate() {
        if (!alive)//生きている状態しか更新しない
            return;
        GetOrder();
        CheckStatus();//状態更新
        SetAnimationStatus();//動画状態更新
        Move();//移動
    }

    protected abstract void ChildUpdate();

    protected void StandardLateUpdate() {
        CheckChildStatus();//子どもクラスで実現された
        ClearStatus();//後始末//親クラスで実現された

        if (deadEnd)
            Death();
    }

    protected abstract void Inif();//特殊初期化
    protected abstract void GetOrder();//命令を取得する
    protected abstract void CheckChildStatus();//特殊状態チェック
    protected abstract void SetAnimationStatus();//アニメーションチェック

    public bool alive = true;
    public bool deadEnd = false;

    public void DeadPlayEnd() {//死亡演出
        deadEnd = true;
    }

    public void SetDead() {
        alive = false;
        myRigidbody.isKinematic = true;
        ResetThis();
        myAnimator.SetBool("Dieing", true);
    }

    private void Death() {
        OtherProcessWhenDead();
        Destroy(transform.gameObject);
    }

    protected abstract void OtherProcessWhenDead();//死ぬ時の特殊処理

    //*********************************************
    //移動関連
    //*********************************************
    private void CheckStatus() {
        if (onTheGround)//床に居る時にジャンプ回数をリセットする
            jumpCount = 0;
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
        oldPos = transform.position;
        status = (int)CreatureStatus.none;
    }

    private void Jump(float strength) {
        myRigidbody.AddForce(new Vector2(0, strength));
    }

    private void MoveOnX(bool right) {
        myRigidbody.velocity =
            new Vector2((right ? speed : -speed),
            myRigidbody.velocity.y);
    }

    protected void ResetForceOnX() {
        if (onTheGround || attacking)//床の上に居る限り、減速する
            myRigidbody.velocity = new Vector2(0,
                myRigidbody.velocity.y);
    }

    private void ResetThis() {
        myRigidbody.velocity = new Vector2(0, 0);
    }

    //方向変更ではもっと効率が良い方法があるはず
    protected void ChangeDirectOnX(bool right) {
        Vector2 theScale = transform.localScale;

        theScale.x = right ? Math.Abs(theScale.x) : -Math.Abs(theScale.x);
        transform.localScale = theScale;
        facingRight = right;
    }

    protected void ChangeDirectOnX() {
        Vector2 theScale = transform.localScale;

        theScale.x =  - theScale.x;
        transform.localScale = theScale;
    }
}
