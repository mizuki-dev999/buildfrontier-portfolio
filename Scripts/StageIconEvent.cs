using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using KanKikuchi.AudioManager;

public class StageIconEvent : MonoBehaviour
{
    //EventTrigger���A�^�b�`���Ă���
    public EventTrigger _EventTrigger;  
    public Vector3 stageIconPosi;
    public int id;
    public Camera cam;
    public GameObject zoomOutButton;
    public GameObject questPanel;
    Vector2 startPosition;
    Vector2 endPosition;
    private float safeRange = 5;
    private int zoomSize = 260;

    void Start()
    {
        cam = Camera.main;
        zoomOutButton = GameObject.Find("UICanvas").transform.Find("ZoomOutButton").gameObject; // �N�G�X�g����}�b�v�ɐ؂�ւ���{�^��
        questPanel = GameObject.Find("AllQuestPanel").transform.Find($"QuestPanel{id}").gameObject; // �X�e�[�W�ɑΉ������N�G�X�g�p�l�����擾
        //���������o
    }

    //EventTrigger��PointerDown�C�x���g�ɓo�^���鏈��
    public void PointerDown()
    {
        startPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    //EventTrigger��PointerUp�C�x���g�ɓo�^���鏈��
    public void PointerUp()
    {
        endPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if(Vector2.Distance(startPosition, endPosition) < safeRange)
        {
            SEManager.Instance.Play(SEPath.CLICK);
            cam.orthographicSize = zoomSize;
            cam.transform.position = stageIconPosi;
            transform.parent.gameObject.SetActive(false);
            zoomOutButton.SetActive(true);
            questPanel.SetActive(true);
        }
    }
}
