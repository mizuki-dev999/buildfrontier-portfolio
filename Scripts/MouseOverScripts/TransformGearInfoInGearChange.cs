using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransformGearInfoInGearChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform gearInfoPanel;
    public RectTransform operationInfoPanel;
    public void OnPointerEnter(PointerEventData eventData)
    {
        gearInfoPanel.localPosition = new Vector3(667.2f, 42.4f, 0);//��ʓ���
        operationInfoPanel.localPosition = new Vector3(-273, 463, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gearInfoPanel.localPosition = new Vector3(-337, 989, 0);//��ʊO��
        operationInfoPanel.localPosition = new Vector3(-188, 1338, 0);
    }
}
