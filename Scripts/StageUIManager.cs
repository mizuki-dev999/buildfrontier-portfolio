using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using KanKikuchi.AudioManager;
using DG.Tweening;

// �X�e�[�WUI���Ǘ�(�X�e�[�W�i�s�x/NextButton/ExitButton)
public class StageUIManager : MonoBehaviour
{
    //-------------------------
    [Header("�����X�e�[�^�XUI")]
    public Image hpBar;
    public Image mpBar;
    public Image expBar;
    public Image jobImage;
    public Sprite[] jobSymbolAry;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI jobText;
    //-------------------------
    [Header("�G�X�e�[�^�XUI")]
    public Image enemyHpBar;
    public Image castTimeBar;
    public TextMeshProUGUI enemyPeacentageText;
    public TextMeshProUGUI enemyNameText; // �G�̖��O
    public TextMeshProUGUI skillName;
    //-------------------------
    [Header("�퓬���UI")]
    public Button menuBtn;
    public TextMeshProUGUI stageText; // �X�e�[�W�i�s�x
    public TextMeshProUGUI HasMoneyText;
    public TextMeshProUGUI dropCommonItemText;
    public TextMeshProUGUI dropRareItemText;
    public TextMeshProUGUI dropEpicItemText;
    public TextMeshProUGUI dropLegendaryItemText;
    public TextMeshProUGUI timerText;
    //-------------------------
    [Header("�I�[�g�{�^��&�X�s�[�h�A�b�v�{�^��")]
    public GameObject autoFrameEffect;
    public Image autoFrameEffectImage;
    public Image autoSkillButton;
    public GameObject speedUpFrameEffect;
    public Image speedUpFrameEffectImage;
    public Image speedUpButton;
    public Tweener autoRotateTween;
    public Tweener speedUpRotateTween;
    public Sprite activeImage;
    public Sprite inactiveImage;
    //-------------------------
    [Header("���j���[UI")]
    public TextMeshProUGUI stageNameText;
    public GameObject menuPanel; //�X�P�[��
    public Image menuPanelImage; //�t�F�[�h
    public float menuPanelAlpha;
    public float popUpAnimationtime;
    public GameObject popUpMenu;
    public CanvasGroup popUpMenuCanvasGroup;
    //-------------------------
    [Header("�ݒ�")]
    public Button optionBtn;
    public GameObject optionPanel;
    public Image optionPanelImage;
    public GameObject optionMenu;
    public CanvasGroup optionMenuCanvasGroup;
    public Button[] optionBtns;
    public GameObject[] optionPanels;
    public Slider bgmSlider;
    public Slider seSlider;
    public TextMeshProUGUI bgmVolumeText;
    public TextMeshProUGUI seVolumeText;
    //-------------------------
    [Header("�X�L�������p�l��")]
    public GameObject skillInfoPanel;
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI skillInfoText;
    public TextMeshProUGUI skillLevelText;
    //-------------------------
    [Header("���̑�")]
    [System.NonSerialized] public GameObject myCharacterStatus; // ���L�������̃I�u�W�F�N�g
    [System.NonSerialized] public MyCharacterStatus myStatus; // ���L�������̃X�N���v�g
    public BattleManager battleManager;
    public GameObject StopPanel; //�A�Ŗh�~�p�l��
    public Image BackGroundImage; // �w�iImage
    public Image EnemyImage; // �GImage
    public GameObject enemyImage; // �G�̃t�F�[�h�A�E�g�ɕK�v!!
    public ExpTable expTable;
    //-------------------------
    [Header("�f�o�b�O�p")]
    public TextMeshProUGUI baseStatus;
    public TextMeshProUGUI damageStatus;
    public TextMeshProUGUI resistStatus;
    public TextMeshProUGUI anotherStatus;

    void Awake()
    {
        myCharacterStatus = GameObject.Find("MyCharacterStatus"); // Home�V�[���Ő�������MyCharacterStatus���擾
        myStatus = myCharacterStatus.GetComponent<MyCharacterStatus>(); // script���擾
    }

    private void Start()
    {
        AutoRotateEffectStart(autoFrameEffect);
        SpeedUpRotateEffectStart(speedUpFrameEffect);
        if (myStatus.AutoSkillMode)
        {
            autoFrameEffectImage.color = new Color(1, 1, 1, 0.8f);
            autoSkillButton.sprite = activeImage;
        }
        if (myStatus.SpeedUpMode)
        {
            Time.timeScale = 2;
            speedUpFrameEffectImage.color = new Color(1, 1, 1, 0.8f);
            speedUpButton.sprite = activeImage;
        }
        jobImage.sprite = jobSymbolAry[(int)myStatus.job];
    }

    /// <summary>
    /// �X�e�[�W�i�s�x���e�L�X�g�ɔ��f
    /// </summary>
    /// <param name="currentStageNum">���݂̃X�e�[�W�i�s�x</param>
    /// <param name="maxStageNum">���v�X�e�[�W��</param>
    public void UpdateStageData(int currentStageNum, int maxStageNum)
    {
        stageText.text = $"<cspace=-0.1em>{currentStageNum+1}/{maxStageNum}";
    }
    /// <summary>
    /// �w�i�摜�̕ύX
    /// </summary>
    /// <param name="sprite">�w�i�摜</param>
    public void UpdateBackGround(Sprite sprite)
    {
        BackGroundImage.sprite = sprite;
    }

    public void UpdateEnemyImage(Sprite sprite)
    {
        EnemyImage.sprite = sprite;
    }

    public void UpdateMyHpText(int hp)
    {
        hpText.text = $"{hp}";
    }

    public void UpdateMyMpText(int mp)
    {
        mpText.text = $"{mp}";
    }

    public void UpdateLevelText(int myLevel)
    {
        levelText.text = $"<cspace=0.01em>Lv{myLevel}";
    }

    public void UpdateExpText()
    {
        if (myStatus.Level == 100)
        {
            expText.text = ($"<cspace=0.01em>EXP --/--");
            expBar.fillAmount = 1;
        }
        else
        {
            expText.text = ($"<cspace=0.01em>EXP {myStatus.Exp}/{expTable.GetNextExp(myStatus.Level)}");
            expBar.fillAmount = (float)myStatus.Exp / (float)expTable.GetNextExp(myStatus.Level);
        }
    }

    // ����ɖ߂�
    public void OnExitButton()
    {
        ClickSound();
        FadeManager.Instance.LoadScene("Home", 1.0f);
    }

    public void UpdateEnemyNameText(string enemyName)
    {
        enemyNameText.text = enemyName;
    }

    public void AutoRotateEffectStart(GameObject effectObj)
    {
        effectObj.transform.localEulerAngles = new Vector3(0, 0, 0);
        autoRotateTween = effectObj.transform.DOLocalRotate(new Vector3(0, 0, 360f), 2f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)
            .SetLink(effectObj);
        autoRotateTween.Play().SetUpdate(true);
    }

    public void SpeedUpRotateEffectStart(GameObject effectObj)
    {
        effectObj.transform.localEulerAngles = new Vector3(0, 0, 0);
        speedUpRotateTween = effectObj.transform.DOLocalRotate(new Vector3(0, 0, 360f), 2f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)
            .SetLink(effectObj).SetUpdate(true);
        speedUpRotateTween.Play();
    }

    public void OnAutoSkillBtn()
    {
        ClickSound();
        if (myStatus.AutoSkillMode)
        {
            myStatus.AutoSkillMode = false;
            autoFrameEffectImage.color = new Color(1, 1, 1, 0);
            autoSkillButton.sprite = inactiveImage; 
        }
        else
        {
            myStatus.AutoSkillMode = true;
            autoFrameEffectImage.color = new Color(1, 1, 1, 0.8f);
            autoSkillButton.sprite = activeImage;        
        }
    }

    public void OnSpeedUpBtn()
    {
        ClickSound();
        if (myStatus.SpeedUpMode)
        {
            Time.timeScale = 1;
            myStatus.SpeedUpMode = false;
            speedUpFrameEffectImage.color = new Color(1, 1, 1, 0);
            speedUpButton.sprite = inactiveImage;
        }
        else
        {
            Time.timeScale = 2;
            myStatus.SpeedUpMode = true;
            speedUpFrameEffectImage.color = new Color(1, 1, 1, 0.8f);
            speedUpButton.sprite = activeImage;
        }
    }

    public void OnMenuBtn()
    {
        ClickSound();
        menuBtn.interactable = false;
        menuPanel.SetActive(true);
        Time.timeScale = 0;
        //���j���[�p�l��������鉉�o
        menuPanelImage.DOFade(menuPanelAlpha, popUpAnimationtime).SetUpdate(true);
        popUpMenu.transform.DOScale(1, popUpAnimationtime).SetUpdate(true);
        popUpMenuCanvasGroup.DOFade(1, popUpAnimationtime).SetUpdate(true);
    }

    public void OnBackBtn()
    {
        CancelSound();
        if (myStatus.SpeedUpMode) Time.timeScale = 2;
        else Time.timeScale = 1;
        menuPanelImage.DOFade(0, popUpAnimationtime).SetUpdate(true);
        popUpMenu.transform.DOScale(0, popUpAnimationtime).SetUpdate(true);
        popUpMenuCanvasGroup.DOFade(0, popUpAnimationtime).SetUpdate(true).OnComplete(() =>
        {
            menuPanel.SetActive(false);
            menuBtn.interactable = true;
        });
    }
    //�I�v�V�����֘A
    public void OnOptionBtn()
    {
        ClickSound();
        SoundPanelInit();
        optionBtn.interactable = false;
        optionPanel.SetActive(true);
        optionPanelImage.DOFade(0.2f, 0.2f).SetUpdate(true);
        optionMenu.transform.DOScale(1, 0.2f).SetUpdate(true);
        optionMenuCanvasGroup.DOFade(1, 0.2f).SetUpdate(true);
    }

    public void OnOptionBackBtn()
    {
        CancelSound();
        optionPanelImage.DOFade(0, 0.2f).SetUpdate(true);
        optionMenu.transform.DOScale(0, 0.2f).SetUpdate(true);
        optionMenuCanvasGroup.DOFade(0, 0.2f).SetUpdate(true).OnComplete(() =>
        {
            optionPanel.SetActive(false);
            optionBtn.interactable = true;
        });
    }
    public void ChangeBgm()
    {
        myStatus.bgmVolume = bgmSlider.value * 0.014f;
        bgmVolumeText.text = $"{bgmSlider.value}";
        BGMManager.Instance.ChangeBaseVolume(myStatus.bgmVolume);
    }
    public void ChangeSe()
    {
        myStatus.seVolume = seSlider.value * 0.02f;
        seVolumeText.text = $"{seSlider.value}";
        SEManager.Instance.ChangeBaseVolume(myStatus.seVolume);
        ClickSound();
    }

    public void SoundPanelInit()
    {
        bgmSlider.value = myStatus.bgmVolume / 0.014f;
        bgmVolumeText.text = $"{bgmSlider.value}";
        seSlider.value = myStatus.seVolume / 0.02f;
        seVolumeText.text = $"{seSlider.value}";
    }
    //----------------------------------------------------
    public void OnRetireBtn()
    {
        StopPanel.SetActive(true);
        ClickSound();
        battleManager.ChangePhaseStop();
        Time.timeScale = 1;
        FadeManager.Instance.LoadScene("Map", 0.5f);
    }
    public void LevelUp(int getExp)
    {
        if (myStatus.Level == myStatus.maxLevel) return;
        myStatus.Exp += getExp;
        while(myStatus.Exp >= expTable.GetNextExp(myStatus.Level))
        {
            myStatus.Exp -= expTable.GetNextExp(myStatus.Level);
            myStatus.Level++;
            UpdateLevelText(myStatus.Level);
            if (myStatus.Level == myStatus.maxLevel) break;
        }
        myStatus.SetStatus(battleManager.skillDatabases);
    }
    public void ClickSound()
    {
        SEManager.Instance.Play(SEPath.CLICK);
    }
    public void CancelSound()
    {
        SEManager.Instance.Play(SEPath.CANCEL);
    }
}
