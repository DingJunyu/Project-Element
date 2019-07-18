using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineStatus : MonoBehaviour {
    public UnityEngine.Material standard;
    public UnityEngine.Material outLineVer;

    private void Update() {

    }

    public void SetChoose() {
        GetComponent<SpriteRenderer>().material = outLineVer;
    }

    public void SetUnChoose() {
        GetComponent<SpriteRenderer>().material = standard;
    }
}
