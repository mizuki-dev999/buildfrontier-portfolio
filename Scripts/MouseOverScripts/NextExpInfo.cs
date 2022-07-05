using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class NextExpInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ExpTable expTable;
    public GameObject nextExpInfoPanel;
    public TextMeshProUGUI nextExpText;
    public GameObject myCharacterStatus;
    public MyCharacterStatus myStatus;

    public void OnPointerEnter(PointerEventData eventData)
    {
        nextExpInfoPanel.SetActive(true);
        nextExpText.text = $"{myStatus.Exp}/{expTable.GetNextExp(myStatus.Level)}";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        nextExpInfoPanel.SetActive(false);
    }

    void Start()
    {
        myCharacterStatus = GameObject.Find("MyCharacterStatus"); // ¶¬‚µ‚½MyCharacterStatus‚ğæ“¾
        myStatus = myCharacterStatus.GetComponent<MyCharacterStatus>(); // script‚ğæ“¾
    }
}
