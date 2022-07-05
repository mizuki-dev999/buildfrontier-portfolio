using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouceExpImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public HomeUIManager homeUIManager;
    public void OnPointerEnter(PointerEventData eventData)
    {
        homeUIManager.NextExpImageActive();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        homeUIManager.NextExpImageInactive();
    }
}
