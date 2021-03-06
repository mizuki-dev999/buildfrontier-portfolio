using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> ItemDataList = new();

    [System.Serializable]
    public class ItemData
    {
        // ACeÌ®«îñ
        public string Name;
        public int Id;
        public Sprite sprite;
        public int Rare;
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
            //Ðè
            Ðè,
            Ðè,
            ÐèCX,
            _K[,
            Ðèñ,
            //¼è
            ¼è,
            ¼è,
            ¼èCX,
            XsA,
            ¼èñ,
            //Itnh
            ,
            ¹ï,
            //hï
            w,
            {fBA[}[,
            Kgbg,
            bOA[}[,
            //ANZ
            ANZT[,
        }
        public DetailType detailType;

        // îbp[^
        public int Hp;
        public int HpRate;
        public int Mp;
        public int MpRate;
        public int Atk;
        public int AtkRate;
        public int Def;
        public int DefRate;
        public int ArmorPoint;
        public int ArmorUp;
        public int ArmorRate;

        //®«_[W
        public int PhisicalDmg = 0;
        public int FireDmg = 0;
        public int IceDmg = 0;
        public int ThunderDmg = 0;
        public int WindDmg = 0;
        public int ShiningDmg = 0;
        public int DarknessDmg = 0;
        //®«â³l(%)
        public int PhisicalRate = 0;
        public int FireRate = 0;
        public int IceRate = 0;
        public int ThunderRate = 0;
        public int WindRate = 0;
        public int ShiningRate = 0;
        public int DarknessRate = 0;
        //ïRl
        public int PhisicalResist = 0;
        public int FireResist = 0;
        public int IceResist = 0;
        public int ThunderResist = 0;
        public int WindResist = 0;
        public int ShiningResist = 0;
        public int DarknessResist = 0;
        //»Ì¼
        public int SpdRate; //U¬xâ³
        public int AutoHpRecover; //©®HPñ
        public int AutoHpRecoverRate; //©®HPñ(%)
        public int AutoMpRecover; // ©®MPñ
        public int AutoMpRecoverRate; // ©®MPñ(%)
        public int GuardRate; //K[hm¦
        public int ExpGetRate; // æ¾o±ÊÁ
    }
}
