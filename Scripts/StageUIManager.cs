using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using KanKikuchi.AudioManager;
using DG.Tweening;

// ステージUIを管理(ステージ進行度/NextButton/ExitButton)
public class StageUIManager : MonoBehaviour
{
    //-------------------------
    [Header("自分ステータスUI")]
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
    [Header("敵ステータスUI")]
    public Image enemyHpBar;
    public Image castTimeBar;
    public TextMeshProUGUI enemyPeacentageText;
    public TextMeshProUGUI enemyNameText; // 敵の名前
    public TextMeshProUGUI skillName;
    //-------------------------
    [Header("戦闘情報UI")]
    public Button menuBtn;
    public TextMeshProUGUI stageText; // ステージ進行度
    public TextMeshProUGUI HasMoneyText;
    public TextMeshProUGUI dropCommonItemText;
    public TextMeshProUGUI dropRareItemText;
    public TextMeshProUGUI dropEpicItemText;
    public TextMeshProUGUI dropLegendaryItemText;
    public TextMeshProUGUI timerText;
    //-------------------------
    [Header("オートボタン&スピードアップボタン")]
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
    [Header("メニューUI")]
    public TextMeshProUGUI stageNameText;
    public GameObject menuPanel; //スケール
    public Image menuPanelImage; //フェード
    public float menuPanelAlpha;
    public float popUpAnimationtime;
    public GameObject popUpMenu;
    public CanvasGroup popUpMenuCanvasGroup;
    //-------------------------
    [Header("設定")]
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
    [Header("スキル説明パネル")]
    public GameObject skillInfoPanel;
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI skillInfoText;
    public TextMeshProUGUI skillLevelText;
    //-------------------------
    [Header("その他")]
    [System.NonSerialized] public GameObject myCharacterStatus; // 自キャラ情報のオブジェクト
    [System.NonSerialized] public MyCharacterStatus myStatus; // 自キャラ情報のスクリプト
    public BattleManager battleManager;
    public GameObject StopPanel; //連打防止パネル
    public Image BackGroundImage; // 背景Image
    public Image EnemyImage; // 敵Image
    public GameObject enemyImage; // 敵のフェードアウトに必要!!
    public ExpTable expTable;
    //-------------------------
    [Header("デバッグ用")]
    public TextMeshProUGUI baseStatus;
    public TextMeshProUGUI damageStatus;
    public TextMeshProUGUI resistStatus;
    public TextMeshProUGUI anotherStatus;

    void Awake()
    {
        myCharacterStatus = GameObject.Find("MyCharacterStatus"); // Homeシーンで生成したMyCharacterStatusを取得
        myStatus = myCharacterStatus.GetComponent<MyCharacterStatus>(); // scriptを取得
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
    /// ステージ進行度をテキストに反映
    /// </summary>
    /// <param name="currentStageNum">現在のステージ進行度</param>
    /// <param name="maxStageNum">合計ステージ数</param>
    public void UpdateStageData(int currentStageNum, int maxStageNum)
    {
        stageText.text = $"<cspace=-0.1em>{currentStageNum+1}/{maxStageNum}";
    }
    /// <summary>
    /// 背景画像の変更
    /// </summary>
    /// <param name="sprite">背景画像</param>
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

    // 自宅に戻る
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
        //メニューパネルが現れる演出
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
    //オプション関連
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
