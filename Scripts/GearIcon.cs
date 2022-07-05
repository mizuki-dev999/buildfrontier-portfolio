using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GearIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler 
{
    public Item thisGear;
    public Image frame;
    public Image gearImage;
    [System.NonSerialized] public GameObject gearInfoPanel;
    [System.NonSerialized] public TextMeshProUGUI infoText;
    [System.NonSerialized] public GameObject myCharacterStatus; // ���L�������̃I�u�W�F�N�g
    [System.NonSerialized] public MyCharacterStatus myStatus; // ���L�������̃X�N���v�g
    [System.NonSerialized] public GameObject inventoryObj; // �C���x���g���I�u�W�F�N�g
    [System.NonSerialized] public Inventory inventory; // �C���x���g���I�u�W�F�N�g�̃X�N���v�g
    [System.NonSerialized] public GameObject homeUIManagerObj; // �C���x���g���I�u�W�F�N�g
    [System.NonSerialized] public HomeUIManager homeUIManager; // �C���x���g���I�u�W�F�N�g�̃X�N���v�g
    [System.NonSerialized] public bool drag = true;
    //private Vector2 prePosition;
    private GameObject draggingObj;

    public void OnBeginDrag(PointerEventData eventData)
    {   
        if (!drag) return;
        draggingObj = Instantiate(this.gameObject);
        draggingObj.GetComponent<GearIcon>().enabled = false;
        draggingObj.GetComponent<RectTransform>().sizeDelta = new Vector2(140, 140);
        draggingObj.transform.SetParent(this.transform.root.gameObject.transform);
        draggingObj.name = "draggingObj";
        frame.color = Color.gray;
        gearImage.color = Color.gray;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!drag) return;
        draggingObj.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!drag) return;
        bool flg = true;
        
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var hit in raycastResults)
        {
            if (thisGear.Equipped == false)
            {
                if (hit.gameObject.CompareTag("MainHand") && thisGear.itemType == Item.ItemType.MainHand)
                {
                    //���C���n���h�����C���n���h�ɑ���
                    bool equip = false;
                    //�ߐڃW���u�̂Ƃ�
                    if ((CheckJobType(myStatus.job) == true) &&
                        (thisGear.detailType != Item.DetailType.�Ў��)) equip = true;
                    //�������W���u�̂Ƃ�
                    else if ((CheckJobType(myStatus.job) == false) &&
                        (thisGear.detailType == Item.DetailType.�Ў��)) equip = true;
                    if (equip)
                    {
                        if (CheckNotEquipLevel()) break;
                        if (myStatus.MainHand != null) inventory.ItemAdd(myStatus.MainHand);
                        Destroy(draggingObj);
                        FromEquipToInventoryEquippedIcon(myStatus.MainHand, 0);
                        transform.SetParent(hit.gameObject.transform);
                        transform.localPosition = new Vector2(0, 0);
                        EquipGear(thisGear, 0);
                        flg = false;
                    }
                }
                else if (hit.gameObject.CompareTag("OffHand") && thisGear.itemType == Item.ItemType.MainHand)
                {
                    //���C���n���h���I�t�n���h�ɑ���
                    bool equip = false;
                    //�ߐڃW���u�̂Ƃ�
                    if ((CheckJobType(myStatus.job) == true) &&
                        (thisGear.detailType != Item.DetailType.�Ў��)) equip = true;
                    //�������W���u�̂Ƃ�
                    else if ((CheckJobType(myStatus.job) == false) &&
                        (thisGear.detailType == Item.DetailType.�Ў��)) equip = true;
                    if (equip)
                    {
                        if (CheckNotEquipLevel()) break;
                        if (myStatus.OffHand != null) inventory.ItemAdd(myStatus.OffHand);
                        Destroy(draggingObj);
                        FromEquipToInventoryEquippedIcon(myStatus.OffHand, 1);
                        transform.SetParent(hit.gameObject.transform);
                        transform.localPosition = new Vector2(0, 0);
                        EquipGear(thisGear, 1);
                        flg = false;
                    }
                }
                else if (hit.gameObject.CompareTag("OffHand") && thisGear.itemType == Item.ItemType.OffHand)
                {
                    //�I�t�n���h���I�t�n���h�ɑ���
                    bool equip = false;
                    //�ߐڃW���u�̂Ƃ�
                    if ((CheckJobType(myStatus.job) == true) &&
                        (thisGear.detailType != Item.DetailType.������)) equip = true;
                    //�������W���u�̂Ƃ�
                    else if (CheckJobType(myStatus.job) == false) equip = true;
                    if (equip)
                    {
                        if (CheckNotEquipLevel()) break;
                        //���蕐�푕�����Ă�����
                        if (myStatus.MainHand != null && myStatus.MainHand.itemType == Item.ItemType.TwoHand)
                        {
                            inventory.ItemAdd(myStatus.MainHand);
                            FromEquipToInventoryEquippedIcon(myStatus.MainHand, 0);
                        }
                        if (myStatus.OffHand != null) inventory.ItemAdd(myStatus.OffHand);
                        Destroy(draggingObj);
                        FromEquipToInventoryEquippedIcon(myStatus.OffHand, 1);
                        transform.SetParent(hit.gameObject.transform);
                        transform.localPosition = new Vector2(0, 0);
                        EquipGear(thisGear, 1);
                        flg = false;
                    } 
                }
                else if (hit.gameObject.CompareTag("MainHand") && thisGear.itemType == Item.ItemType.TwoHand)
                {
                    bool equip = false;
                    //�ߐڃW���u�̂Ƃ�
                    if ((CheckJobType(myStatus.job) == true) &&
                        (thisGear.detailType != Item.DetailType.�����)) equip = true;
                    //�������W���u�̂Ƃ�
                    else if ((CheckJobType(myStatus.job) == false) &&
                        (thisGear.detailType == Item.DetailType.�����)) equip = true;
                    if (equip)
                    {
                        if (CheckNotEquipLevel()) break;
                        if (myStatus.MainHand != null) inventory.ItemAdd(myStatus.MainHand);
                        if (myStatus.OffHand != null) inventory.ItemAdd(myStatus.OffHand);
                        Destroy(draggingObj);
                        FromEquipToInventoryEquippedIcon(myStatus.MainHand, 0);
                        FromEquipToInventoryEquippedIcon(myStatus.OffHand, 1);
                        transform.SetParent(homeUIManager.equipSlots[0].transform);
                        transform.localPosition = new Vector2(0, 0);
                        EquipGear(thisGear, 0);
                        flg = false;
                    }
                }
                else if (hit.gameObject.CompareTag("Helm") && thisGear.detailType == Item.DetailType.�w����)
                {
                    if (CheckNotEquipLevel()) break;
                    if (myStatus.Helm != null) inventory.ItemAdd(myStatus.Helm);
                    Destroy(draggingObj);
                    FromEquipToInventoryEquippedIcon(myStatus.Helm, 2);
                    transform.SetParent(hit.gameObject.transform);
                    transform.localPosition = new Vector2(0, 0);
                    EquipGear(thisGear, 2);
                    flg = false;
                }
                else if (hit.gameObject.CompareTag("BodyArmor") && thisGear.detailType == Item.DetailType.�{�f�B�A�[�}�[)
                {
                    if (CheckNotEquipLevel()) break;
                    if (myStatus.BodyArmor != null) inventory.ItemAdd(myStatus.BodyArmor);
                    Destroy(draggingObj);
                    FromEquipToInventoryEquippedIcon(myStatus.BodyArmor, 3);
                    transform.SetParent(hit.gameObject.transform);
                    transform.localPosition = new Vector2(0, 0);
                    EquipGear(thisGear, 3);
                    flg = false;
                }
                else if (hit.gameObject.CompareTag("Gauntlet") && thisGear.detailType == Item.DetailType.�K���g���b�g)
                {
                    if (CheckNotEquipLevel()) break;
                    if (myStatus.Gauntlet != null) inventory.ItemAdd(myStatus.Gauntlet);
                    Destroy(draggingObj);
                    FromEquipToInventoryEquippedIcon(myStatus.Gauntlet, 4);
                    transform.SetParent(hit.gameObject.transform);
                    transform.localPosition = new Vector2(0, 0);
                    EquipGear(thisGear, 4);
                    flg = false;
                }
                else if (hit.gameObject.CompareTag("LegArmor") && thisGear.detailType == Item.DetailType.���b�O�A�[�}�[)
                {
                    if (CheckNotEquipLevel()) break;
                    if (myStatus.LegArmor != null) inventory.ItemAdd(myStatus.LegArmor);
                    Destroy(draggingObj);
                    FromEquipToInventoryEquippedIcon(myStatus.LegArmor, 5);
                    transform.SetParent(hit.gameObject.transform);
                    transform.localPosition = new Vector2(0, 0);
                    EquipGear(thisGear, 5);
                    flg = false;
                }
                else if (hit.gameObject.CompareTag("RightAccessory") && thisGear.detailType == Item.DetailType.�A�N�Z�T���[)
                {
                    if (CheckNotEquipLevel()) break;
                    if (myStatus.RightAccessory != null) inventory.ItemAdd(myStatus.RightAccessory);
                    Destroy(draggingObj);
                    FromEquipToInventoryEquippedIcon(myStatus.RightAccessory, 6);
                    transform.SetParent(hit.gameObject.transform);
                    transform.localPosition = new Vector2(0, 0);
                    EquipGear(thisGear, 6);
                    flg = false;
                }
                else if (hit.gameObject.CompareTag("LeftAccessory") && thisGear.detailType == Item.DetailType.�A�N�Z�T���[)
                {
                    if (CheckNotEquipLevel()) break;
                    if (myStatus.LeftAccessory != null) inventory.ItemAdd(myStatus.LeftAccessory);
                    Destroy(draggingObj);
                    FromEquipToInventoryEquippedIcon(myStatus.LeftAccessory, 7);
                    transform.SetParent(hit.gameObject.transform);
                    transform.localPosition = new Vector2(0, 0);
                    EquipGear(thisGear, 7);
                    flg = false;
                }
                else if (hit.gameObject.CompareTag("SellPanel") && this.transform.parent.gameObject.CompareTag("Inventory"))
                {
                    transform.SetParent(homeUIManager.sellContent.transform);
                    homeUIManager.totalSellMoney += thisGear.Money;
                    int money = (homeUIManager.totalSellMoney > 999999999) ? 999999999 : homeUIManager.totalSellMoney;
                    homeUIManager.sellMoneyText.text = $"{money:N0}";
                }
                else if (hit.gameObject.CompareTag("Inventory") && this.transform.parent.gameObject.CompareTag("SellPanel"))
                {
                    //�A�C�R�����C���x���g����
                    if (thisGear.itemType == Item.ItemType.MainHand || thisGear.itemType == Item.ItemType.OffHand || thisGear.itemType == Item.ItemType.TwoHand)
                    {
                        this.transform.SetParent(homeUIManager.forSellWeaponContent.transform);
                        this.transform.SetSiblingIndex(inventory.WeaponInventory.IndexOf(thisGear));
                    }
                    else if (thisGear.itemType == Item.ItemType.Armor)
                    {
                        this.transform.SetParent(homeUIManager.forSellArmorContent.transform);
                        this.transform.SetSiblingIndex(inventory.ArmorInventory.IndexOf(thisGear));
                    }
                    else if (thisGear.itemType == Item.ItemType.Accessory)
                    {
                        this.transform.SetParent(homeUIManager.forSellAccessoryContent.transform);
                        this.transform.SetSiblingIndex(inventory.AccessoryInventory.IndexOf(thisGear));
                    }
                    homeUIManager.totalSellMoney -= thisGear.Money;
                    int money = (homeUIManager.totalSellMoney > 999999999) ? 999999999 : homeUIManager.totalSellMoney;
                    homeUIManager.sellMoneyText.text = $"{money:N0}";
                }
            }
            else
            {
                //�����Ґ�����C���x���g����
                if (hit.gameObject.CompareTag("Inventory"))
                {
                    Destroy(draggingObj);
                    inventory.ItemAdd(thisGear);
                    //������null��
                    if (transform.parent.gameObject.CompareTag("MainHand"))
                    {
                        myStatus.MainHand = null;
                        flg = false;

                    }
                    else if (transform.parent.gameObject.CompareTag("OffHand"))
                    {
                        myStatus.OffHand = null;
                        flg = false;
                    }
                    else if (transform.parent.gameObject.CompareTag("Helm"))
                    {
                        myStatus.Helm = null;
                        flg = false;
                    }
                    else if (transform.parent.gameObject.CompareTag("BodyArmor"))
                    {
                        myStatus.BodyArmor = null;
                        flg = false;
                    }
                    else if (transform.parent.gameObject.CompareTag("Gauntlet"))
                    {
                        myStatus.Gauntlet = null;
                        flg = false;
                    }
                    else if (transform.parent.gameObject.CompareTag("LegArmor"))
                    {
                        myStatus.LegArmor = null;
                        flg = false;
                    }
                    else if (transform.parent.gameObject.CompareTag("RightAccessory"))
                    {
                        myStatus.RightAccessory = null;
                        flg = false;
                    }
                    else if (transform.parent.gameObject.CompareTag("LeftAccessory"))
                    {
                        myStatus.LeftAccessory = null;
                        flg = false;
                    }
                    //�A�C�R�����C���x���g����
                    if (thisGear.itemType == Item.ItemType.MainHand || thisGear.itemType == Item.ItemType.OffHand || thisGear.itemType == Item.ItemType.TwoHand)
                    {
                        this.transform.SetParent(homeUIManager.weaponContent.transform);
                        this.transform.SetSiblingIndex(inventory.WeaponInventory.IndexOf(thisGear));
                    }
                    else if(thisGear.itemType == Item.ItemType.Armor)
                    {
                        this.transform.SetParent(homeUIManager.armorContent.transform);
                        this.transform.SetSiblingIndex(inventory.ArmorInventory.IndexOf(thisGear));
                    }
                    else if(thisGear.itemType == Item.ItemType.Accessory)
                    {
                        this.transform.SetParent(homeUIManager.accessoryContent.transform);
                        this.transform.SetSiblingIndex(inventory.AccessoryInventory.IndexOf(thisGear));
                    }                     
                }
            }
        }
        if (flg)
        {
            frame.color = Color.white;
            gearImage.color = Color.white;
            Destroy(draggingObj);
        }
        else
        {
            frame.color = Color.white;
            gearImage.color = Color.white;
            //myStatus.SetStatus();
            homeUIManager.UpdateStatusText(homeUIManager.statusMiniTexts);
            homeUIManager.UpdateInventoryCountTexts();
        }
    }

    //���������\��
    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateGearInfoTexts();
        homeUIManager.gearInfoBGs[0].GetComponent<FitTextSize>().FitSize();
        homeUIManager.gearInfoBGs[1].GetComponent<FitTextSize>().FitSize();
        homeUIManager.gearInfoBGs[2].GetComponent<FitTextSize>().FitSize();
        float posi_y = (this.gameObject.GetComponent<RectTransform>().position.y - TextsMaxHeight() < 50) ? 50 + TextsMaxHeight() : this.gameObject.GetComponent<RectTransform>().position.y;
        if (!drag)
        {
            homeUIManager.gearInfoPanelRectTrance.position =
               new Vector3(this.gameObject.GetComponent<RectTransform>().position.x + 100,
               posi_y, 0);
        }
        else if (thisGear.Equipped)
        {
            homeUIManager.gearInfoPanelRectTrance.position = 
                new Vector3(this.gameObject.GetComponent<RectTransform>().position.x - 400, posi_y, 0);
        }
        else
        {
            homeUIManager.gearInfoPanelRectTrance.position =
                new Vector3(this.gameObject.GetComponent<RectTransform>().position.x + 100,
                posi_y, 0);
        }
        posi_y = 0;
    }
    //����������\��
    public void OnPointerExit(PointerEventData eventData)
    {
        homeUIManager.gearInfoPanelRectTrance.localPosition = new Vector3(-1448, 0, 0);//��ʓ��̍���
        homeUIManager.gearInfoBGs[0].GetComponent<FitTextSize>().FitOutSize();
        homeUIManager.gearInfoBGs[1].GetComponent<FitTextSize>().FitOutSize();
        homeUIManager.gearInfoBGs[2].GetComponent<FitTextSize>().FitOutSize();
        homeUIManager.gearInfoBGs[1].SetActive(false);
        homeUIManager.gearInfoBGs[2].SetActive(false);
    }
    
    private void Start()
    {
        myCharacterStatus = GameObject.Find("MyCharacterStatus"); // ��������MyCharacterStatus���擾
        myStatus = myCharacterStatus.GetComponent<MyCharacterStatus>(); // script���擾
        inventoryObj = GameObject.Find("Inventory");
        inventory = inventoryObj.GetComponent<Inventory>();
        homeUIManagerObj = GameObject.Find("HomeUIManager");
        homeUIManager = homeUIManagerObj.GetComponent<HomeUIManager>();
        homeUIManager.gearInfoBGs[1].SetActive(false);
        homeUIManager.gearInfoBGs[2].SetActive(false);
    }
    public void EquipGear(Item iconItem, int part)
        {
            switch (part)
            {
                case 0:
                    iconItem.Equipped = true;
                    myStatus.MainHand = iconItem;
                    if(iconItem.itemType == Item.ItemType.TwoHand) myStatus.OffHand = null;
                    RemoveInventoryItem(iconItem);
                    break;
                case 1:
                iconItem.Equipped = true;
                myStatus.OffHand = iconItem;
                if (myStatus.MainHand != null && myStatus.MainHand.itemType == Item.ItemType.TwoHand) myStatus.MainHand = null;
                RemoveInventoryItem(iconItem);
                    break;
                case 2:
                iconItem.Equipped = true;
                myStatus.Helm = iconItem;
                    RemoveInventoryItem(iconItem);
                    break;
                case 3:
                iconItem.Equipped = true;
                myStatus.BodyArmor = iconItem;
                    RemoveInventoryItem(iconItem);
                    break;
                case 4:
                iconItem.Equipped = true;
                myStatus.Gauntlet = iconItem;
                RemoveInventoryItem(iconItem);
                    break;
                case 5:
                iconItem.Equipped = true;
                myStatus.LegArmor = iconItem;
                    RemoveInventoryItem(iconItem);
                    break;
                case 6:
                iconItem.Equipped = true;
                myStatus.RightAccessory = iconItem;
                    RemoveInventoryItem(iconItem);
                    break;
                case 7:
                iconItem.Equipped = true;
                myStatus.LeftAccessory = iconItem;
                    RemoveInventoryItem(iconItem);
                    break;
                default:
                    break;
            }
        }
    /// <summary>
    /// �������C���x���g������폜
    /// </summary>
    public void RemoveInventoryItem(Item iconItem)
        {
            switch (iconItem.itemType)
            {
                case Item.ItemType.MainHand:
                case Item.ItemType.OffHand:
                case Item.ItemType.TwoHand:
                    inventory.WeaponInventory.Remove(iconItem);
                    break;
                case Item.ItemType.Armor:
                    inventory.ArmorInventory.Remove(iconItem);
                    break;
                case Item.ItemType.Accessory:
                    inventory.AccessoryInventory.Remove(iconItem);
                    break;
                default:
                    break;
            }
        }
    //�������̑������C���x���g���Ɉڂ�
    public void FromEquipToInventoryEquippedIcon(Item myEquipGear, int i)
    {
        if(myEquipGear != null)
        {
            GameObject tobj = homeUIManager.equipSlots[i].transform.GetChild(1).gameObject;
            switch (i)
            {
                case 0:
                case 1:
                    tobj.transform.SetParent(homeUIManager.weaponContent.transform);
                    tobj.transform.SetSiblingIndex(inventory.WeaponInventory.IndexOf(myEquipGear));
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                    tobj.transform.SetParent(homeUIManager.armorContent.transform);
                    tobj.transform.SetSiblingIndex(inventory.ArmorInventory.IndexOf(myEquipGear));
                    break;
                case 6:
                case 7:
                    tobj.transform.SetParent(homeUIManager.accessoryContent.transform);
                    tobj.transform.SetSiblingIndex(inventory.AccessoryInventory.IndexOf(myEquipGear));
                    break;
                default:
                    break;
            }
        }
    }

    public List<string> GetGearInfoText(Item item)
    {
        List<object> list = new()
        {
            item.Hp,
            item.HpRate,
            item.Mp,
            item.MpRate,
            item.Atk,
            item.AtkRate,
            item.Def,
            item.DefRate,
            item.ArmorUp,//
            item.ArmorRate,//
            //�����_���[�W,�����␳�l(100%~)
            item.PhisicalDmg,item.PhisicalRate,
            item.FireDmg,item.FireRate,
            item.IceDmg,item.IceRate,
            item.ThunderDmg,item.ThunderRate,
            item.WindDmg,item.WindRate,
            item.ShiningDmg,item.ShiningRate,
            item.DarknessDmg,item.DarknessRate,         
            //��R�l(0~80%)
            item.PhisicalResist,
            item.FireResist,
            item.IceResist,
            item.ThunderResist,
            item.WindResist,
            item.ShiningResist,
            item.DarknessResist,
            //���̑�
            item.SpdRate,
            item.AutoHpRecover,
            item.AutoHpRecoverRate,//
            item.AutoMpRecover,
            item.AutoMpRecoverRate,//
            item.ExpGetRate,
            
        };
        List<string> textList = new();
        for(int i=0; i<list.Count; i++)
        {
            if(list[i].ToString() != "0")
            {
                switch (i)
                {
                    case 0:
                        textList.Add($"HP +{list[i]}");
                        break;
                    case 1:
                        textList.Add($"HP +{list[i]}%");
                        break;
                    case 2:
                        textList.Add($"MP +{list[i]}");
                        break;
                    case 3:
                        textList.Add($"MP +{list[i]}%");
                        break;
                    case 4:
                        textList.Add($"�U���\�� +{list[i]}");
                        break;
                    case 5:
                        textList.Add($"�U���\�� +{list[i]}%");
                        break;
                    case 6:
                        textList.Add($"�h��\�� +{list[i]}");
                        break;
                    case 7:
                        textList.Add($"�h��\�� +{list[i]}%");
                        break;
                    case 8:
                        textList.Add($"���b�l +{list[i]}");
                        break;
                    case 9:
                        textList.Add($"���b�l +{list[i]}%");
                        break;
                    case 10:
                        textList.Add($"�����_���[�W +{list[i]}");
                        break;
                    case 11:
                        textList.Add($"�����␳ +{list[i]}%");
                        break;
                    case 12:
                        textList.Add($"���_���[�W +{list[i]}");
                        break;
                    case 13:
                        textList.Add($"���␳ +{list[i]}%");
                        break;
                    case 14:
                        textList.Add($"�X�_���[�W +{list[i]}");
                        break;
                    case 15:
                        textList.Add($"�X�␳ +{list[i]}%");
                        break;
                    case 16:
                        textList.Add($"���_���[�W +{list[i]}");
                        break;
                    case 17:
                        textList.Add($"���␳ +{list[i]}%");
                        break;
                    case 18:
                        textList.Add($"���_���[�W +{list[i]}");
                        break;
                    case 19:
                        textList.Add($"���␳ +{list[i]}%");
                        break;
                    case 20:
                        textList.Add($"���_���[�W +{list[i]}");
                        break;
                    case 21:
                        textList.Add($"���␳ +{list[i]}%");
                        break;
                    case 22:
                        textList.Add($"�Ń_���[�W +{list[i]}");
                        break;
                    case 23:
                        textList.Add($"�ŕ␳ +{list[i]}%");
                        break;
                    case 24:
                        textList.Add($"�����ϐ� +{list[i]}%");
                        break;
                    case 25:
                        textList.Add($"���ϐ� +{list[i]}%");
                        break;
                    case 26:
                        textList.Add($"�X�ϐ� +{list[i]}%");
                        break;
                    case 27:
                        textList.Add($"���ϐ� +{list[i]}%");
                        break;
                    case 28:
                        textList.Add($"���ϐ� +{list[i]}%");
                        break;
                    case 29:
                        textList.Add($"���ϐ� +{list[i]}%");
                        break;
                    case 30:
                        textList.Add($"�őϐ� +{list[i]}%");
                        break;
                    case 31:
                        textList.Add($"�U�����x�㏸ +{list[i]}%");
                        break;
                    case 32:
                        textList.Add($"����HP�� +{list[i]}");
                        break;
                    case 33:
                        textList.Add($"����HP�� +{list[i]}%");
                        break;
                    case 34:
                        textList.Add($"����MP�� +{list[i]}");
                        break;
                    case 35:
                        textList.Add($"����MP�� +{list[i]}%");
                        break;
                    case 36:
                        textList.Add($"�o���l���� +{list[i]}%");
                        break;
                    

                }
            }
        }
        return textList;
    }

    public void UpdateGearInfoTexts()
    {

        homeUIManager.gearInfoTexts[0].text = GearInfoText(thisGear);
        homeUIManager.gearInfoTexts[1].text = "";
        homeUIManager.gearInfoTexts[2].text = "";
        if (thisGear.Equipped) return; 
        if (thisGear.itemType == Item.ItemType.MainHand || thisGear.itemType == Item.ItemType.TwoHand)
        {
            if (myStatus.MainHand != null)
            {
                homeUIManager.gearInfoBGs[1].SetActive(true);
                homeUIManager.gearInfoTexts[1].text = GearInfoText(myStatus.MainHand);
            }
                 
            if (myStatus.OffHand != null && thisGear.itemType == Item.ItemType.TwoHand)
            {
                homeUIManager.gearInfoBGs[2].SetActive(true);
                homeUIManager.gearInfoTexts[2].text = GearInfoText(myStatus.OffHand);
            }
        }
        else if(thisGear.itemType == Item.ItemType.OffHand)
        {
            if (myStatus.OffHand != null)
            {
                homeUIManager.gearInfoBGs[1].SetActive(true);
                homeUIManager.gearInfoTexts[1].text = GearInfoText(myStatus.OffHand);
            }
        }
        else if (thisGear.detailType == Item.DetailType.�w����)
        {
            if (myStatus.Helm != null)
            {
                homeUIManager.gearInfoBGs[1].SetActive(true);
                homeUIManager.gearInfoTexts[1].text = GearInfoText(myStatus.Helm);
            }
        }
        else if (thisGear.detailType == Item.DetailType.�{�f�B�A�[�}�[)
        {
            if (myStatus.BodyArmor != null)
            {
                homeUIManager.gearInfoBGs[1].SetActive(true);
                homeUIManager.gearInfoTexts[1].text = GearInfoText(myStatus.BodyArmor);
            }
        }
        else if (thisGear.detailType == Item.DetailType.�K���g���b�g)
        {
            if (myStatus.Gauntlet != null)
            {
                homeUIManager.gearInfoBGs[1].SetActive(true);
                homeUIManager.gearInfoTexts[1].text = GearInfoText(myStatus.Gauntlet);
            }
        }
        else if (thisGear.detailType == Item.DetailType.���b�O�A�[�}�[)
        {
            if (myStatus.LegArmor != null)
            {
                homeUIManager.gearInfoBGs[1].SetActive(true);
                homeUIManager.gearInfoTexts[1].text = GearInfoText(myStatus.LegArmor);
            }
        }
        else if (thisGear.itemType == Item.ItemType.Accessory)
        {
            if (myStatus.RightAccessory != null)
            {
                homeUIManager.gearInfoBGs[1].SetActive(true);
                homeUIManager.gearInfoTexts[1].text = GearInfoText(myStatus.RightAccessory);
            }
            if (myStatus.LeftAccessory != null)
            {
                homeUIManager.gearInfoBGs[2].SetActive(true);
                homeUIManager.gearInfoTexts[2].text = GearInfoText(myStatus.LeftAccessory);
            }
        }

    }

    public string GearInfoText(Item item)
    {
        string rareColor;
        string rareName;
        switch (item.Rare)
        {
            case 0:
                rareColor = "#cccccc";
                rareName = "�R����";
                break;
            case 1:
                rareColor = "#5ae05a";
                rareName = "���A";
                break;
            case 2:
                rareColor = "#5a9de0";
                rareName = "�G�s�b�N";
                break;
            case 3:
                rareColor = "#9d5ae0";
                rareName = "���W�F���_���[";
                break;
            default:
                rareColor = "false";
                rareName = "false";
                break;
        }
        if (rareColor == "false") return "";
        string topInfoText;
        switch (item.itemType)
        {
            case Item.ItemType.MainHand:
            case Item.ItemType.TwoHand:
                topInfoText = $"�b�ԍU���� {item.Spd}\n";
                break;
            case Item.ItemType.OffHand:
                topInfoText = $"�K�[�h�m�� {item.GuardRate}%\n";
                break;
            case Item.ItemType.Armor:
                topInfoText = $"���b�l {item.ArmorPoint}\n";
                break;
            case Item.ItemType.Accessory:
                topInfoText = "";
                break;
            default:
                topInfoText = "false";
                break;
        }
        if (topInfoText == "false") return "";
        string posi = (item.Equipped) ? "���݂̑���" : "�C���x���g���̑���";
        return $"<line-height=57%><size=70%>{posi}</size>\n<color={rareColor}>{item.Name}\n</color><line-height=50%><size=75%>{rareName} {item.detailType}\n{topInfoText}<line-height=25%>\n<line-height=65%>" + string.Join("\n", GetGearInfoText(item)) + $"</size>\n<size=65%>�����\���x��: {item.Level}";
    }

    public float TextsMaxHeight()
    {
        float max = (homeUIManager.gearInfoTexts[0].preferredHeight >= homeUIManager.gearInfoTexts[1].preferredHeight) ? homeUIManager.gearInfoTexts[0].preferredHeight : homeUIManager.gearInfoTexts[1].preferredHeight;
        max = (max >= homeUIManager.gearInfoTexts[2].preferredHeight) ? max : homeUIManager.gearInfoTexts[2].preferredHeight;
        return max;
    }

    public void NotEquipLevel()
    {
        if (myStatus.Level <= thisGear.Level)
        {
            return;
        }
    }
    
    public void SetItem(Item item)
        {
            thisGear = item;
        }
    /// <summary>
    /// �ߐڃW���u���������W���u���̃`�F�b�N���s��
    /// </summary>
    /// <param name="job"></param>
    /// <returns>�ߐڂȂ�True�A�������Ȃ�Flase</returns>
    public bool CheckJobType(MyCharacterStatus.Job job)
    {
        switch ((int)job)
        {
            case 0:
            case 1:
            case 2:
                return true;
            case 3:
            case 4:
            case 5:
                return false;
            default:
                return true;
        }
    }

    public bool CheckNotEquipLevel()
    {
        if (myStatus.Level < thisGear.Level)
        {
            Debug.Log("�����\���x���ɒB���Ă��܂���");
            return true;
        }
        else return false;  
    }
}
