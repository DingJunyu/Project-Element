using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOnTarget : MonoBehaviour {
    private bool alreadyAttack = false;//一回は一つのターゲットにダメージを与える
    private int damage = 1;//ここはまだ完成していない！！

    private void Start() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (alreadyAttack)
            return;

        if (collision.gameObject.tag == "Enemy") {
            alreadyAttack = true;//
            collision.transform.GetComponent<LifeSupportSystem>().SufferDamage(
                transform.parent.transform.
                GetComponent<CombatSystem>().ReferPhysicsDamage());
        }
    }
}
