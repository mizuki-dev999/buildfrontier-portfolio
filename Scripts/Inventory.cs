using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public List<Item> WeaponInventory = new(); // ����C���x���g��(List)
    public List<Item> ArmorInventory = new();�@// �h��C���x���g��(List)
    public List<Item> AccessoryInventory = new(); // �A�N�Z�C���x���g��(List)
    public int maxNum = 100; // ��ނ��Ƃ̃A�C�e�����������
    public static Inventory singleton;
    [System.NonSerialized] public MyCharacterStatus myStatus; // ���L�������̃X�N���v�g

    //�V���O���g���ɂ���
    void Awake()
    {
        //�@�X�N���v�g���ݒ肳��Ă��Ȃ���΃Q�[���I�u�W�F�N�g���c���X�N���v�g��ݒ�
        if (singleton == null)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        myStatus = GameObject.Find("MyCharacterStatus").GetComponent<MyCharacterStatus>(); // script���擾
    }

    /// <summary>
    /// �A�C�e�����e��C���x���g��(List)�ɉ�����
    /// </summary>
    public void ItemAdd(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.MainHand:
            case Item.ItemType.OffHand:
            case Item.ItemType.TwoHand:
                item.Equipped = false;
                WeaponInventory.Add(item);
                WeaponInventory.Sort((a, b) => b.Rare - a.Rare);
                break;
            case Item.ItemType.Armor:
                item.Equipped = false;
                ArmorInventory.Add(item);
                ArmorInventory.Sort((a, b) => b.Rare - a.Rare);
                break;
            case Item.ItemType.Accessory:
                item.Equipped = false;
                AccessoryInventory.Add(item);
                AccessoryInventory.Sort((a, b) => b.Rare - a.Rare);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// �I�΂ꂽ�A�C�e��ID����A�C�e���𐶐����A�C�e�����e��C���x���g���ɒǉ�
    /// </summary>
    public void ItemDropAndGet(ItemDatabase.ItemData dropItem)
    {
        int lv = Mathf.Clamp(UnityEngine.Random.Range(myStatus.Level, myStatus.Level + 5), 0, 100);
        //�A�C�e���𐶐�
        Item item = new()
        {
            Name = dropItem.Name,
            Id = dropItem.Id,
            Rare = dropItem.Rare,
            Level = lv,
            Money =  lv * 50,
            Equipped = false,
            New = false,
            itemType = GetItemType(dropItem.itemType),
            detailType = GetDetailType(dropItem.detailType),
            sprite = dropItem.sprite,
            // ��b�p�����[�^
            Hp = GetRandomStatus(dropItem.Hp, lv),
            HpRate = GetRandomStatus(dropItem.HpRate, lv),
            Mp = GetRandomStatus(dropItem.Mp,lv),
            MpRate = GetRandomStatus(dropItem.MpRate, lv),
            Atk = GetRandomStatus(dropItem.Atk, lv),
            AtkRate = GetRandomStatus(dropItem.AtkRate, lv),
            Def = GetRandomStatus(dropItem.Def, lv),
            DefRate = GetRandomStatus(dropItem.DefRate, lv),
            Spd = GetSpd(dropItem.detailType),
            ArmorPoint = GetRandomStatus(dropItem.ArmorPoint, lv),
            ArmorUp = GetRandomStatus(dropItem.ArmorUp, lv),
            ArmorRate = GetRandomStatus(dropItem.ArmorRate, lv),
            PhisicalDmg = GetRandomStatus(dropItem.PhisicalDmg, lv),
            FireDmg = GetRandomStatus(dropItem.FireDmg, lv),
            IceDmg = GetRandomStatus(dropItem.IceDmg, lv),
            ThunderDmg = GetRandomStatus(dropItem.ThunderDmg, lv),
            WindDmg = GetRandomStatus(dropItem.WindDmg, lv),
            ShiningDmg = GetRandomStatus(dropItem.ShiningDmg, lv),
            DarknessDmg = GetRandomStatus(dropItem.DarknessDmg, lv),
            //�����␳�l(%)
            PhisicalRate = GetRandomStatus(dropItem.PhisicalRate, lv),
            FireRate = GetRandomStatus(dropItem.FireRate, lv),
            IceRate = GetRandomStatus(dropItem.IceRate, lv),
            ThunderRate = GetRandomStatus(dropItem.ThunderRate, lv),
            WindRate = GetRandomStatus(dropItem.WindRate, lv),
            ShiningRate = GetRandomStatus(dropItem.ShiningRate, lv),
            DarknessRate = GetRandomStatus(dropItem.DarknessRate, lv),
            //��R�l
            PhisicalResist = Mathf.Clamp(GetRandomStatus(dropItem.PhisicalResist, (lv - 1) / 10 + 1), 0, 80),
            FireResist = Math.Clamp(GetRandomStatus(dropItem.FireResist, (lv - 1) / 10 + 1), 0, 80),
            IceResist = Math.Clamp(GetRandomStatus(dropItem.IceResist, (lv - 1) / 10 + 1), 0, 80),
            ThunderResist = Math.Clamp(GetRandomStatus(dropItem.ThunderResist, (lv - 1) / 10 + 1), 0, 80),
            WindResist = Math.Clamp(GetRandomStatus(dropItem.WindResist, (lv - 1) / 10 + 1), 0, 10),
            ShiningResist = Math.Clamp(GetRandomStatus(dropItem.ShiningResist, (lv - 1) / 10 + 1), 0, 80),
            DarknessResist = Math.Clamp(GetRandomStatus(dropItem.DarknessResist, (lv - 1) / 10 + 1), 0, 80),
            //���̑�
            SpdRate = Math.Clamp(GetRandomStatus(dropItem.SpdRate, (lv - 1) / 10 + 1), 0, 100),
            AutoHpRecover = GetRandomStatus(dropItem.AutoHpRecover, lv),
            AutoHpRecoverRate = GetRandomStatus(dropItem.AutoHpRecoverRate, lv),
            AutoMpRecover = GetRandomStatus(dropItem.AutoMpRecover, lv),
            AutoMpRecoverRate = GetRandomStatus(dropItem.AutoMpRecoverRate, lv),
            GuardRate = Math.Clamp(GetRandomStatus(dropItem.GuardRate, (lv - 1) / 10 + 1), 0, 80),
            ExpGetRate = GetRandomStatus(dropItem.ExpGetRate, (lv - 1) / 10 + 1),
        };
        //�A�C�e�����C���x���g���ɒǉ�
        ItemAdd(item);
    }

    /// <summary>
    /// Item��ItemType�ɕϊ�����
    /// </summary>
    public Item.ItemType GetItemType(ItemDatabase.ItemData.ItemType itemType)
    {
        return itemType switch
        {
            ItemDatabase.ItemData.ItemType.MainHand => Item.ItemType.MainHand,
            ItemDatabase.ItemData.ItemType.OffHand => Item.ItemType.OffHand,
            ItemDatabase.ItemData.ItemType.TwoHand => Item.ItemType.TwoHand,
            ItemDatabase.ItemData.ItemType.Armor => Item.ItemType.Armor,
            ItemDatabase.ItemData.ItemType.Accessory => Item.ItemType.Accessory,
            _ => 0,
        };
    }

    /// <summary>
    /// Item��DetailType�ɕϊ�����
    /// </summary>
    public Item.DetailType GetDetailType(ItemDatabase.ItemData.DetailType detailType)
    {
        return detailType switch
        {
            ItemDatabase.ItemData.DetailType.�Ў茕 => Item.DetailType.�Ў茕,
            ItemDatabase.ItemData.DetailType.�Ў蕀 => Item.DetailType.�Ў蕀,
            ItemDatabase.ItemData.DetailType.�Ў胁�C�X => Item.DetailType.�Ў胁�C�X,
            ItemDatabase.ItemData.DetailType.�_�K�[ => Item.DetailType.�_�K�[,
            ItemDatabase.ItemData.DetailType.�Ў�� => Item.DetailType.�Ў��,
            ItemDatabase.ItemData.DetailType.���茕 => Item.DetailType.���茕,
            ItemDatabase.ItemData.DetailType.���蕀 => Item.DetailType.���蕀,
            ItemDatabase.ItemData.DetailType.���胁�C�X => Item.DetailType.���胁�C�X,
            ItemDatabase.ItemData.DetailType.�X�s�A => Item.DetailType.�X�s�A,
            ItemDatabase.ItemData.DetailType.����� => Item.DetailType.�����,
            ItemDatabase.ItemData.DetailType.�� => Item.DetailType.��,
            ItemDatabase.ItemData.DetailType.������ => Item.DetailType.������,
            ItemDatabase.ItemData.DetailType.�w���� => Item.DetailType.�w����,
            ItemDatabase.ItemData.DetailType.�{�f�B�A�[�}�[ => Item.DetailType.�{�f�B�A�[�}�[,
            ItemDatabase.ItemData.DetailType.�K���g���b�g => Item.DetailType.�K���g���b�g,
            ItemDatabase.ItemData.DetailType.���b�O�A�[�}�[ => Item.DetailType.���b�O�A�[�}�[,
            ItemDatabase.ItemData.DetailType.�A�N�Z�T���[ => Item.DetailType.�A�N�Z�T���[,
            _ => 0,
        };
    }

    // �X�e�[�^�X�Ƀ����_��������������
    public int GetRandomStatus(int status, int lv)
    {
        status *= lv;
        return status + UnityEngine.Random.Range(0,status/2+1);
    }

    public float GetSpd(ItemDatabase.ItemData.DetailType detailType)
    {
        var spd = detailType switch
        {
            ItemDatabase.ItemData.DetailType.�Ў茕 => 1.8f,
            ItemDatabase.ItemData.DetailType.�Ў蕀 => 1.6f,
            ItemDatabase.ItemData.DetailType.�Ў胁�C�X => 1.7f,
            ItemDatabase.ItemData.DetailType.�_�K�[ => 1.9f,
            ItemDatabase.ItemData.DetailType.�Ў�� => 1.8f,
            ItemDatabase.ItemData.DetailType.���茕 => 1.5f,
            ItemDatabase.ItemData.DetailType.���蕀 => 1.3f,
            ItemDatabase.ItemData.DetailType.���胁�C�X => 1.4f,
            ItemDatabase.ItemData.DetailType.�X�s�A => 1.6f,
            ItemDatabase.ItemData.DetailType.����� => 1.6f,
            _ => 0,
        };
        if (spd != 0) spd += (float)Math.Round(UnityEngine.Random.value / 10, 2, MidpointRounding.AwayFromZero);
        return spd;
    }
}
