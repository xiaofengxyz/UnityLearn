using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform frog;

    public float offsetY;
    public float zoomBase;

    private float ratio;

    private void Start() {
        ratio = Screen.height / (float)Screen.width;
        // Debug.Log(ratio);
        Camera.main.orthographicSize = zoomBase * ratio * 0.5f;

    }
    private void LateUpdate() {
        transform.position = new Vector3(transform.position.x, frog.position.y + offsetY * ratio, transform.position.z);

    }

}
