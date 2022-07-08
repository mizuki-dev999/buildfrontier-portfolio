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
            �p�b�V�u�X�L��,
            �A�N�e�B�u�X�L��,
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
                �U���X�L��,
                HP�񕜃X�L��,
                MP�񕜃X�L��,
                ���C�t�X�e�B�[��,
                �}�i�X�e�B�[��,
                HP,
                HP�␳,
                MP,
                MP�␳,
                �U���\��,
                �U���\�͕␳,
                �h��\��,
                �h��\�͕␳,
                ���b�l,
                ���b�l�␳,
                �����_���[�W,
                �����␳,
                ���_���[�W,
                ���␳,
                �X�_���[�W,
                �X�␳,
                ���_���[�W,
                ���␳,
                ���_���[�W,
                ���␳,
                ���_���[�W,
                ���␳,
                �Ń_���[�W,
                �ŕ␳,
                �����ϐ�,
                ���ϐ�,
                �X�ϐ�,
                ���ϐ�,
                ���ϐ�,
                ���ϐ�,
                �őϐ�,
                �U�����x,
                ����HP��,
                ����HP�񕜕␳,
                ����MP��,
                ����MP�񕜕␳,
                �K�[�h�m��, 
            }

            public EffectType effectType;
            public int target; //0:���� 1:�G
            public int EffectAmount;
        }
    }
}
