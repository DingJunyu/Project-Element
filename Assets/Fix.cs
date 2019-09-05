using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fix : MonoBehaviour {
    Vector3 newScale;

    Fix() {
        newScale = new Vector3(2, 2, 0);
    }

    private void FixedUpdate() {
        GetComponent<RectTransform>().localScale = newScale;
    }
}
