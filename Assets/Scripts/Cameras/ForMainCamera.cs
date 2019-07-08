using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForMainCamera : MonoBehaviour {
    private GameObject player;
    private float coordZ = -10;

    private void Start() {
        player = GameObject.Find("Player");
    }

    private void Update() {
        transform.position = new Vector3(player.transform.position.x,
            player.transform.position.y, coordZ);
    }
}
