using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : CreatureController
{
    private Rigidbody2D rig;   //刚体

    private float jumpForce;  //跳跃的力
    // Start is called before the first frame update
    void Start() {
        rig = GetComponent<Rigidbody2D>();
        jumpForce = 300f;
    }

    // Update is called once per frame
    void Update() {
        ControlWithCommand();
    }

    void ControlWithCommand() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rig.AddForce(new Vector2(0, jumpForce)); 
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            rig.AddForce(new Vector2(40f, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rig.AddForce(new Vector2(-40f, 0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Item_Mark"))
            Debug.Log("1");
    }
}
