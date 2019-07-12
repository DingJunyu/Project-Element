using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemList : MonoBehaviour {

    public enum ItemSerial {
        clover,
        water
    }

    [SerializeField]
    public GameObject[] itemList;

    public GameObject ReferThisItem(int num) {
        if (num < 0 || num > itemList.Length) {
            return default;
        }
        return itemList[num];
    }

    //より安全なバージョンです
    public GameObject ReferThisItem(ItemSerial itemType) {
        return itemList[(int)itemType];
    }
}
