using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StandardEnemy : CreatureController {
    public GameObject HitPointBar;
    public bool hasHPBar = true;
    protected GameObject realHitPointBar;

    //HPバーつけたものに必ず使う
    protected void EnemyInif() {
        if (!hasHPBar)
            return;
        realHitPointBar = Instantiate(HitPointBar, transform);
    }

    protected void EnemyStandardUpdate() {
        if (hasHPBar)
            realHitPointBar.transform.Find("HpFill").transform.GetComponent<BarController>().
                SetPercentage(transform.GetComponent<LifeSupportSystem>().hitPoint,
                transform.GetComponent<LifeSupportSystem>().maxHitPoint);

        if (!alive) {//死ぬ時にＨＰバーを削除する
            Destroy(realHitPointBar);
            hasHPBar = false;//上の部分を無効化する
        }
    }

    ~StandardEnemy() {
        Destroy(realHitPointBar);
    }

    protected override abstract void ChildStart();
    protected override abstract void ChildUpdate();

    protected override abstract void Inif();
    protected override abstract void GetOrder();//命令を取得する
    protected override abstract void CheckChildStatus();
    protected override abstract void SetAnimationStatus();
}
