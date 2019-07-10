using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForMainCamera : MonoBehaviour {
    private GameObject player;
    private float coordZ = -10;

    //各ステージの限界
    public GameObject min_x;
    public GameObject max_x;
    public GameObject min_y;
    public GameObject max_y;

    private Vector3 newPos;

    private void Start() {
        player = GameObject.Find("Player");
    }

    private void Update() {
        GetPlayerPos();
        CheckRange();
        transform.position = newPos;
    }

    private void GetPlayerPos() {
        newPos= new Vector3(player.transform.position.x,
            player.transform.position.y, coordZ);
    }

    private void CheckRange() {
        if (newPos.x - Screen.width / 100 < min_x.transform.position.x)
            newPos.x = min_x.transform.position.x + Screen.width / 100;
        if (newPos.x + Screen.width / 100 > max_x.transform.position.x)
            newPos.x = max_x.transform.position.x - Screen.width / 100;
        if (newPos.y - Screen.height / 100 < min_y.transform.position.y)
            newPos.y = min_y.transform.position.y + Screen.height / 100;
        if (newPos.y + Screen.height / 100 > max_y.transform.position.y)
            newPos.y = max_y.transform.position.y - Screen.height / 100;
    }   
}
