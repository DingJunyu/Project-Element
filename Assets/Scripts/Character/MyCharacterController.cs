using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : CreatureController
{
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        GetOrder();
    }

    private void GetOrder() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position = new Vector3(transform.position.x + .08f,
                transform.position.y);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position = new Vector3(transform.position.x - .08f,
                transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Debug.Log("102");
    }
}
