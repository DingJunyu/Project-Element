using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDamage : MonoBehaviour {
    ColorBox colorBox;

    int damageData;//

    public float time = 1.5f;
    float startTime;

    private GameObject myRealParent;

    ShowDamage() {
        colorBox = new ColorBox();
    }

    public void SetDamage(int damage,bool isThisDamage) {
        damageData = damage;
        GetComponent<UnityEngine.UI.Text>().text =
            string.Format("{0}", damageData);
        GetComponent<UnityEngine.UI.Text>().color =
            isThisDamage ? colorBox.SetColorWithRGB(255f, 0f, 0f) :
            colorBox.SetColorWithRGB(0f, 255f, 0f);
    }

    private void Awake() {
        startTime = Time.fixedTime;
    }

    private void Update() {
        if (startTime + time < Time.fixedTime)
            Destroy(this.gameObject);
    }
}
