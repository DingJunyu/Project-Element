using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    private Vector2 speed;
    private Vector2 maxSpeed;

    Rigidbody2D thisBody;

    protected void MoveOnX(bool right,float newSpeed) {
        if (right)
            speed.x += newSpeed;
        else
            speed.x -= newSpeed;
    }

    protected void Jump(float strength) {

    }

    // Start is called before the first frame update
    void Start() {
        maxSpeed = new Vector2(20f, 0f);

        thisBody = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void Move() {

    }

    private void CheckSpeed() {
        if (thisBody.velocity.x >= maxSpeed.x) {
            thisBody.velocity = new Vector2(maxSpeed.x, thisBody.velocity.y);
        }
    }
}
