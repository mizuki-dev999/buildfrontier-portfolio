using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraDrag : MonoBehaviour
{
    public RayHitMouse rayHitMouse;
    public Image content;
    bool is_down;
    Vector3 mouseStartPos;
    Vector2 contentSize;
    Vector2 contentOffset;
    BoxCollider2D cameraCollider;
    Vector3 defCameraPosition;
    Vector2 defColliderSize;
    Vector2 defColliderCenter;
    float defCameraOrthographicSize;

    void Awake()
    {
        contentSize = new Vector2(content.rectTransform.sizeDelta.x, content.rectTransform.sizeDelta.y);
        contentOffset = new Vector2(content.transform.position.x, content.transform.position.y);
        cameraCollider = GetComponent<BoxCollider2D>();
        defCameraPosition = Camera.main.transform.position;
        defColliderSize = cameraCollider.size;
        defColliderCenter = cameraCollider.offset;
        defCameraOrthographicSize = Camera.main.orthographicSize;
    }

    void Update()
    {
        if(Camera.main.orthographicSize != 260)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnTouchDown();

            }
            if (Input.GetMouseButton(0))
            {
                if (is_down && mouseStartPos != Input.mousePosition)
                {
                    OnTouchMove();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                is_down = false;
            }
        }
    }

    public void OnTouchDown()
    {
        var hit = rayHitMouse.Ray();
        if (hit == gameObject)
        {
            is_down = true;
            mouseStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void OnTouchMove()
    {
        if (is_down)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var moving_distance = mouseStartPos - mousePos;
            transform.position += moving_distance;
        }

        var zoom = Camera.main.orthographicSize / defCameraOrthographicSize;
        cameraCollider.size = new Vector2(1920* zoom, 1080* zoom);
        cameraCollider.offset = new Vector2(defColliderCenter.x * zoom, defColliderCenter.y * zoom);

        var colliderOffset = new Vector2(cameraCollider.offset.x, cameraCollider.offset.y);

        var limitR = (contentSize.x - cameraCollider.size.x) / 2 - colliderOffset.x + contentOffset.x;
        var limitL = (contentSize.x - cameraCollider.size.x) / -2 - colliderOffset.x + contentOffset.x;
        var limitT = (contentSize.y - cameraCollider.size.y) / 2 - colliderOffset.y + contentOffset.y;
        var limitB = (contentSize.y - cameraCollider.size.y) / -2 - colliderOffset.y + contentOffset.y;

        var posi = transform.position;

        if (contentSize.x > cameraCollider.size.x)
        {
            if (transform.position.x > limitR)
            {
                posi.x = limitR;
            }
            else if (transform.position.x < limitL)
            {
                posi.x = limitL;
            }
        }
        else
        {
            posi.x = defCameraPosition.x;
        }

        if (contentSize.y > cameraCollider.size.y)
        {
            if (transform.position.y > limitT)
            {
                posi.y = limitT;
            }
            else if (transform.position.y < limitB)
            {
                posi.y = limitB;
            }
        }
        else
        {
            posi.y = defCameraPosition.y;
        }
        transform.position = posi;
    }
}
