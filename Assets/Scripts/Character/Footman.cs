using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footman : StandardEnemy {
    GameObject player;//プレーヤーを目標にする
    float distance;
    const int reactDistance = 5;
    bool active;
    //位置関係
    bool over;
    bool right;

    public float leftXtoNow = 2;//左への終点
    public float rightXtoNow = 2;//右への終点
    public bool faceLeft = true;//元の画像が左に向いているか

    private float realLeftX;
    private float realRightX;
    private bool facingLeft;

    // Start is called before the first frame update
    protected override void ChildStart() {
        EnemyInif();

    }

    protected override void ChildUpdate() {
        EnemyStandardUpdate();//敵クラスの標準アップデート
    }

    protected override void Inif() {

    }

    protected override void CheckChildStatus() {

    }

    protected override void GetOrder() {

    }

    protected override void SetAnimationStatus() {

    }

    //目標を指定する
    private void FindTarget() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void CheckDis() {
        distance = Vector2.Distance(this.transform.position, player.transform.position);
        if (distance < reactDistance)
            active = true;
        if (active) {//位置関係を判断する
            over = transform.position.y > player.transform.position.y;
            right = transform.position.x > player.transform.position.x;
        }
    }

    private void SetStatus() {
        if (!active)
            return;

        if (right)
            status = (int)CreatureStatus.moveToRight;
        else
            status = (int)CreatureStatus.moveToLeft;

    }

    private void Patroling() {
        if (active)
            return;

        if (facingLeft)
            status = (int)CreatureStatus.moveToLeft;
        else
            status = (int)CreatureStatus.moveToRight;

        //マークポイントに超えたら転向します
        if (transform.position.x <= realLeftX && facingLeft) {
            ChangeDirectOnX();
            facingLeft = !facingLeft;
        }
        if (transform.position.x >= realRightX && !facingLeft) {
            ChangeDirectOnX();
            facingLeft = !facingLeft;
        }
    }
}
