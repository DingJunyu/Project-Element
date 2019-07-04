using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        transform.parent.transform.GetComponent<MyCharacterController>().
            SetGround();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Debug.Log("!");
        transform.parent.transform.GetComponent<MyCharacterController>().
            RemoveOnTheGround();
    }
}
    