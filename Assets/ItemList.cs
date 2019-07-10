using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemList : MonoBehaviour {

    [SerializeField]
    public GameObject[] itemList;

    public GameObject ReferThisItem(int num) {
        if (num < 0 || num >= itemList.Length) {
            return default;
        }
        return itemList[num];
    }
}
