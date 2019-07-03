using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public Vector2 speed;
    public Vector2 maxSpeed;

    protected void Jump(float strength) {

    }

    protected void MoveOnX(bool right) {
        transform.position = new Vector3(transform.position.x + (right ? .08f : -.08f),
            transform.position.y);
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }

    private void Move() {

    }

    private void CheckSpeed() {

    }
}
