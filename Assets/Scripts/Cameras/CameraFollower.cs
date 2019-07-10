using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
    Camera mainCamera;
    Vector3 nowPos;
    Vector3 newPos;
    const float myZ = -2.0f;

    // Start is called before the first frame update
    void Start() {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        nowPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate() {
        newPos = mainCamera.transform.position + nowPos;
        newPos.z = myZ;
        transform.position = newPos;
    }
}
