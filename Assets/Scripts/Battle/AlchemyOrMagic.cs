using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AlchemyOrMagic : MonoBehaviour {
    private GameObject myRealParent;

    //一定時間内に一回攻撃は一つの敵に対して一回しか発生しません
    List<GameObject> hitList;

    private float lastAttackTime;//一回は一つのターゲットにダメージを与え
    private int damage = 0;//ここはまだ完成していない！！
    DamageContainer damageContainer;

    public const float noneDamageGap = 0.5f;//その間隔
    protected float speed = 1;

    protected float startTime;
    protected float time;//継続時間
    protected float limitedTime = 2f;
    protected float multiTime;//使う量によって継続時間が変わる
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

    private void Awake() {
        myRealParent = transform.parent.gameObject;
    }

    private void Start() {
        StandardStart();
        CommonStart();
    }

    private void StandardStart() {
        hitList = new List<GameObject>();

        damageContainer = default;

        startTime = Time.fixedTime;
        lastAttackTime = Time.fixedTime;
    }

    protected abstract void CommonStart();

    private void Update() {
        StandardUpdate();
        CommonUpdate();
       
        TakeDamage();
        ClearThis();
    }

    protected abstract void CommonUpdate();

    private void StandardUpdate() {
        transform.parent.transform.localPosition = new Vector3(0f, 0f, 0.1f);
    }

    private void SetPos() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {//敵をダメージリストに入れる
        if (collision.gameObject.tag == "Enemy")
            hitList.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy")
            hitList.Remove(collision.gameObject);//削除の効率が悪いかも。。。
    }

    private void TakeDamage() {
        if (lastAttackTime + noneDamageGap > Time.fixedTime)
            return;
        lastAttackTime = Time.fixedTime;

        if (hitList != null)
            foreach (var target in hitList) {
                if (damage != 0)
                    target.transform.GetComponent<LifeSupportSystem>().
                        SufferDamage(damage);
                if (damageContainer != default)
                    target.transform.GetComponent<LifeSupportSystem>().
                        SufferDamage(damageContainer);
            }
    }

    //削除
    private void ClearThis() {
        if (startTime + limitedTime > Time.fixedTime)
            return;

        GameObject.Find("Player").GetComponent<AlchemyCombatSystem>().
            SetEnd();

        Destroy(transform.parent.gameObject);
        Destroy(this);
    }
}
