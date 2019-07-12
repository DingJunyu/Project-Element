using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTypeList : MonoBehaviour {
    [SerializeField]
    public GameObject[] attackList;//攻撃のエフェクトを返す？

    public AttackTypeList() {
        attackList = new GameObject[(int)TypeOfAttack.Shape.ShapeType.amount];
    }

    public GameObject ReferThisAttack(TypeOfAttack.Shape.ShapeType shape) {
        return attackList[(int)shape];
    }
}
