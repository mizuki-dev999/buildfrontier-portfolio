using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    // アイテムの属性情報
    public string Name;
    public int Id;
    public Sprite sprite;
    public int Rare;
    public int Level;
    public int Money;
    public bool Equipped = false; //装備しているか
    public bool New = false; //入手されてから触ったか
    public enum ItemType
    {
        MainHand,
        OffHand,
        TwoHand,
        Armor,
        Accessory,
    }
    public ItemType itemType;
    public enum DetailType
    {
        //片手
        片手剣,
        片手斧,
        片手メイス,
        ダガー,
        片手杖,
        //両手
        両手剣,
        両手斧,
        両手メイス,
        スピア,
        両手杖,
        //オフハンド
        盾,
        魔道具,
        //防具
        ヘルム,
        ボディアーマー,
        ガントレット,
        レッグアーマー,
        //アクセ
        アクセサリー,
    }
    public DetailType detailType;

    // 基礎パラメータ
    public int Hp;
    public int HpRate;
    public int Mp;
    public int MpRate;
    public int Atk;
    public int AtkRate;
    public int Def;
    public int DefRate;
    public float Spd;
    public int ArmorPoint;
    public int ArmorUp;
    public int ArmorRate;

    //属性ダメージ
    public int PhisicalDmg = 0;
    public int FireDmg = 0;
    public int IceDmg = 0;
    public int ThunderDmg = 0;
    public int WindDmg = 0;
    public int ShiningDmg = 0;
    public int DarknessDmg = 0;
    //属性補正値(%)
    public int PhisicalRate = 0;
    public int FireRate = 0;
    public int IceRate = 0;
    public int ThunderRate = 0;
    public int WindRate = 0;
    public int ShiningRate = 0;
    public int DarknessRate = 0;
    //抵抗値
    public int PhisicalResist = 0;
    public int FireResist = 0;
    public int IceResist = 0;
    public int ThunderResist = 0;
    public int WindResist = 0;
    public int ShiningResist = 0;
    public int DarknessResist = 0;
    //その他
    public int SpdRate; //攻撃速度補正
    public int AutoHpRecover; //自動HP回復
    public int AutoHpRecoverRate; //自動HP回復(%)
    public int AutoMpRecover; // 自動MP回復
    public int AutoMpRecoverRate; // 自動MP回復(%)
    public int GuardRate; //ガード確率
    public int ExpGetRate; // 取得経験量増加
}
