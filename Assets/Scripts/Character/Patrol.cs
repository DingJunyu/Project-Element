using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//patrolではジャンプできない
public class Patrol : StandardEnemy {
    public float leftXtoNow = 2;//左への終点
    public float rightXtoNow = 2;//右への終点
    public bool faceLeft = true;//元の画像が左に向いているか

    private float realLeftX;
    private float realRightX;
    private bool facingLeft;

    // Start is called before the first frame update
    void Start() {
        StandardStart();
        EnemyInif();
        facingLeft = faceLeft;//左向きに合わせる

        realLeftX = transform.position.x - leftXtoNow;
        realRightX = transform.position.x + rightXtoNow;
    }

    // Update is called once per frame
    void Update() {
        StandardUpdate();//標準アップデート
        EnemyStandardUpdate();//敵クラスの標準アップデート
        StandardLateUpdate();//後始末
    }

    protected override void Inif() {
        
    }

    protected override void CheckChildStatus() {
        
    }

    protected override void GetOrder() {
        if (facingLeft)
            status = (int)CreatureStatus.moveToLeft;
        else
            status = (int)CreatureStatus.moveToRight;

        if (transform.position.x <= realLeftX && facingLeft) {
            ChangeDirectOnX();
            facingLeft = !facingLeft;
        }
        if (transform.position.x >= realRightX && !facingLeft) {
            ChangeDirectOnX();
            facingLeft = !facingLeft;
        }
    }

    protected override void SetAnimationStatus() {
        
    }
}
