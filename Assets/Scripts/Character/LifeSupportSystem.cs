using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSupportSystem : MonoBehaviour {
    public int maxHitPoint = 5;
    public int hitPoint;

    private void Start() {
        hitPoint = maxHitPoint;
    }

    private void Update() {
        CheckAlive();//生きる状態確認
    }

    private void CheckAlive() {
        if (hitPoint <= 0) {
            hitPoint = 0;
            transform.GetComponent<CreatureController>().SetDead();//死ぬ時に演出行う
        }
    }

    public void ReferDamage(int damage) {
        hitPoint -= damage;
    }

    public void ReferHeal(int damage) {
        hitPoint += damage;
    }
}
