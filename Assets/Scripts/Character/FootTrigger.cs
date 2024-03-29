﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log("On The Ground");
        transform.parent.transform.GetComponent<MyCharacterController>().
            SetGround();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //Debug.Log("Leave The Ground");
        transform.parent.transform.GetComponent<MyCharacterController>().
            RemoveOnTheGround();
    }
}
    