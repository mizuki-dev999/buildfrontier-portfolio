using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using KanKikuchi.AudioManager;
using DG.Tweening;
using System;

public class HomeUIManager : MonoBehaviour
{
    //-------------------------
    [Header("ステータスUI")]
    public CanvasGroup statusImage;
    public RectTransform statusRectTransform;
    public TextMeshProUGUI[] statusTexts;
    public TextMeshProUGUI myName;
    //-------------------------
    [Header("錬金UI")]
    public CanvasGroup[] alchemyImages;
    public RectTransform[] alchemyRectTransforms;
    //-------------------------
    [Header("装備UI")]
    public CanvasGroup[] gearImages;
    public RectTransform[] gearRectTransforms;
    public Button[] gearBtns;
    public CanvasGroup changeGearPanel;
    public RectTransform changeGearRectTransform;
    public TextMeshProUGUI[] statusMiniTexts; //装備変更用ステータステキスト
    //-------------------------
    [Header("クエストUI")]
    public Image[] questImages;
    public RectTransform[] questRectTransforms;
    public TextMeshProUGUI[] questTexts;
    //-------------------------
    [Header("ジョブUI")]
    public CanvasGroup[] jobCanvasGroup;
    public RectTransform[] jobRectTransforms;
    //-------------------------
    [Header("メニューUI")]
    public CanvasGroup menuImage;
    public RectTransform menuRectTransform;
    //-------------------------
    [Header("UIまとめ")]
    public GameObject[] allUI;
    //-------------------------
    [Header("装備変更スクロールビュー")]
    public GameObject[] gearScrollViews;
    //-------------------------
    [Header("装備アイコン生成親オブジェクト")]
    public GameObject weaponContent; //武器はここに表示
    public GameObject armorContent; // 防具はここに表示
    public GameObject accessoryContent; // アクセはここに表示
    public GameObject gearIconPrefab;
    public Sprite[] gearFrame;
    //-------------------------
    [Header("装備編成スロット")]
    public GameObject[] equipSlots;
    public GameObject[] equipSlotsInStatus;
    //-------------------------
    [Header("装備詳細")]
    public TextMeshProUGUI[] gearInfoTexts;
    public GameObject[] gearInfoBGs;
    public RectTransform gearInfoPanelRectTrance;
    //-------------------------
    [Header("装備売却")]
    public GameObject sellGearPanel;//親
    public GameObject sellContent;//売却物をここに表示
    public GameObject forSellWeaponContent; //武器はここに表示
    public GameObject forSellArmorContent; // 防具はここに表示
    public GameObject forSellAccessoryContent; // アクセはここに表示
    public RectTransform sellGearPanelRectTrans;
    public CanvasGroup sellGearPanelCanvasGroup;
    public TextMeshProUGUI sellMoneyText;
    [System.NonSerialized] public int totalSellMoney;
    public Button[] sellGearChangeBtns;
    public GameObject[] sellGearScrollViews;
    //-------------------------
    [Header("アンダーボタン")]
    public Button[] underBtns;
    public Image[] underBtnImages;
    public Sprite normalSprite;
    public Sprite pressedSprite;
    //-------------------------
    [Header("装備種切り替えボタン")]
    public Button[] gearChangeBtns;
    //-------------------------
    [Header("ジョブチェンジ")]
    public TextMeshProUGUI selectJobNameText;
    public TextMeshProUGUI nowJobNameText;
    public TextMeshProUGUI jobInfoText;
    public TextMeshProUGUI equipGearText;
    public Button[] jobBtns;
    public CanvasGroup[] jobChangeCanvasGroups;
    public RectTransform[] jobChangeRecttransforms;
    public GameObject jobSymbol;
    //-------------------------
    [Header("スキルツリー")]
    public RectTransform skillTreePanelRectTrasform;
    public CanvasGroup skillTreePanelCanvasGroup;
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI skillInfoText;
    public TextMeshProUGUI skillLevelText;
    public TextMeshProUGUI jobTopName;
    public SkillDatabase[] skillDatabases;
    public Button skillLevelUpBtn;
    public Button skillLevelDownBtn;
    public TextMeshProUGUI jobPointText;
    public TextMeshProUGUI[] levelTags;
    //-------------------------
    [Header("スキル登録")]
    public CanvasGroup skillRegisterPanelCanvasGroup;
    public RectTransform skillRegisterPanelRectTransform;
    public GameObject hasActiveSkillPanel; //生成親
    public GameObject skillRegisterIcon; //生成プレハブ
    public HomeSkillSlotIcon[] skillSlots;
    public Image[] skillSlotImages;
    public TextMeshProUGUI skillRegisterNameText;
    public TextMeshProUGUI skillRegisterInfoText;
    public TextMeshProUGUI skillRegisterLevelText;
    //-------------------------
    [Header("テキスト")]
    public TextMeshProUGUI myLevelText; // レベル表記テキスト
    public TextMeshProUGUI moneyText; // 所持金テキスト
    public TextMeshProUGUI sceanText; // 現場面名テキスト
    public TextMeshProUGUI hasWeaponCountText; // 武器の個数
    public TextMeshProUGUI hasArmorCountText; // 防具の個数
    public TextMeshProUGUI hasAccessoryCountText; // アクセの個数
    public TextMeshProUGUI hasForSellWeaponCountText; // 武器の個数(売却用)
    public TextMeshProUGUI hasForSellArmorCountText; // 防具の個数(売却用)
    public TextMeshProUGUI hasForSellAccessoryCountText; // アクセの個数(売却用)
    //-------------------------
    [Header("必要経験値表示用")]
    public GameObject nextExpImage;
    public TextMeshProUGUI nextExpText;
    public ExpTable expTable;
    //-------------------------
    [Header("トップパネル")]
    public RectTransform[] topRectTransforms;
    public CanvasGroup[] topImages;
    //-------------------------
    [Header("設定変更パネル")]
    public Button optionBtn;
    public GameObject optionPanel;
    public Image optionPanelImage;
    public GameObject optionMenu;
    public CanvasGroup optionMenuCanvasGroup;
    public Button[] optionBtns;
    public GameObject[] optionPanels;
    //-------------------------
    [Header("その他")]
    public GameObject stopPanel; // UIが作動しないように最前面においてさえぎる
    //public static HomeUIManager instance;   
    [System.NonSerialized] public GameObject myCharacterStatus; // 自キャラ情報のオブジェクト
    [System.NonSerialized] public MyCharacterStatus myStatus; // 自キャラ情報のスクリプト
    [System.NonSerialized] public GameObject inventoryObj; // インベントリオブジェクト
    [System.NonSerialized] public Inventory inventory; // インベントリオブジェクトのスクリプト
    public MapDatabase mapDatabase;
    [System.NonSerialized] public MapDatabase.MapData mapData;
    [System.NonSerialized] public int lastInUINumber; //最後にインしたUI番号
    [System.NonSerialized] public int nextInUINumber; //次にインしたいUI番号
    public Image expBar;
    public Slider bgmSlider;
    public Slider seSlider;
    public TextMeshProUGUI bgmVolumeText;
    public TextMeshProUGUI seVolumeText;
    public CanvasGroup alertImageCanvasGroup;
    public TextMeshProUGUI alertText; // 注意喚起テキスト
    public static bool multiTouchEnabled = false;

    private void Awake()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.HOME, 0.5f, 0.5f, 0.7f, 0, 1);
    }

    void Start()
    {
        //------------------------------------------------------
        myCharacterStatus = GameObject.Find("MyCharacterStatus"); // 生成したMyCharacterStatusを取得
        myStatus = myCharacterStatus.GetComponent<MyCharacterStatus>(); // scriptを取得
        inventoryObj = GameObject.Find("Inventory");
        inventory = inventoryObj.GetComponent<Inventory>();
        //------------------------------------------------------
        DOTween.Clear(true);
        //underBtns[3].interactable = false;
        underBtnImages[3].sprite = pressedSprite;
        gearChangeBtns[0].interactable = false;
        //初期アニメーション
        SlideInQuestUI();
        SlideInTopImage();
        //テキストの更新
        UpdateInventoryCountTexts();
        myLevelText.text = string.Format("Lv{0}", myStatus.Level); // レベル表記を更新
        if (myStatus.Level != myStatus.maxLevel) expBar.fillAmount = (float)myStatus.Exp / (float)expTable.GetNextExp(myStatus.Level);
        else expBar.fillAmount = 1;
        moneyText.text = $"{myStatus.Money:N0}"; // 所持マネー
        //インベントリにアイテムを生成
        UpdateWeaponInventory();
        UpdateArmorInventory();
        UpdateAccessoryInventory();
        myStatus.dropCommonItemNum = 0;
        myStatus.dropRareItemNum = 0;
        myStatus.dropEpicItemNum = 0;
        myStatus.dropLegendaryItemNum = 0;
    }

    //-ボタン用関数------------------------------------------------------------------------------------
    #region
    public void OnStatusBtn()
    {
        ClickSound();
        if (lastInUINumber == 0) return;
        stopPanel.SetActive(true);
        UpdateStatusText(statusTexts);
        nextInUINumber = 0;
        ChoiceOutUI();
        SlideOutTopimage("ステータス");
        BtnImageChanger(underBtnImages[0], underBtnImages);
        SetEquippedGearInStatus();
    }
    public void OnAlchemyBtn()
    {
        ClickSound();
        if (lastInUINumber == 1) return;
        stopPanel.SetActive(true);
        nextInUINumber = 1;
        ChoiceOutUI();
        SlideOutTopimage("錬金");
        BtnImageChanger(underBtnImages[1], underBtnImages);
    }
    public void OnInventoryBtn()
    {
        ClickSound();
        if (lastInUINumber == 2) return;
        stopPanel.SetActive(true);
        nextInUINumber = 2;
        ChoiceOutUI();
        SlideOutTopimage("装備");
        BtnImageChanger(underBtnImages[2], underBtnImages);
    }
    public void OnQuestBtn()
    {
        ClickSound();
        if (lastInUINumber == 3) return;
        stopPanel.SetActive(true);
        nextInUINumber = 3;
        ChoiceOutUI();
        SlideOutTopimage("クエスト");
        BtnImageChanger(underBtnImages[3], underBtnImages);
    }
    public void OnJobBtn()
    {
        ClickSound();
        if (lastInUINumber == 4) return;
        stopPanel.SetActive(true);
        nextInUINumber = 4;
        ChoiceOutUI();
        SlideOutTopimage("ジョブ");
        BtnImageChanger(underBtnImages[4], underBtnImages);
    }
    public void OnMenuBtn()
    {
        ClickSound();
        if (lastInUINumber == 5) return;
        stopPanel.SetActive(true);
        nextInUINumber = 5;
        ChoiceOutUI();
        SlideOutTopimage("メニュー");
        BtnImageChanger(underBtnImages[5], underBtnImages);
    }
    public void OnChangeGearBtn()
    {
        ClickSound();
        if (lastInUINumber == 6) return;
        stopPanel.SetActive(true);
        UpdateStatusText(statusMiniTexts);
        nextInUINumber = 6;
        ChoiceOutUI();
        SlideOutTopimage("装備変更");
        SetEquippedGear();
        hasWeaponCountText.text = $"{inventory.WeaponInventory.Count}/{inventory.maxNum}";
        hasArmorCountText.text = $"{inventory.ArmorInventory.Count}/{inventory.maxNum}";
        hasAccessoryCountText.text = $"{inventory.AccessoryInventory.Count}/{inventory.maxNum}";
        UpdateWeaponInventory();
        UpdateArmorInventory();
        UpdateAccessoryInventory();
    }
    public void OnSellGearBtn()
    {
        ClickSound();
        if (lastInUINumber == 7) return;
        stopPanel.SetActive(true);
        nextInUINumber = 7;
        ChoiceOutUI();
        SlideOutTopimage("売却");
        UpdateSellInventoryCountTexts();
        UpdateWeaponForSellInventory();
        UpdateArmorForSellInventory();
        UpdateAccessoryForSellInventory();
        foreach (Transform n in sellContent.transform)
        {
            Destroy(n.gameObject);
        }
        totalSellMoney = 0;
        sellMoneyText.text = $"{totalSellMoney:N0}";
    }
    public void OnJobChangeBtn()
    {
        ClickSound();
        if (lastInUINumber == 8) return;
        stopPanel.SetActive(true);
        nextInUINumber = 8;
        ChoiceOutUI();
        JobChangeInit();
        SlideOutTopimage("ジョブチェンジ");
    }
    public void OnSkillTreeBtn()
    {
        ClickSound();
        if (lastInUINumber == 9) return;
        stopPanel.SetActive(true);
        nextInUINumber = 9;
        ChoiceOutUI();
        SkillTreeTextInit();
        SlideOutTopimage("スキルツリー");
    }
    public void OnSkillRegistBtn()
    {
        ClickSound();
        if (lastInUINumber == 10) return;
        stopPanel.SetActive(true);
        nextInUINumber = 10;
        ChoiceOutUI();
        SkillSlotInit((int)myStatus.job);
        SlideOutTopimage("スキル登録");
    }
    //---------------------------------
    //装備編成関連ボタン
    #region
    public void OnWeaponBtn()
    {
        BtnIntaractableChanger(gearChangeBtns[0], gearChangeBtns);
        ClickSound();
        ActiveChanger(gearScrollViews[0], gearScrollViews);
    }

    public void OnArmorBtn()
    {
        BtnIntaractableChanger(gearChangeBtns[1], gearChangeBtns);
        ClickSound();
        ActiveChanger(gearScrollViews[1], gearScrollViews);
    }

    public void OnAccessoryBtn()
    {
        BtnIntaractableChanger(gearChangeBtns[2], gearChangeBtns);
        ClickSound();
        ActiveChanger(gearScrollViews[2], gearScrollViews);
    }
    #endregion
    //---------------------------------
    //装備売却関連ボタン
    #region
    public void OnSellWeaponBtn()
    {
        BtnIntaractableChanger(sellGearChangeBtns[0], sellGearChangeBtns);
        ClickSound();
        ActiveChanger(sellGearScrollViews[0], sellGearScrollViews);
    }

    public void OnSellArmorBtn()
    {
        BtnIntaractableChanger(sellGearChangeBtns[1], sellGearChangeBtns);
        ClickSound();
        ActiveChanger(sellGearScrollViews[1], sellGearScrollViews);
    }

    public void OnSellAccessoryBtn()
    {
        BtnIntaractableChanger(sellGearChangeBtns[2], sellGearChangeBtns);
        ClickSound();
        ActiveChanger(sellGearScrollViews[2], sellGearScrollViews);
    }
    
    public void OnSellBtn()
    {
        ClickSound();
        foreach (Transform sellObj in sellContent.transform)
        {
            GearIcon sellObjGearIcon = sellObj.GetComponent<GearIcon>();
            sellObjGearIcon.RemoveInventoryItem(sellObjGearIcon.thisGear);
            Destroy(sellObj.gameObject);
        }
        myStatus.Money = Mathf.Clamp(myStatus.Money + totalSellMoney, 0, 999999999);
        totalSellMoney = 0;
        sellMoneyText.text = $"{totalSellMoney:N0}";
        moneyText.text = $"{myStatus.Money:N0}"; // 所持マネー
        UpdateSellInventoryCountTexts();
    }
    #endregion
    //---------------------------------
    public void OnJobNameBtn(int jobId)
    {
        ClickSound();
        BtnIntaractableChanger(jobBtns[jobId], jobBtns);
        if (jobId == 1) jobId = 3;
        //装備不可能武器の場合外してインベントリに入れる
        switch (jobId)
        {
            case 0:
            case 1:
            case 2:
                if(myStatus.MainHand != null)
                {
                    if (myStatus.MainHand.detailType == Item.DetailType.片手杖 || myStatus.MainHand.detailType == Item.DetailType.両手杖)
                    {
                        inventory.ItemAdd(myStatus.MainHand);
                        myStatus.MainHand = null;
                    }
                }
                if(myStatus.OffHand != null)
                {
                    if (myStatus.OffHand.detailType == Item.DetailType.片手杖 || myStatus.OffHand.detailType == Item.DetailType.魔道具)
                    {
                        inventory.ItemAdd(myStatus.OffHand);
                        myStatus.OffHand = null;
                    }
                }
                break;
            case 3:
            case 4:
            case 5:
                if (myStatus.MainHand != null)
                {
                    if (myStatus.MainHand.detailType != Item.DetailType.片手杖 && myStatus.MainHand.detailType != Item.DetailType.両手杖)
                    {
                        inventory.ItemAdd(myStatus.MainHand);
                        myStatus.MainHand = null;
                    }
                }
                if (myStatus.OffHand != null)
                {
                    if (myStatus.OffHand.detailType != Item.DetailType.片手杖 && myStatus.OffHand.detailType != Item.DetailType.魔道具 && myStatus.OffHand.detailType != Item.DetailType.盾)
                    {
                        inventory.ItemAdd(myStatus.OffHand);
                        myStatus.OffHand = null;
                    }
                }    
                break;
            default:
                break;
        }
        myStatus.job = (MyCharacterStatus.Job) Enum.ToObject(typeof(MyCharacterStatus.Job), jobId);
        nowJobNameText.text = $"{myStatus.job}";
    }
    //キャンセル用ボタン-------------------------------------------------------
    public void OnInventoryCancelBtn()
    {
        CancelSound();
        if (lastInUINumber == 2) return;
        stopPanel.SetActive(true);
        nextInUINumber = 2;
        ChoiceOutUI();
        SlideOutTopimage("装備");
        BtnImageChanger(underBtnImages[2], underBtnImages);
    }
    public void OnJobCancelBtn()
    {
        CancelSound();
        if (lastInUINumber == 4) return;
        stopPanel.SetActive(true);
        nextInUINumber = 4;
        ChoiceOutUI();
        SlideOutTopimage("ジョブ");
        BtnImageChanger(underBtnImages[4], underBtnImages);
    }
    //---------------------------------
    // メインクエスト移行ボタン
    public void OnMainQuestButton()
    {
        // インベントリが満タンの時のアラート処理
        if (inventory.WeaponInventory.Count > inventory.maxNum ||
            inventory.ArmorInventory.Count > inventory.maxNum ||
            inventory.AccessoryInventory.Count > inventory.maxNum)
        {
            ShowAlertImage("インベントリが満タンです");
        }
        else
        {
            stopPanel.SetActive(true);
            ClickSound();
            FadeManager.Instance.LoadScene("Map", 0.5f);
        }       
    }
    #endregion
    //---------------------------------------------------------------------
    //経験値詳細を表示
    #region
    public void NextExpImageActive()
    {
        nextExpImage.SetActive(true);
        if(myStatus.Level != myStatus.maxLevel)nextExpText.text = $"{myStatus.Exp}/{expTable.GetNextExp(myStatus.Level)}";
        else nextExpText.text = $"Max";
    }
    //経験値詳細を非表示
    public void NextExpImageInactive()
    {
        nextExpImage.SetActive(false);
        nextExpText.text = $"Max";
    }
    #endregion
    //--------------------------------------------------------------------
    // マップデータの取得
    public MapDatabase.MapData GetMapData(int mapId)
    {
        foreach (MapDatabase.MapData data in mapDatabase.MapDataList)
        {
            // 引数のＩＤと合致するマップデータを取得する
            if (data.Id == mapId)
            {
                // マップの情報を返す
                mapData = data;
                break;
            }
        }
        return null;
    }
    //---------------------------------------------
    // アクティブ状態を相互変換
    #region
    /// <summary>
    /// ゲームオブジェクトのアクティブ状態を切り替える
    /// </summary>
    public void ActiveChanger(GameObject targetObj, GameObject[] objs)
    {
        foreach(GameObject obj in objs)
        {
            if(obj == targetObj)
            {
                targetObj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }
    /// <summary>
    /// 引数となるボタンの画像を押された状態にし、他のボタンの画像を通常状態にする
    /// </summary>
    public void BtnImageChanger(Image targetImage, Image[] btns)
    {
        foreach (Image img in btns)
        {
            if(targetImage == img)
            {
                targetImage.sprite = pressedSprite;
            }
            else
            {
                img.sprite = normalSprite;
            }
        }
    }
    /// <summary>
    /// ボタンのintaractablを切り替える
    /// </summary>
    public void BtnIntaractableChanger(Button targetBtn, Button[] btns)
    {
        foreach (Button btn in btns)
        {
            if (btn == targetBtn)
            {
                targetBtn.interactable = false;
            }
            else
            {
                btn.interactable = true;
            }
        }
    }
    #endregion
    //---------------------------------
    // 左上の名前を変更する
    public void SceanTextChanger(string sceanName)
    {
        sceanText.text = sceanName;
    }
    //------------------------------------------------------------------------
    // UIアニメーション
    #region
    /// <summary>
    /// 最後にインしたUIをアウトする
    /// </summary>
    public void ChoiceOutUI()
    {
        switch (lastInUINumber)
        {
            case 0:
                SlideOutStatusUI();
                break;
            case 1:
                SlideOutAlchemyUI();
                break;
            case 2:
                SlideOutGearUI();
                break;
            case 3:
                SlideOutQuestUI();
                break;
            case 4:
                SlideOutJobUI();
                break;
            case 5:
                SlideOutMenuUI();
                break;
            case 6:
                SlideOutChangeGearUI();
                break;
            case 7:
                SlideOutSellGearUI();
                break;
            case 8:
                SlideOutJobChangeUI();
                break;
            case 9:
                SlideOutSkillTreeUI();
                break;
            case 10:
                SlideOutSkillRegistUI();
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 次にインしたいUIの処理を行う
    /// </summary>
    public void ChoiceInUI()
    {
        allUI[lastInUINumber].SetActive(false);
        allUI[nextInUINumber].SetActive(true);
        switch (nextInUINumber)
        {
            case 0:
                SlideInStatusUI();
                break;
            case 1:
                SlideInAlchemyUI();
                break;
            case 2:
                SlideInGearUI();
                break;
            case 3:
                SlideInQuestUI();
                break;
            case 4:
                SlideInJobUI();
                break;
            case 5:
                SlideInMenuUI();
                break;
            case 6:
                SlideInChangeGearUI();
                break;
            case 7:
                SlideInSellGearUI();
                break;
            case 8:
                SlideInJobChangeUI();
                break;
            case 9:
                SlideInSkillTreeUI();
                break;
            case 10:
                SlideInSkillRegistUI();
                break;
            default:
                break;
        }
    }
    
    //ステータス
    public void SlideInStatusUI()
    {
        statusImage.DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        statusRectTransform.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            lastInUINumber = 0;
            stopPanel.SetActive(false);//操作可能に
        });
    }
    public void SlideOutStatusUI()
    {
        statusImage.DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        statusRectTransform.DOAnchorPosX(400, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            ChoiceInUI();
        });
    }
    //錬金
    public void SlideInAlchemyUI()
    {
        alchemyImages[0].DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        alchemyRectTransforms[0].DOLocalMoveX(-297.6f, 0.5f).SetEase(Ease.OutCubic);
        alchemyImages[1].DOFade(1f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);
        alchemyRectTransforms[1].DOLocalMoveX(-297.6f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            lastInUINumber = 1;
            stopPanel.SetActive(false);//操作可能に
        });
    }
    public void SlideOutAlchemyUI()
    {
        alchemyImages[0].DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        alchemyRectTransforms[0].DOAnchorPosX(102.4f, 0.5f).SetEase(Ease.OutCubic);
        alchemyImages[1].DOFade(0f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);
        alchemyRectTransforms[1].DOAnchorPosX(102.4f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            ChoiceInUI();
        });
    }
    //装備
    public void SlideInGearUI()
    {
        gearImages[0].DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        gearRectTransforms[0].DOLocalMoveX(-400, 0.5f).SetEase(Ease.OutCubic);
        gearImages[1].DOFade(1f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);
        gearRectTransforms[1].DOLocalMoveX(-658.5f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);
        gearImages[2].DOFade(1f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic);
        gearRectTransforms[2].DOLocalMoveX(-141.5f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            lastInUINumber = 2;
            stopPanel.SetActive(false);//操作可能に
        });
    }
    public void SlideOutGearUI()
    {
        gearImages[0].DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        gearRectTransforms[0].DOAnchorPosX(0, 0.5f).SetEase(Ease.OutCubic);
        gearImages[2].DOFade(0f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);
        gearRectTransforms[2].DOAnchorPosX(258.5f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);
        gearImages[1].DOFade(0f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic);
        gearRectTransforms[1].DOAnchorPosX(-258.5f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            ChoiceInUI();
        });
    }
    //クエスト
    public void SlideInQuestUI()
    {
        questTexts[0].DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        questImages[0].DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        questRectTransforms[0].DOLocalMoveX(-400, 0.5f).SetEase(Ease.OutCubic);

        questTexts[1].DOFade(1f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic);
        questImages[1].DOFade(1f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);
        questRectTransforms[1].DOLocalMoveX(-658.5f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);

        questTexts[2].DOFade(1f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic);
        questImages[2].DOFade(1f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic);
        questRectTransforms[2].DOLocalMoveX(-141.5f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            lastInUINumber = 3;
            stopPanel.SetActive(false);//操作可能に
        });
    }
    public void SlideOutQuestUI()
    {
        questTexts[0].DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        questImages[0].DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        questRectTransforms[0].DOAnchorPosX(0, 0.5f).SetEase(Ease.OutCubic);

        questTexts[2].DOFade(0f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);
        questImages[2].DOFade(0f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);
        questRectTransforms[2].DOAnchorPosX(258.5f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);

        questTexts[1].DOFade(0f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic);
        questImages[1].DOFade(0f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic);
        questRectTransforms[1].DOAnchorPosX(-258.5f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            ChoiceInUI();
        });
    }
    //ジョブ
    public void SlideInJobUI()
    {
        jobCanvasGroup[0].DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        jobRectTransforms[0].DOLocalMoveX(-400, 0.5f).SetEase(Ease.OutCubic);

        jobCanvasGroup[1].DOFade(1f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);
        jobRectTransforms[1].DOLocalMoveX(-658.5f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);

        jobCanvasGroup[2].DOFade(1f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic);
        jobRectTransforms[2].DOLocalMoveX(-141.5f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            lastInUINumber = 4;
            stopPanel.SetActive(false);//操作可能に
        });
    }
    public void SlideOutJobUI()
    {
        jobCanvasGroup[0].DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        jobRectTransforms[0].DOAnchorPosX(0, 0.5f).SetEase(Ease.OutCubic);

        jobCanvasGroup[2].DOFade(0f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);
        jobRectTransforms[2].DOAnchorPosX(258.5f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);

        jobCanvasGroup[1].DOFade(0f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic);
        jobRectTransforms[1].DOAnchorPosX(-258.5f, 0.5f).SetDelay(0.1f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            ChoiceInUI();
        });
    }
    //メニュー
    public void SlideInMenuUI()
    {
        menuImage.DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        menuRectTransform.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            lastInUINumber = 5;
            stopPanel.SetActive(false);
        });
    }
    public void SlideOutMenuUI()
    {
        menuImage.DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        menuRectTransform.DOAnchorPosX(400, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            ChoiceInUI();
        });
    }
    //装備変更
    public void SlideInChangeGearUI()
    {
        changeGearPanel.DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        changeGearRectTransform.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            lastInUINumber = 6;
            stopPanel.SetActive(false);
        });
    }
    public void SlideOutChangeGearUI()
    {
        changeGearPanel.DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        changeGearRectTransform.DOAnchorPosX(400, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            ChoiceInUI();
        });
    }
    //装備売却
    public void SlideInSellGearUI()
    {
        sellGearPanelCanvasGroup.DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        sellGearPanelRectTrans.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            lastInUINumber = 7;
            stopPanel.SetActive(false);
        });
    }
    public void SlideOutSellGearUI()
    {
        sellGearPanelCanvasGroup.DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        sellGearPanelRectTrans.DOAnchorPosX(400, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            ChoiceInUI();
        });
    }
    
    //ジョブチェンジ
    public void SlideInJobChangeUI()
    {
        jobChangeCanvasGroups[0].DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        jobChangeRecttransforms[0].DOAnchorPosX(30, 0.5f).SetEase(Ease.OutCubic);
        jobChangeCanvasGroups[1].DOFade(1f, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);
        jobChangeRecttransforms[1].DOAnchorPosX(0, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            lastInUINumber = 8;
            stopPanel.SetActive(false);
        });
    }
    public void SlideOutJobChangeUI()
    {
        jobChangeCanvasGroups[1].DOFade(0, 0.5f).SetEase(Ease.OutCubic);
        jobChangeRecttransforms[1].DOAnchorPosX(400, 0.5f).SetEase(Ease.OutCubic);
        jobChangeCanvasGroups[0].DOFade(0, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic);
        jobChangeRecttransforms[0].DOAnchorPosX(430, 0.5f).SetDelay(0.05f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            ChoiceInUI();
        });
    }
    //スキルツリー
    public void SlideInSkillTreeUI()
    {
        skillTreePanelCanvasGroup.DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        skillTreePanelRectTrasform.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            lastInUINumber = 9;
            stopPanel.SetActive(false);
        });
    }
    public void SlideOutSkillTreeUI()
    {
        skillTreePanelCanvasGroup.DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        skillTreePanelRectTrasform.DOAnchorPosX(400, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            ChoiceInUI();
        });
    }
    //スキル登録
    public void SlideInSkillRegistUI()
    {
        skillRegisterPanelCanvasGroup.DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        skillRegisterPanelRectTransform.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            lastInUINumber = 10;
            stopPanel.SetActive(false);
        });
    }
    public void SlideOutSkillRegistUI()
    {
        skillRegisterPanelCanvasGroup.DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        skillRegisterPanelRectTransform.DOAnchorPosX(400, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            ChoiceInUI();
        });
    }
    //オプションボタン
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
    //トップUI
    public void SlideInTopImage()
    {
        topImages[0].DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        topRectTransforms[0].DOAnchorPosX(0, 0.5f).SetEase(Ease.OutCubic);
        topImages[1].DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        topRectTransforms[1].DOAnchorPosX(0, 0.5f).SetEase(Ease.OutCubic);
    }
    public void SlideOutTopimage(string sceanName)
    {
        topImages[0].DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        topRectTransforms[0].DOAnchorPosX(-384, 0.5f).SetEase(Ease.OutCubic);
        topImages[1].DOFade(0f, 0.5f).SetEase(Ease.OutCubic);
        topRectTransforms[1].DOAnchorPosX(384, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            sceanText.text = sceanName;
            SlideInTopImage();
        });
    }
    #endregion
    //------------------------------------------------------------------------
    // ステータステキストの更新
    public void UpdateStatusText(TextMeshProUGUI[] texts)
    {
        myStatus.SetStatus(skillDatabases);
        int DPH = myStatus.GetPhisicalDmg() + myStatus.GetFireDmg() + myStatus.GetIceDmg() + myStatus.GetThunderDmg() + myStatus.GetWindDmg() + myStatus.GetShiningDmg() + myStatus.GetDarknessDmg();
        int DPS = (int)System.Math.Round((float)DPH * (float)myStatus.GetSpd(), System.MidpointRounding.AwayFromZero);
        //基礎能力
        texts[0].text = $"<line-height=65%>{myStatus.GetHp()}\n{myStatus.GetMp()}\n{myStatus.GetAtk()}\n{myStatus.GetDef()}\n{myStatus.GetSpd()}\n{myStatus.GetArmorPoint()}</line-height>";
        //耐性
        texts[1].text = $"<line-height=65%>{myStatus.GetPhisicalResist()}%\n{myStatus.GetFireResist()}%\n{myStatus.GetIceResist()}%\n{myStatus.GetThunderResist()}%\n{myStatus.GetWindResist()}%\n{myStatus.GetShiningResist()}%\n{myStatus.GetDarknessResist()}%</line-height>";
        //属性ダメージ
        texts[2].text = $"<line-height=65%>{DPS}\n{DPH}</line-height>\n<line-height=0%>\n<line-height=65%>{myStatus.GetPhisicalDmg()}\n+{myStatus.PhisicalRate-100}%</line-height>\n<line-height=-10%>\n<line-height=65%>{myStatus.GetFireDmg()}\n+{myStatus.FireRate - 100}%</line-height>\n<line-height=-10%>\n<line-height=65%>{myStatus.GetIceDmg()}\n+{myStatus.IceRate - 100}%</line-height>\n<line-height=-10%>\n<line-height=65%>{myStatus.GetThunderDmg()}\n+{myStatus.ThunderRate - 100}%</line-height>\n<line-height=-10%>\n<line-height=65%>{myStatus.GetWindDmg()}\n+{myStatus.WindRate - 100}%</line-height>\n<line-height=-10%>\n<line-height=65%>{myStatus.GetShiningDmg()}\n+{myStatus.ShiningRate - 100}%</line-height>\n<line-height=-10%>\n<line-height=65%>{myStatus.GetDarknessDmg()}\n+{myStatus.DarknessRate - 100}%</line-height>";
        //詳細能力
        texts[3].text = $"<line-height=65%>{myStatus.GetSpdRate()-100}%\n{myStatus.GetAutoHpRecover()}\n{myStatus.GetAutoMpRecover()}\n{myStatus.GetGuardRate()}%\n+{myStatus.GetExpGetRate() - 100}%</line-height>";
        if (texts.Length > 4)
        {
            //プロフィール
            myName.text = myStatus.Name;
            texts[4].text = $"<line-height=65%>{myStatus.Level}\n{myStatus.job}</line-height>";
            //実績
            texts[5].text = $"<line-height=65%>\n{myStatus.ClearMapIdList.Count}/42</line-height>";
        }
    }
    // ジョブチェンジボタン初期化
    public void JobChangeInit()
    {
        jobSymbol.SetActive(false);
        selectJobNameText.text = $"";
        nowJobNameText.text = $"{myStatus.job}";
        if((int)myStatus.job == 3) jobBtns[1].interactable = false;
        else jobBtns[(int)myStatus.job].interactable = false;
        jobInfoText.text = "";
        equipGearText.text = "";
    }
    //スキルツリー初期化
    public void SkillTreeTextInit()
    {
        skillNameText.text = "ーーー";
        skillInfoText.text = "<line-height=80%>種類：ーーー\n" +
            "消費MP：ーーー\n" +
            "威力：ーーー\n" +
            "クールタイム：ーーー\n" +
            "効果：";
        skillLevelText.text = "Lv0/0";
        int[] baseLevelAry = { 1, 10, 20, 30, 45, 60, 80, 100 };
        for(int i=0; i<levelTags.Length; i++)
        {
            if(myStatus.Level < baseLevelAry[i]) levelTags[i].color = new Color(140 / 255f, 140 / 255f, 140 / 255f);
        }
        var jp = myStatus.job switch
        {
            MyCharacterStatus.Job.デュエリスト => myStatus.JPDuelist,
            MyCharacterStatus.Job.ウォーリアー => myStatus.JPWarrior,
            MyCharacterStatus.Job.ナイト => myStatus.JPKnight,
            MyCharacterStatus.Job.ウィザード => myStatus.JPWizard,
            MyCharacterStatus.Job.ビショップ => myStatus.JPBishop,
            MyCharacterStatus.Job.メイデン => myStatus.JPMaiden,
            _ => 0,
        };
        jobPointText.text = $"<cspace=-0.01em>ジョブポイント：<size=160%>{jp}";
    }
    //スキル登録の初期化
    //スキルスロットの画像をジョブに合わせて切り替える
    public void SkillSlotInit(int job)
    {
        foreach(Transform obj in hasActiveSkillPanel.transform)
        {
            Destroy(obj.gameObject);
        }
        skillRegisterNameText.text = "ーーー";
        skillRegisterInfoText.text = "<line-height=80%>種類：ーーー\n" +
            "消費MP：ーーー\n" +
            "威力：ーーー\n" +
            "クールタイム：ーーー\n" +
            "効果：";
        skillRegisterLevelText.text = "Lv0";
        int[][] jobActiveSkillArys = myStatus.GetRegistedActiveSkillArys();
        int[][] allSkillLevelArys = myStatus.GetAllSkillLevelArys();
        for (int i = 0; i < jobActiveSkillArys[job].Length; i++)
        {
            skillSlots[i].Init(job, jobActiveSkillArys[job][i]);
        }
        for (int i = 0; i < allSkillLevelArys[job].Length; i++)
        {
            if(allSkillLevelArys[job][i] > 0 && skillDatabases[job].SkillDataList[i].type == SkillDatabase.Skill.SkillType.アクティブスキル)
            {
                GameObject hasActiveSkillIcon = Instantiate(skillRegisterIcon, hasActiveSkillPanel.transform);
                hasActiveSkillIcon.GetComponent<SkillRegisterIcon>().InitIcon(job, i);
            }
        }
    }
    public void SkillRegistChecker()
    {
        foreach(Transform obj in hasActiveSkillPanel.transform)
        {
            SkillRegisterIcon target = obj.GetComponent<SkillRegisterIcon>();
            target.SkillRegistChecker(target.GetJob(), target.GetId());
        }
    }
    //------------------------------------------------------------------------
    // アイテムドロップ
    #region
    public ItemDatabase commonDatabase; // アイテム情報(スクリプタブルオブジェクト)
    [System.NonSerialized] public ItemDatabase.ItemData dropItemData; // ドロップしたアイテム情報
    [System.NonSerialized] public int dropItemId; // ドロップしたアイテムID
    public void GetDropItemData(int dropItemId)
    {
        foreach (ItemDatabase.ItemData data in commonDatabase.ItemDataList)
        {
            // リストに登録されているアイテムIDと照合
            if (data.Id == dropItemId)
            {
                dropItemData = data;
                break;
            }
        }
    }

    public void OnDropBtn()
    {
        SEManager.Instance.Play(SEPath.ITEM_DROP);
        // 装備ドロップ処理を記載
        int dropItemId = UnityEngine.Random.Range(0, commonDatabase.ItemDataList.Count);
        GetDropItemData(dropItemId);
        inventory.ItemDropAndGet(dropItemData);
        switch (dropItemData.itemType)
        {
            case ItemDatabase.ItemData.ItemType.MainHand:
            case ItemDatabase.ItemData.ItemType.OffHand:
            case ItemDatabase.ItemData.ItemType.TwoHand:
                UpdateWeaponInventory();
                break;
            case ItemDatabase.ItemData.ItemType.Armor:
                UpdateArmorInventory();
                break;
            case ItemDatabase.ItemData.ItemType.Accessory:
                UpdateAccessoryInventory();
                break;
            default:
                break;
        }
        UpdateInventoryCountTexts();
    }
    public void UpdateWeaponInventory()
    {
        foreach (Transform n in weaponContent.transform)
        {
            Destroy(n.gameObject);
        }
        foreach (Item item in inventory.WeaponInventory)
        {
            GameObject obj = Instantiate(gearIconPrefab);
            obj.GetComponent<GearIcon>().SetItem(item);
            obj.GetComponent<Image>().sprite = gearFrame[item.Rare];
            obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.sprite;
            obj.transform.SetParent(weaponContent.transform);
        }
    }
    public void UpdateArmorInventory()
    {
        foreach (Transform n in armorContent.transform)
        {
            Destroy(n.gameObject);
        }
        foreach (Item item in inventory.ArmorInventory)
        {
            GameObject obj = Instantiate(gearIconPrefab);
            obj.GetComponent<GearIcon>().SetItem(item);
            obj.GetComponent<Image>().sprite = gearFrame[item.Rare];
            obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.sprite;
            obj.transform.SetParent(armorContent.transform);
        }
    }
    public void UpdateAccessoryInventory()
    {
        foreach (Transform n in accessoryContent.transform)
        {
            Destroy(n.gameObject);
        }
        foreach (Item item in inventory.AccessoryInventory)
        {
            GameObject obj = Instantiate(gearIconPrefab);
            obj.GetComponent<GearIcon>().SetItem(item);
            obj.GetComponent<Image>().sprite = gearFrame[item.Rare];
            obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.sprite;
            obj.transform.SetParent(accessoryContent.transform);
        }
    }
    public void UpdateWeaponForSellInventory()
    {
        foreach (Transform n in forSellWeaponContent.transform)
        {
            Destroy(n.gameObject);
        }
        foreach (Item item in inventory.WeaponInventory)
        {
            GameObject obj = Instantiate(gearIconPrefab);
            obj.GetComponent<GearIcon>().SetItem(item);
            obj.GetComponent<Image>().sprite = gearFrame[item.Rare];
            obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.sprite;
            obj.transform.SetParent(forSellWeaponContent.transform);
        }
    }
    public void UpdateArmorForSellInventory()
    {
        foreach (Transform n in forSellArmorContent.transform)
        {
            Destroy(n.gameObject);
        }
        foreach (Item item in inventory.ArmorInventory)
        {
            GameObject obj = Instantiate(gearIconPrefab);
            obj.GetComponent<GearIcon>().SetItem(item);
            obj.GetComponent<Image>().sprite = gearFrame[item.Rare];
            obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.sprite;
            obj.transform.SetParent(forSellArmorContent.transform);
        }
    }
    public void UpdateAccessoryForSellInventory()
    {
        foreach (Transform n in forSellAccessoryContent.transform)
        {
            Destroy(n.gameObject);
        }
        foreach (Item item in inventory.AccessoryInventory)
        {
            GameObject obj = Instantiate(gearIconPrefab);
            obj.GetComponent<GearIcon>().SetItem(item);
            obj.GetComponent<Image>().sprite = gearFrame[item.Rare];
            obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.sprite;
            obj.transform.SetParent(forSellAccessoryContent.transform);
        }
    }

    public void SetEquippedGear()
    {
        Item[] items = { myStatus.MainHand, myStatus.OffHand, myStatus.Helm, myStatus.BodyArmor, myStatus.Gauntlet, myStatus.LegArmor, myStatus.RightAccessory, myStatus.LeftAccessory };
        for (int i = 0; i < items.Length; i++)
        {
            if(equipSlots[i].transform.childCount == 2) Destroy(equipSlots[i].transform.GetChild(1).gameObject);
            if (items[i] == null) continue;
            GameObject obj = Instantiate(gearIconPrefab);
            obj.GetComponent<GearIcon>().SetItem(items[i]);
            obj.GetComponent<Image>().sprite = gearFrame[items[i].Rare];
            obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = items[i].sprite;
            obj.transform.SetParent(equipSlots[i].transform);
            obj.transform.localPosition = new Vector2(0, 0);
        }
    }
    public void SetEquippedGearInStatus()
    {
        Item[] items = { myStatus.MainHand, myStatus.OffHand, myStatus.Helm, myStatus.BodyArmor, myStatus.Gauntlet, myStatus.LegArmor, myStatus.RightAccessory, myStatus.LeftAccessory };
        for (int i = 0; i < items.Length; i++)
        {
            if (equipSlotsInStatus[i].transform.childCount == 2) Destroy(equipSlotsInStatus[i].transform.GetChild(1).gameObject);
            if (items[i] == null) continue;
            GameObject obj = Instantiate(gearIconPrefab);
            obj.GetComponent<GearIcon>().SetItem(items[i]);
            obj.GetComponent<Image>().sprite = gearFrame[items[i].Rare];
            obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = items[i].sprite;
            obj.transform.SetParent(equipSlotsInStatus[i].transform);
            obj.transform.localPosition = new Vector2(0, 0);
            obj.GetComponent<GearIcon>().drag = false;
        }
    }

    public void UpdateInventoryCountTexts()
    {
        if(inventory.WeaponInventory.Count>100) hasWeaponCountText.text = $"<color=red>{inventory.WeaponInventory.Count}</color>/{inventory.maxNum}";
        else hasWeaponCountText.text = $"{inventory.WeaponInventory.Count}/{inventory.maxNum}";
        if (inventory.ArmorInventory.Count > 100) hasArmorCountText.text = $"<color=red>{inventory.ArmorInventory.Count}</color>/{inventory.maxNum}";
        else hasArmorCountText.text = $"{inventory.ArmorInventory.Count}/{inventory.maxNum}";
        if (inventory.AccessoryInventory.Count > 100) hasAccessoryCountText.text = $"<color=red>{inventory.AccessoryInventory.Count}</color>/{inventory.maxNum}";
        else hasAccessoryCountText.text = $"{inventory.AccessoryInventory.Count}/{inventory.maxNum}";
    }

    public void UpdateSellInventoryCountTexts()
    {
        if (inventory.WeaponInventory.Count > 100) hasForSellWeaponCountText.text = $"<color=red>{inventory.WeaponInventory.Count}</color>/{inventory.maxNum}";
        else hasForSellWeaponCountText.text = $"{inventory.WeaponInventory.Count}/{inventory.maxNum}";
        if (inventory.ArmorInventory.Count > 100) hasForSellArmorCountText.text = $"<color=red>{inventory.ArmorInventory.Count}</color>/{inventory.maxNum}";
        else hasForSellArmorCountText.text = $"{inventory.ArmorInventory.Count}/{inventory.maxNum}";
        if (inventory.AccessoryInventory.Count > 100) hasForSellAccessoryCountText.text = $"<color=red>{inventory.AccessoryInventory.Count}</color>/{inventory.maxNum}";
        else hasForSellAccessoryCountText.text = $"{inventory.AccessoryInventory.Count}/{inventory.maxNum}";
    }
    #endregion
    //------------------------------------------------------------------------
    // サウンド関連
    #region
    public void ClickSound()
    {
        SEManager.Instance.Play(SEPath.CLICK);
    }
    public void CancelSound()
    {
        SEManager.Instance.Play(SEPath.CANCEL);
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
    #endregion
    //------------------------------------------------------------------------
    // プレイヤーへのメッセージを表示する
    public Sequence alertImageSequence;
    public void ShowAlertImage(string text)
    {
        this.alertText.text = text;
        this.alertImageCanvasGroup.alpha = 1;
        alertImageSequence.Rewind();
        alertImageSequence = DOTween.Sequence().Append(this.alertImageCanvasGroup.DOFade(0, 0.4f).SetDelay(1)).Pause();
        alertImageSequence.Restart();
    }

    public void OnDbugBtn()
    {
        for(int i=0; i<myStatus.DuelistActiveSkillSet.Length; i++)
        {
            Debug.Log($"スキルスロット{i+1}: " + $"{myStatus.DuelistActiveSkillSet[i]}");
        }
    }
}
