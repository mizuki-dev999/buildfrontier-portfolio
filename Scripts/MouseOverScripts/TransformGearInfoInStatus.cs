using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransformGearInfoInStatus : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform gearInfoPanel;
    public void OnPointerEnter(PointerEventData eventData)
    {
        gearInfoPanel.localPosition = new Vector3(-337, 42.45149f, 0);//画面内に
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gearInfoPanel.localPosition = new Vector3(-337, 989, 0);//画面外に
    }
}
