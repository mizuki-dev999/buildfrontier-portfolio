using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    Camera cam; //Main Camera��Camera
    private float MouseZoomSpeed = 400.0f;

    void Start()
    {
        cam = this.gameObject.GetComponent<Camera>(); //Main Camera��Camera���擾����B
    }

    void Update()
    {
        if(cam.orthographicSize != 260)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                Zoom(scroll);
            }
        }
    }

    void Zoom(float scroll)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * MouseZoomSpeed, 340, 540);
        Camera.main.GetComponent<CameraDrag>().OnTouchMove();
    }
}
