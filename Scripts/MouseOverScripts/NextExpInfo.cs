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

    void Start()
    {
        myCharacterStatus = GameObject.Find("MyCharacterStatus"); // ��������MyCharacterStatus���擾
        myStatus = myCharacterStatus.GetComponent<MyCharacterStatus>(); // script���擾
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        nextExpInfoPanel.SetActive(true);
        if (myStatus.Level < myStatus.maxLevel) nextExpText.text = $"{myStatus.Exp}/{expTable.GetNextExp(myStatus.Level)}";
        else nextExpText.text = $"Max";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        nextExpInfoPanel.SetActive(false);
    }

   
}
