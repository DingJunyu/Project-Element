
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOfLight : AlchemyOrMagic {
    TypeOfAttack.SpeedAndSteps speedAndSteps;

    public GameObject attackEntity;

    public RayOfLight() {
        speedAndSteps = new TypeOfAttack.SpeedAndSteps();
    }

    protected override void StandardStart() {

    }

    protected override void StandardUpdate() {
        
    }
}
