
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOfLight : AlchemyOrMagic {
    TypeOfAttack.SpeedAndSteps speedAndSteps;

    public GameObject attackEntity;

    private const float standardSpeedRate = 0.75f;
    private float length = 0.1f;
    private const float maxLength = 5.0f;

    public RayOfLight() {
        speedAndSteps = new TypeOfAttack.SpeedAndSteps();
    }

    protected override void CommonStart() {
        
    }

    protected override void CommonUpdate() {
        SetLength();
    }

    private void LateUpdate() {
        SetLength();
    }

    private void SetLength() {//長さを更新します
        //if (length >= maxLength)
        //    return;
        length += speed * standardSpeedRate;//テスト中
        if (length >= maxLength)
            length = maxLength;
        Vector3 temp;
        temp = transform.localPosition;
        temp.x = length / 2f;
        temp.z = 0.1f;
        temp.y = 0f;
        transform.localPosition = temp;

        temp = transform.localScale;
        temp.y = length;
        //temp.x = 0.2f;
        //temp.z = 0.2f;
        transform.localScale = temp;
    }
}
