using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AlchemyOrMagic : MonoBehaviour {
    //一定時間内に一回攻撃は一つの敵に対して一回しか発生しません
    List<GameObject> hitList;

    private bool alreadyAttack = false;//一回は一つのターゲットにダメージを与える
    private int damage;//ここはまだ完成していない！！

    public int noneDamageGap;//その間隔
    private float speed;

    private int time;//継続時間
    private int limitedTime = 0;
    public void SetlimitedTime(int thisTime) { limitedTime = thisTime; }

    //初期化の時にSetとCalの中のいずれを呼ばないといけない
    private Quaternion direct;
    public void SetDirect(Quaternion Direct) { direct = Direct; }
    public void CalDirect(Vector2 pos) {
        Vector3 temp = pos;
        direct.SetLookRotation(temp);
        direct.z += 90;//test
        transform.localRotation = direct;//方向を更新する
    }

    //色設定も呼ばないといけない
    public void SetColor(Color targetColor) {
        transform.GetComponent<Renderer>().material.color = targetColor;
    }

    private void Start() {
        StandardStart();
    }

    protected abstract void StandardStart();

    private void Update() {
        StandardUpdate();
        TakeDamage();
    }

    protected abstract void StandardUpdate();

    private void OnTriggerEnter2D(Collider2D collision) {//敵をダメージリストに入れる
        hitList.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        hitList.Remove(collision.gameObject);//削除の効率が悪いかも。。。
    }

    private void TakeDamage() {
        foreach (var target in hitList) {
            target.transform.GetComponent<LifeSupportSystem>().SufferDamage(damage);
        }
    }
}
