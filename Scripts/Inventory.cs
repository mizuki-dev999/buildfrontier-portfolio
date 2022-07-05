using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public List<Item> WeaponInventory = new(); // 武器インベントリ(List)
    public List<Item> ArmorInventory = new();　// 防具インベントリ(List)
    public List<Item> AccessoryInventory = new(); // アクセインベントリ(List)
    public int maxNum = 100; // 種類ごとのアイテム所持上限数
    public static Inventory singleton;
    [System.NonSerialized] public MyCharacterStatus myStatus; // 自キャラ情報のスクリプト

    //シングルトンにする
    void Awake()
    {
        //　スクリプトが設定されていなければゲームオブジェクトを残しつつスクリプトを設定
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
        myStatus = GameObject.Find("MyCharacterStatus").GetComponent<MyCharacterStatus>(); // scriptを取得
    }

    /// <summary>
    /// アイテムを各種インベントリ(List)に加える
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
    /// 選ばれたアイテムIDからアイテムを生成→アイテムを各種インベントリに追加
    /// </summary>
    public void ItemDropAndGet(ItemDatabase.ItemData dropItem)
    {
        int lv = Mathf.Clamp(UnityEngine.Random.Range(myStatus.Level, myStatus.Level + 5), 0, 100);
        //アイテムを生成
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
            // 基礎パラメータ
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
            //属性補正値(%)
            PhisicalRate = GetRandomStatus(dropItem.PhisicalRate, lv),
            FireRate = GetRandomStatus(dropItem.FireRate, lv),
            IceRate = GetRandomStatus(dropItem.IceRate, lv),
            ThunderRate = GetRandomStatus(dropItem.ThunderRate, lv),
            WindRate = GetRandomStatus(dropItem.WindRate, lv),
            ShiningRate = GetRandomStatus(dropItem.ShiningRate, lv),
            DarknessRate = GetRandomStatus(dropItem.DarknessRate, lv),
            //抵抗値
            PhisicalResist = Mathf.Clamp(GetRandomStatus(dropItem.PhisicalResist, (lv - 1) / 10 + 1), 0, 80),
            FireResist = Math.Clamp(GetRandomStatus(dropItem.FireResist, (lv - 1) / 10 + 1), 0, 80),
            IceResist = Math.Clamp(GetRandomStatus(dropItem.IceResist, (lv - 1) / 10 + 1), 0, 80),
            ThunderResist = Math.Clamp(GetRandomStatus(dropItem.ThunderResist, (lv - 1) / 10 + 1), 0, 80),
            WindResist = Math.Clamp(GetRandomStatus(dropItem.WindResist, (lv - 1) / 10 + 1), 0, 10),
            ShiningResist = Math.Clamp(GetRandomStatus(dropItem.ShiningResist, (lv - 1) / 10 + 1), 0, 80),
            DarknessResist = Math.Clamp(GetRandomStatus(dropItem.DarknessResist, (lv - 1) / 10 + 1), 0, 80),
            //その他
            SpdRate = Math.Clamp(GetRandomStatus(dropItem.SpdRate, (lv - 1) / 10 + 1), 0, 100),
            AutoHpRecover = GetRandomStatus(dropItem.AutoHpRecover, lv),
            AutoHpRecoverRate = GetRandomStatus(dropItem.AutoHpRecoverRate, lv),
            AutoMpRecover = GetRandomStatus(dropItem.AutoMpRecover, lv),
            AutoMpRecoverRate = GetRandomStatus(dropItem.AutoMpRecoverRate, lv),
            GuardRate = Math.Clamp(GetRandomStatus(dropItem.GuardRate, (lv - 1) / 10 + 1), 0, 80),
            ExpGetRate = GetRandomStatus(dropItem.ExpGetRate, (lv - 1) / 10 + 1),
        };
        //アイテムをインベントリに追加
        ItemAdd(item);
    }

    /// <summary>
    /// ItemのItemTypeに変換する
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
    /// ItemのDetailTypeに変換する
    /// </summary>
    public Item.DetailType GetDetailType(ItemDatabase.ItemData.DetailType detailType)
    {
        return detailType switch
        {
            ItemDatabase.ItemData.DetailType.片手剣 => Item.DetailType.片手剣,
            ItemDatabase.ItemData.DetailType.片手斧 => Item.DetailType.片手斧,
            ItemDatabase.ItemData.DetailType.片手メイス => Item.DetailType.片手メイス,
            ItemDatabase.ItemData.DetailType.ダガー => Item.DetailType.ダガー,
            ItemDatabase.ItemData.DetailType.片手杖 => Item.DetailType.片手杖,
            ItemDatabase.ItemData.DetailType.両手剣 => Item.DetailType.両手剣,
            ItemDatabase.ItemData.DetailType.両手斧 => Item.DetailType.両手斧,
            ItemDatabase.ItemData.DetailType.両手メイス => Item.DetailType.両手メイス,
            ItemDatabase.ItemData.DetailType.スピア => Item.DetailType.スピア,
            ItemDatabase.ItemData.DetailType.両手杖 => Item.DetailType.両手杖,
            ItemDatabase.ItemData.DetailType.盾 => Item.DetailType.盾,
            ItemDatabase.ItemData.DetailType.魔道具 => Item.DetailType.魔道具,
            ItemDatabase.ItemData.DetailType.ヘルム => Item.DetailType.ヘルム,
            ItemDatabase.ItemData.DetailType.ボディアーマー => Item.DetailType.ボディアーマー,
            ItemDatabase.ItemData.DetailType.ガントレット => Item.DetailType.ガントレット,
            ItemDatabase.ItemData.DetailType.レッグアーマー => Item.DetailType.レッグアーマー,
            ItemDatabase.ItemData.DetailType.アクセサリー => Item.DetailType.アクセサリー,
            _ => 0,
        };
    }

    // ステータスにランダム性を持たせる
    public int GetRandomStatus(int status, int lv)
    {
        status *= lv;
        return status + UnityEngine.Random.Range(0,status/2+1);
    }

    public float GetSpd(ItemDatabase.ItemData.DetailType detailType)
    {
        var spd = detailType switch
        {
            ItemDatabase.ItemData.DetailType.片手剣 => 1.8f,
            ItemDatabase.ItemData.DetailType.片手斧 => 1.6f,
            ItemDatabase.ItemData.DetailType.片手メイス => 1.7f,
            ItemDatabase.ItemData.DetailType.ダガー => 1.9f,
            ItemDatabase.ItemData.DetailType.片手杖 => 1.8f,
            ItemDatabase.ItemData.DetailType.両手剣 => 1.5f,
            ItemDatabase.ItemData.DetailType.両手斧 => 1.3f,
            ItemDatabase.ItemData.DetailType.両手メイス => 1.4f,
            ItemDatabase.ItemData.DetailType.スピア => 1.6f,
            ItemDatabase.ItemData.DetailType.両手杖 => 1.6f,
            _ => 0,
        };
        if (spd != 0) spd += (float)Math.Round(UnityEngine.Random.value / 10, 2, MidpointRounding.AwayFromZero);
        return spd;
    }
}
