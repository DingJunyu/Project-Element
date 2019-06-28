using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveUnit
{
    GameObject here;

    public SaveUnit(string str) {
        if (!PlayerPrefs.HasKey(str)) {
            here = default;
        }
    }
}
