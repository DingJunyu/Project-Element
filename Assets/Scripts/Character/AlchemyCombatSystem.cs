using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyCombatSystem : MonoBehaviour {
    public GameObject magicTypeList;//魔法リスト
    private GameObject tubeUsing;//選んだ試験
    private GameObject magic;

    public bool usingMagic = false;//魔法を使っている途中に他の魔法を使えないようにする
    public void SetEnd() { usingMagic = false; }

    public void UseMagic() {
        if (usingMagic)
            return;

        usingMagic = true;

        magic = Instantiate(magicTypeList.GetComponent<AttackTypeList>().
            ReferThisAttack(0), transform);
        magic.transform.position = new Vector3(transform.position.x,
            transform.position.y, 0.1f);
    }
}
