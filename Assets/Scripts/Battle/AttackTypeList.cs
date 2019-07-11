using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTypeList : MonoBehaviour {
    [SerializeField]
    public GameObject[] attackList;//攻撃のエフェクトを返す？

    private TypeOfAttack.Shape shape { get; }//読み込み専用

    public AttackTypeList() {
        shape = new TypeOfAttack.Shape();
    }

    public GameObject ReferThisAttack(int num) {
        if (num < 0 || num >= attackList.Length) {
            return default;
        }
        return attackList[num];
    }
}
