using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHitMouse : MonoBehaviour
{
    [HideInInspector] public GameObject hit;


    public GameObject Ray()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        float max_distance = 30f;
        var layer = LayerMask.NameToLayer("SpriteClick");
        var layer2 = LayerMask.NameToLayer("UI");
        int layer_mask = 1 << layer2 | 1 << layer;
        var is_hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, max_distance, layer_mask);

        if (is_hit)
        {
            hit = is_hit.transform.gameObject;
        }
        else
        {
            hit = null;
        }

        return hit;
    }

}
