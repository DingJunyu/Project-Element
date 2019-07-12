using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour {
    public int physicsDamage = 1;//一般攻撃

    public int ReferPhysicsDamage() {
        return physicsDamage;
    }
}
