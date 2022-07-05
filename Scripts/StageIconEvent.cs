using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using KanKikuchi.AudioManager;

public class StageIconEvent : MonoBehaviour
{
    //EventTriggerをアタッチしておく
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
        zoomOutButton = GameObject.Find("UICanvas").transform.Find("ZoomOutButton").gameObject; // クエストからマップに切り替えるボタン
        questPanel = GameObject.Find("AllQuestPanel").transform.Find($"QuestPanel{id}").gameObject; // ステージに対応したクエストパネルを取得
        //押下時演出
    }

    //EventTriggerのPointerDownイベントに登録する処理
    public void PointerDown()
    {
        startPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    //EventTriggerのPointerUpイベントに登録する処理
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
