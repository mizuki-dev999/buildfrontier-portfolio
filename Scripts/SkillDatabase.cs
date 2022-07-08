using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillDatabase : ScriptableObject
{
    public List<Skill> SkillDataList = new();

    [System.Serializable]
    public class Skill
    {
        public string Name;
        public int necessarySkillId;
        public int[] nextSkillId;
        public int MaxLevel;
        public enum SkillType
        {
            パッシブスキル,
            アクティブスキル,
        }
        public Sprite sprite;
        public SkillType type;
        public int Cost;
        public float CoolTime;
        public float EffectTime;
        public List<SkillEffect> skillEffects= new();

        [System.Serializable]
        public class SkillEffect
        {
            public enum EffectType
            {
                攻撃スキル,
                HP回復スキル,
                MP回復スキル,
                ライフスティール,
                マナスティール,
                HP,
                HP補正,
                MP,
                MP補正,
                攻撃能力,
                攻撃能力補正,
                防御能力,
                防御能力補正,
                装甲値,
                装甲値補正,
                物理ダメージ,
                物理補正,
                炎ダメージ,
                炎補正,
                氷ダメージ,
                氷補正,
                雷ダメージ,
                雷補正,
                風ダメージ,
                風補正,
                光ダメージ,
                光補正,
                闇ダメージ,
                闇補正,
                物理耐性,
                炎耐性,
                氷耐性,
                雷耐性,
                風耐性,
                光耐性,
                闇耐性,
                攻撃速度,
                自動HP回復,
                自動HP回復補正,
                自動MP回復,
                自動MP回復補正,
                ガード確率, 
            }

            public EffectType effectType;
            public int target; //0:自分 1:敵
            public int EffectAmount;
        }
    }
}
