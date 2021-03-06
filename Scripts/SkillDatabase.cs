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
            pbVuXL,
            ANeBuXL,
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
                UXL,
                HPñXL,
                MPñXL,
                CtXeB[,
                }iXeB[,
                HP,
                HPâ³,
                MP,
                MPâ³,
                U\Í,
                U\Íâ³,
                hä\Í,
                hä\Íâ³,
                bl,
                blâ³,
                ¨_[W,
                ¨â³,
                _[W,
                â³,
                X_[W,
                Xâ³,
                _[W,
                â³,
                _[W,
                â³,
                õ_[W,
                õâ³,
                Å_[W,
                Åâ³,
                ¨Ï«,
                Ï«,
                XÏ«,
                Ï«,
                Ï«,
                õÏ«,
                ÅÏ«,
                U¬x,
                ©®HPñ,
                ©®HPñâ³,
                ©®MPñ,
                ©®MPñâ³,
                K[hm¦, 
            }

            public EffectType effectType;
            public int target; //0:©ª 1:G
            public int EffectAmount;
        }
    }
}
