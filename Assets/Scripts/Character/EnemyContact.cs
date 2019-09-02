using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContact : MonoBehaviour {
    private bool playerHere;//プレーヤーとの接触状況

    private float lastDamageTime = 0;
    public float damageTime = 1.0f;

    public int damage = 1;
    
    //プレーヤー側がダメージを受ける
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.transform.tag == "Player") {
            if (damageTime + lastDamageTime < Time.fixedTime) {
                collision.transform.GetComponent<LifeSupportSystem>().SufferDamage(damage);
                lastDamageTime = Time.fixedTime;
            }
        }
    }
}
