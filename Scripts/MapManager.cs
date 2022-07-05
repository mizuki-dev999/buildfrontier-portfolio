using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [System.NonSerialized] public GameObject myCharacterStatus; // ���L�������̃I�u�W�F�N�g
    [System.NonSerialized] public MyCharacterStatus myStatus; // ���L�������̃X�N���v�g
    [System.NonSerialized] public GameObject inventoryObj; // �C���x���g���I�u�W�F�N�g
    [System.NonSerialized] public Inventory inventory; // �C���x���g���I�u�W�F�N�g�̃X�N���v�g
    public Camera cam;
    public GameObject allQuestPanel;
    public GameObject parent;
    public GameObject zoomOutButton;    
    public GameObject[] stageIconArray;
    public static int staticMapId = 0; // �o�g���V�[���Ɉڍs�̍ۂɕK�v
    public TextMeshProUGUI myLevelText;
    public TextMeshProUGUI myMoneyText;
    public Image expBar;
    public ExpTable expTable;
    public GameObject StopPanel;

    private void Awake()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.MAP, 0.1f, 0.3f, 0.7f, 0, 1);
    }

    void Start()
    {
        DOTween.Clear(true);
        myCharacterStatus = GameObject.Find("MyCharacterStatus"); // ��������MyCharacterStatus���擾
        myStatus = myCharacterStatus.GetComponent<MyCharacterStatus>(); // script���擾
        inventoryObj = GameObject.Find("Inventory");
        inventory = inventoryObj.GetComponent<Inventory>();
        myStatus.dropCommonItemNum = 0;
        myStatus.dropRareItemNum = 0;
        myStatus.dropEpicItemNum = 0;
        myStatus.dropLegendaryItemNum = 0;
        myLevelText.text = $"Lv{myStatus.Level}";
        myMoneyText.text = $"{myStatus.Money:N0}";
        expBar.fillAmount = (float)myStatus.Exp / (float)expTable.GetNextExp(myStatus.Level);
        CreateStageIcon(parent, System.Math.Min(14, 14)); // �{�^���̐���
        //myStatus.ClearMapIdList.Count / 3 + 1
    }

    // �}�b�v�{�^���̐���
    public void CreateStageIcon(GameObject parent, int num)
    {
        for (int i = 0; i < num; i++)
        {
            int mapId = i;
            GameObject stageIcon = Instantiate(stageIconArray[mapId]);
            stageIcon.transform.SetParent(parent.transform);
        }
    }

    public void OnZoomOutButton()
    {
        ClickSound();
        zoomOutButton.SetActive(false);
        foreach(Transform child in allQuestPanel.transform)
        {
            child.gameObject.SetActive(false);
        }
        cam.orthographicSize = 540;
        cam.GetComponent<CameraDrag>().OnTouchMove();
        parent.SetActive(true);
    }

    public void OnQuestIcon(int _mapId)
    {
        StopPanel.SetActive(true);
        ClickSound();
        staticMapId = _mapId;
        FadeManager.Instance.LoadScene("Quest", 0.5f);
    }

    public void OnBackHomeBtn()
    {
        StopPanel.SetActive(true);
        ClickSound();
        FadeManager.Instance.LoadScene("Home", 0.5f);
    }

    public void ClickSound()
    {
        SEManager.Instance.Play(SEPath.CLICK);
    }
}
