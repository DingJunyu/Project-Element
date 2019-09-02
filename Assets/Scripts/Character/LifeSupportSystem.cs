using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSupportSystem : MonoBehaviour {
    public int maxHitPoint = 5;
    public int hitPoint;

    private PhysicalProtectiveSystem physicalProtectiveSystem;

    public GameObject showDamage;
    private GameObject realDamage;

    private void Start() {
        hitPoint = maxHitPoint;

        physicalProtectiveSystem = GetComponent<PhysicalProtectiveSystem>();
    }

    private void Update() {
        
    }

    private void CheckAlive() {
        if (hitPoint <= 0) {
            hitPoint = 0;
            transform.GetComponent<CreatureController>().SetDead();//死ぬ時に演出行う
        }
    }

    public void SufferDamage(int damage) {//普通のダメージを受ける
        hitPoint -= damage;

        realDamage = Instantiate(showDamage, this.transform);
        realDamage.GetComponent<ShowDamage>().SetDamage(damage, true);

        CheckAlive();
    }

    public void SufferDamage(DamageContainer damage) {
        hitPoint -= (int)physicalProtectiveSystem.reserveDamage(damage);
        CheckAlive();
    }

    public void SufferHeal(int damage) {
        hitPoint += damage;
        CheckTop();
    }

    private void CheckButton() {
        if (hitPoint <= 0)
            hitPoint = 0;
    }

    private void CheckTop() {
        if (hitPoint > maxHitPoint)
            hitPoint = maxHitPoint;
    }
}
