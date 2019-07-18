using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForMainCamera : MonoBehaviour {
    private GameObject player;
    private float coordZ = -10;

    private Camera thisCamera;

    //各ステージの限界
    public GameObject min_x;
    public GameObject max_x;
    public GameObject min_y;
    public GameObject max_y;

    float cameraHeight;
    float cameraWidth;
    float aspectRatio = Screen.width * 1.0f / Screen.height;

    private Vector3 newPos;

    private void Start() {
        player = GameObject.Find("Player");
        thisCamera = GetComponent<Camera>();

        cameraHeight = thisCamera.orthographicSize;
    }

    private void Update() {
        GetPlayerPos();
        CheckRate();
        CheckRange();
        transform.position = newPos;
    }

    private void GetPlayerPos() {
        newPos= new Vector3(player.transform.position.x,
            player.transform.position.y, coordZ);
    }

    private void CheckRate() {
        cameraHeight = GetComponent<Camera>().orthographicSize;
        cameraWidth = cameraHeight * aspectRatio;
    }

    private void CheckRange() {
        if (newPos.x - cameraWidth < min_x.transform.position.x)
            newPos.x = min_x.transform.position.x + cameraWidth;
        if (newPos.x + cameraWidth > max_x.transform.position.x)
            newPos.x = max_x.transform.position.x - cameraWidth;
        if (newPos.y - cameraHeight < min_y.transform.position.y)
            newPos.y = min_y.transform.position.y + cameraHeight;
        if (newPos.y + cameraHeight > max_y.transform.position.y)
            newPos.y = max_y.transform.position.y - cameraHeight;
    }
}
