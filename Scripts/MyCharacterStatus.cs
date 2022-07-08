using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KanKikuchi.AudioManager;

[System.Serializable]
public class MyCharacterStatus : MonoBehaviour
{
    static public MyCharacterStatus singleton;
    //----------------------------------------
    public string Name = "�e�X�g";
    public int Level = 100;
    public int maxLevel = 100;
    public int JPDuelist = 100;
    public int JPWarrior = 0;
    public int JPKnight = 0;
    public int JPWizard = 0;
    public int JPBishop = 0;
    public int JPMaiden = 0;
    public int Exp = 0;
    public int Money = 0;
    //����
    public Item MainHand = null;
    public Item OffHand = null;
    public Item Helm = null;
    public Item BodyArmor = null;
    public Item Gauntlet = null;
    public Item LegArmor = null;
    public Item RightAccessory = null;
    public Item LeftAccessory = null;
    //��v�X�e�[�^�X
    public int Hp = 0;
    public int Mp = 0;
    public int Atk = 0; //�U���\��
    public int Def = 0; //�h��\��
    public float Spd = 0; //�U�����x 
    public int ArmorPoint = 0;
    //��v�X�e�[�^�X�␳�l(%)
    public int HpRate = 100;
    public int MpRate = 100;
    public int AtkRate = 100;
    public int DefRate = 100;
    public int ArmorRate = 100;
    //�����_���[�W
    public int PhisicalDmg = 0;
    public int FireDmg = 0;
    public int IceDmg = 0;
    public int ThunderDmg = 0;
    public int WindDmg = 0;
    public int ShiningDmg = 0;
    public int DarknessDmg = 0;
    //�����␳�l(100%~)
    public int PhisicalRate = 100;
    public int FireRate = 100;
    public int IceRate = 100;
    public int ThunderRate = 100;
    public int WindRate = 100;
    public int ShiningRate = 100;
    public int DarknessRate = 100;
    //��R�l(0~80%)
    public int PhisicalResist = 0;
    public int FireResist = 0;
    public int IceResist = 0;
    public int ThunderResist = 0;
    public int WindResist = 0;
    public int ShiningResist = 0;
    public int DarknessResist = 0;
    //���̑�
    public int SpdRate = 100; //�U�����x�␳(100%~200%)
    public int AutoHpRecover = 0; //����HP��
    public int AutoHpRecoverRate = 100;
    public int AutoMpRecover = 0; //����MP��
    public int AutoMpRecoverRate = 100;
    public int GuardRate = 0; //�K�[�h�m��
    public int ExpGetRate = 0; // �擾�o���ʑ���
    //�W���u�֘A
    public enum Job
    {
        �f���G���X�g,
        �E�H�[���A�[,
        �i�C�g,
        �E�B�U�[�h,
        �r�V���b�v,
        ���C�f��,
    }
    public Job job = Job.�f���G���X�g;
    //�X�L�����x���i�[
    public int[] DuelistSkillLevelAry = Enumerable.Repeat<int>(0, 40).ToArray();
    public int[] WarriorSkilklLevelAry = Enumerable.Repeat<int>(0, 40).ToArray();
    public int[] KnightSkillLevelAry = Enumerable.Repeat<int>(0, 40).ToArray();
    public int[] WizardSkillLevelAry = Enumerable.Repeat<int>(0, 40).ToArray();
    public int[] BishopSkillLevelAry = Enumerable.Repeat<int>(0, 40).ToArray();
    public int[] MaidenSkillLevelAry = Enumerable.Repeat<int>(0, 40).ToArray();
    //�X�L���Ґ�
    public int[] DuelistActiveSkillSet = Enumerable.Repeat<int>(-1, 8).ToArray();
    public int[] WarriorActiveSkillSet = Enumerable.Repeat<int>(-1, 8).ToArray();
    public int[] KnightActiveSkillSet = Enumerable.Repeat<int>(-1, 8).ToArray();
    public int[] WizardActiveSkillSet = Enumerable.Repeat<int>(-1, 8).ToArray();
    public int[] BishopActiveSkillSet = Enumerable.Repeat<int>(-1, 8).ToArray();
    public int[] MaidenActiveSkillSet = Enumerable.Repeat<int>(-1, 8).ToArray();
    //�Q�[���i�s�x�L�^
    public List<int> ClearMapIdList = new();
    public int LastQuestId = 1;
    public bool AutoSkillMode = false;
    public bool SpeedUpMode = false;
    public int dropCommonItemNum = 0;
    public int dropRareItemNum = 0;
    public int dropEpicItemNum = 0;
    public int dropLegendaryItemNum = 0;
    public float bgmVolume = 0.07f;
    public float seVolume = 0.1f;
    public bool showSkillInfo = true;
    //-----------------------------------------
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
        BGMManager.Instance.ChangeBaseVolume(bgmVolume);
        SEManager.Instance.ChangeBaseVolume(seVolume);
    }

    private void LoadMyCharacterStatus()
    {
        Name = "�e�X�g";
        Level = 1;
        JPDuelist = 0;
        JPWarrior = 0;
        JPKnight = 0;
        JPWizard = 0;
        JPBishop = 0;
        JPMaiden = 0;
        Exp = 0;
        Money = 0;
        MainHand = null;
        OffHand = null;
        Helm = null;
        BodyArmor = null;
        Gauntlet = null;
        LegArmor = null;
        RightAccessory = null;
        LeftAccessory = null;
    }

    //�U�����x��Ԃ�
    public float GetBaseSpd()
    {
        float mainSpd = (MainHand == null) ? 0 : MainHand.Spd;
        float offSpd = (OffHand == null) ? 0 : OffHand.Spd;
        if(mainSpd != 0 && offSpd == 0)
        {
            return mainSpd;
        }
        else if (mainSpd == 0 && offSpd != 0)
        {
            return offSpd;
        }
        else if(mainSpd != 0 && offSpd != 0)
        {
            return (float)System.Math.Round((mainSpd + offSpd) / 2, 2, System.MidpointRounding.AwayFromZero);
        }
        else
        {
            return 1.2f;
        }
    }

    public void SetStatus(SkillDatabase[] skillDatabases)
    {
        //������
        #region
        Hp = 100+(25*Level);
        Mp = 100 + (10 * Level);
        Atk = 150+(5*Level); //�U���\��
        Def = 150+(5*Level); //�h��\��
        Spd = GetBaseSpd(); //�U�����x
        ArmorPoint = 30;
        //��v�X�e�[�^�X�␳�l(%)
        HpRate = 100;
        MpRate = 100;
        AtkRate = 100;
        DefRate = 100;
        ArmorRate = 100;
        //�����_���[�W
        PhisicalDmg = 5*Level;
        FireDmg = 5 * Level;
        IceDmg = 5 * Level;
        ThunderDmg = 5 * Level;
        WindDmg = 5 * Level;
        ShiningDmg = 5 * Level;
        DarknessDmg = 5 * Level;
        //�����␳�l(100%~)
        PhisicalRate = 100;
        FireRate = 100;
        IceRate = 100;
        ThunderRate = 100;
        WindRate = 100;
        ShiningRate = 100;
        DarknessRate = 100;
        //��R�l(0~80%)
        PhisicalResist = 0;
        FireResist = 0;
        IceResist = 0;
        ThunderResist = 0;
        WindResist = 0;
        ShiningResist = 0;
        DarknessResist = 0;
        //���̑�
        SpdRate = 100; //�U�����x�␳(100%~200%)
        AutoHpRecover = 5; //�����̗͉�
        AutoHpRecoverRate = 100; //�����̗͉񕜕␳
        AutoMpRecover = 5; // �����}�i��
        AutoMpRecoverRate = 100; // �����}�i�񕜕␳
        GuardRate = 0; //�K�[�h�m��
        ExpGetRate = 0; // �擾�o���ʑ���
        #endregion
        //�p�b�V�u�X�L�����f
        SetPassiveSkill(job, skillDatabases);
        Item[] items = { MainHand, OffHand, Helm, BodyArmor, Gauntlet, LegArmor, RightAccessory, LeftAccessory };
        foreach (Item item in items)
        {
            //��v�X�e�[�^�X
            Hp += (item == null) ? 0 : item.Hp;
            Mp += (item == null) ? 0 : item.Mp;
            Atk += (item == null) ? 0 : item.Atk;
            Def += (item == null) ? 0 : item.Def;
            ArmorPoint += (item == null) ? 0 : item.ArmorPoint;
            ArmorPoint += (item == null) ? 0 : item.ArmorUp;
            //��v�X�e�[�^�X�␳�l(%)
            HpRate += (item == null) ? 0 : item.HpRate;
            MpRate += (item == null) ? 0 : item.MpRate;
            AtkRate += (item == null) ? 0 : item.AtkRate;
            DefRate += (item == null) ? 0 : item.DefRate;
            ArmorRate += (item == null) ? 0 : item.ArmorRate;
            //�����_���[�W
            PhisicalDmg += (item == null) ? 0 : item.PhisicalDmg;
            FireDmg += (item == null) ? 0 : item.FireDmg;
            IceDmg += (item == null) ? 0 : item.IceDmg;
            ThunderDmg += (item == null) ? 0 : item.ThunderDmg;
            WindDmg += (item == null) ? 0 : item.WindDmg;
            ShiningDmg += (item == null) ? 0 : item.ShiningDmg;
            DarknessDmg += (item == null) ? 0 : item.DarknessDmg;
            //�����␳�l(%)
            PhisicalRate += (item == null) ? 0 : item.PhisicalRate;
            FireRate += (item == null) ? 0 : item.FireRate;
            IceRate += (item == null) ? 0 : item.IceRate;
            ThunderRate += (item == null) ? 0 : item.ThunderRate;
            WindRate += (item == null) ? 0 : item.WindRate;
            ShiningRate += (item == null) ? 0 : item.ShiningRate;
            DarknessRate += (item == null) ? 0 : item.DarknessRate;
            //��R�l
            PhisicalResist += (item == null) ? 0 : item.PhisicalResist;
            FireResist += (item == null) ? 0 : item.FireResist;
            IceResist += (item == null) ? 0 : item.IceResist;
            ThunderResist += (item == null) ? 0 : item.ThunderResist;
            WindResist += (item == null) ? 0 : item.WindResist;
            ShiningResist += (item == null) ? 0 : item.ShiningResist;
            DarknessResist += (item == null) ? 0 : item.DarknessResist;
            //���̑�
            SpdRate += (item == null) ? 0 : item.SpdRate;
            AutoHpRecover += (item == null) ? 0 : item.AutoHpRecover;
            AutoHpRecoverRate += (item == null) ? 0 : item.AutoHpRecoverRate;
            AutoMpRecover += (item == null) ? 0 : item.AutoMpRecover;
            AutoMpRecoverRate += (item == null) ? 0 : item.AutoMpRecoverRate;
            GuardRate += (item == null) ? 0 : item.GuardRate;
            ExpGetRate += (item == null) ? 0 : item.ExpGetRate;
        }
        //����K�p
        #region
        /*
        //��R�l
        PhisicalResist = Mathf.Clamp(PhisicalResist, 0,80);
        FireResist = Mathf.Clamp(FireResist, 0, 80);
        IceResist = Mathf.Clamp(IceResist, 0, 80);
        ThunderResist = Mathf.Clamp(ThunderResist, 0, 80);
        WindResist = Mathf.Clamp(WindResist, 0, 80);
        ShiningResist = Mathf.Clamp(ShiningResist, 0, 80);
        DarknessResist = Mathf.Clamp(DarknessResist, 0, 80);
        //�ڍ׃X�e
        SpdRate = Mathf.Clamp(SpdRate, 0, 200);
        GuardRate = Mathf.Clamp(GuardRate, 0, 100);
        */
        #endregion
        //�␳�K�p
        #region
        /*
        //��v�X�e�[�^�X
        Hp = (HpRate <= 100) ? Hp : (int)System.Math.Round((float)Hp * (float)HpRate/100, System.MidpointRounding.AwayFromZero);
        Mp = (MpRate <= 100) ? Mp : (int)System.Math.Round((float)Mp * (float)MpRate/100, System.MidpointRounding.AwayFromZero);
        Atk = (AtkRate <= 100) ? Atk : (int)System.Math.Round((float)Atk * (float)AtkRate / 100, System.MidpointRounding.AwayFromZero);
        Def = (DefRate <= 100) ? Def : (int)System.Math.Round((float)Def * (float)DefRate / 100, System.MidpointRounding.AwayFromZero);
        Spd = (SpdRate <= 100) ? Spd : (float)System.Math.Round((float)Spd / ((float)SpdRate / 100), 2, System.MidpointRounding.AwayFromZero);
        ArmorPoint = (ArmorRate <= 100)? ArmorPoint : (int)System.Math.Round((float)ArmorPoint * ((float)ArmorRate / 100), 2, System.MidpointRounding.AwayFromZero);
        //�����_���[�W
        PhisicalDmg = GetPhisicalDmg();
        FireDmg = GetFireDmg();
        IceDmg = GetIceDmg();
        ThunderDmg = GetThunderDmg();
        WindDmg = GetWindDmg();
        ShiningDmg = GetShiningDmg();
        DarknessDmg = GetDarknessDmg();
        //�ڍ׃X�e
        AutoHpRecover = (AutoHpRecoverRate <= 100)? AutoHpRecover : (int)System.Math.Round((float)AutoHpRecover * (float)AutoHpRecoverRate / 100, System.MidpointRounding.AwayFromZero);
        AutoMpRecover = (AutoMpRecoverRate <= 100)? AutoMpRecover : (int)System.Math.Round((float)AutoMpRecover * (float)AutoMpRecoverRate / 100, System.MidpointRounding.AwayFromZero);
        */
        #endregion
    }
    //�␳��K�p�����X�e�[�^�X��return����------------------------------
    //��b�X�e
    #region
    public int GetHp(int buffNum = 0, int buffRate = 0)
    {
        return (HpRate + buffRate <= 0) ? 1 : (int)System.Math.Round((float)(Hp + buffNum) * (float)(HpRate + buffRate) / 100, System.MidpointRounding.AwayFromZero);
    }
    public int GetMp(int buffNum = 0, int buffRate = 0)
    {
        return (MpRate + buffRate <= 0) ? 0 : (int)System.Math.Round((float)(Mp + buffNum) * (float)(MpRate + buffRate) / 100, System.MidpointRounding.AwayFromZero);
    }
    public int GetAtk(int buffNum = 0, int buffRate = 0)
    {
        return (AtkRate + buffRate <= 0) ? 0 : (int)System.Math.Round((float)(Atk + buffNum) * (float)(AtkRate + buffRate) / 100, System.MidpointRounding.AwayFromZero);
    }
    public int GetDef(int buffNum = 0, int buffRate = 0)
    {
        return (DefRate + buffRate <= 0) ? 0 : (int)System.Math.Round((float)(Def + buffNum) * (float)(DefRate + buffRate) / 100, System.MidpointRounding.AwayFromZero);
    }
    public float GetSpd(int buffRate = 0)
    {
        return (float)System.Math.Round((float)Spd * ((float)GetSpdRate(buffRate) / 100), 2, System.MidpointRounding.AwayFromZero);
    }
    public int GetArmorPoint(int buffNum = 0, int buffRate = 0)
    {
        return (ArmorRate + buffRate <= 0) ? 0 : (int)System.Math.Round((float)(ArmorPoint + buffNum) * ((float)(ArmorRate + buffRate) / 100), System.MidpointRounding.AwayFromZero);
    }
    #endregion
    //�����_���[�W
    #region
    public int GetPhisicalDmg(int buffNum = 0, int buffRate = 0)
    {
        return (PhisicalRate + buffRate <= 0)? 0:(int)System.Math.Round((float)(PhisicalDmg + buffNum) * (float)(PhisicalRate+buffRate) / 100, System.MidpointRounding.AwayFromZero);
    }
    public int GetFireDmg(int buffNum = 0, int buffRate = 0)
    {
        return (FireRate + buffRate <= 0) ? 0 : (int)System.Math.Round((float)(FireDmg + buffNum) * (float)(FireRate + buffRate) / 100, System.MidpointRounding.AwayFromZero);
    }
    public int GetIceDmg(int buffNum = 0, int buffRate = 0)
    {
        return (IceRate + buffRate <= 0) ? 0 : (int)System.Math.Round((float)(IceDmg + buffNum) * (float)(IceRate + buffRate) / 100, System.MidpointRounding.AwayFromZero);
    }
    public int GetThunderDmg(int buffNum = 0, int buffRate = 0)
    {
        return (ThunderRate + buffRate <= 0) ? 0 : (int)System.Math.Round((float)(ThunderDmg + buffNum) * (float)(ThunderRate + buffRate) / 100, System.MidpointRounding.AwayFromZero);
    }
    public int GetWindDmg(int buffNum = 0, int buffRate = 0)
    {
        return (WindRate + buffRate <= 0) ? 0 : (int)System.Math.Round((float)(WindDmg + buffNum) * (float)(WindRate + buffRate) / 100, System.MidpointRounding.AwayFromZero);        
    }
    public int GetShiningDmg(int buffNum = 0, int buffRate = 0)
    {
        return (ShiningRate + buffRate <= 0) ? 0 : (int)System.Math.Round((float)(ShiningDmg + buffNum) * (float)(ShiningRate + buffRate) / 100, System.MidpointRounding.AwayFromZero);
    }
    public int GetDarknessDmg(int buffNum = 0, int buffRate = 0)
    {
        return (DarknessRate + buffRate <= 0) ? 0 : (int)System.Math.Round((float)(DarknessDmg + buffNum) * (float)(DarknessRate + buffRate) / 100, System.MidpointRounding.AwayFromZero);
    }
    #endregion
    //�����ϐ�
    #region
    public int GetPhisicalResist(int buffNum = 0)
    {
        return Mathf.Min(PhisicalResist + buffNum, 80);
    }
    public int GetFireResist(int buffNum = 0)
    {
        return Mathf.Min(FireResist + buffNum, 80);
    }
    public int GetIceResist(int buffNum = 0)
    {
        return Mathf.Min(IceResist + buffNum, 80);
    }
    public int GetThunderResist(int buffNum = 0)
    {
        return Mathf.Min(ThunderResist + buffNum, 80);
    }
    public int GetWindResist(int buffNum = 0)
    {
        return Mathf.Min(WindResist + buffNum, 80);
    }
    public int GetShiningResist(int buffNum = 0)
    {
        return Mathf.Min(ShiningResist + buffNum, 80);
    }
    public int GetDarknessResist(int buffNum = 0)
    {
        return Mathf.Min(DarknessResist + buffNum, 80);
    }
    #endregion
    //���̑�
    #region
    public int GetSpdRate(int buffRate = 0)
    {
        return Mathf.Clamp(SpdRate + buffRate,1, 200); 
    }
    public int GetAutoHpRecover(int buffNum = 0, int buffRate = 0)
    {
        return (AutoHpRecoverRate + buffRate <= 0)? 0:(int)System.Math.Round((float)(AutoHpRecover+ buffNum) * (float)(AutoHpRecoverRate+ buffRate) / 100, System.MidpointRounding.AwayFromZero);
    }
    public int GetAutoMpRecover(int buffNum = 0, int buffRate = 0)
    {
        return (AutoMpRecoverRate + buffRate <= 0) ? 0 : (int)System.Math.Round((float)(AutoMpRecover+ buffNum) * (float)(AutoMpRecoverRate + buffRate) / 100, System.MidpointRounding.AwayFromZero);
    }
    public int GetGuardRate(int buffRate = 0)
    {
        return Mathf.Clamp(GuardRate + buffRate, 0, 100);
    }
    public int GetExpGetRate(int buffRate = 0)
    {
        return ExpGetRate + buffRate;
    }
    #endregion

    //�p�b�V�u�X�L���𔽉f����
    public void SetPassiveSkill(MyCharacterStatus.Job job, SkillDatabase[] skillDatabases)
    {
        switch (job)
        {
            case Job.�f���G���X�g:
                for (int i = 0; i< skillDatabases[0].SkillDataList.Count; i++)
                {
                    SkillDatabase.Skill skill = skillDatabases[0].SkillDataList[i];
                    if (skill.type == SkillDatabase.Skill.SkillType.�A�N�e�B�u�X�L��) continue;
                    foreach (SkillDatabase.Skill.SkillEffect skillEffect in skill.skillEffects)
                    {

                        SkillEffectController(skillEffect, DuelistSkillLevelAry[i]);
                    }
                }
                break;
        }
    }
    public void SkillEffectController(SkillDatabase.Skill.SkillEffect skillEffect, int lv)
    {
        switch (skillEffect.effectType)
        {
            case SkillDatabase.Skill.SkillEffect.EffectType.HP:
                Hp += skillEffect.EffectAmount*lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.HP�␳:
                HpRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.MP:
                Mp += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.MP�␳:
                MpRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�U���\��:
                Atk += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�U���\�͕␳:
                AtkRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�h��\��:
                Def += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�h��\�͕␳:
                DefRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.���b�l:
                ArmorPoint += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.���b�l�␳:
                ArmorRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�����_���[�W:
                PhisicalDmg += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�����␳:
                PhisicalRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.���_���[�W:
                FireDmg += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.���␳:
                FireRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�X�_���[�W:
                IceDmg += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�X�␳:
                IceRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.���_���[�W:
                ThunderDmg += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.���␳:
                ThunderRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.���_���[�W:
                WindDmg += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.���␳:
                WindRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.���_���[�W:
                ShiningDmg += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.���␳:
                ShiningRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�Ń_���[�W:
                DarknessDmg += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�ŕ␳:
                DarknessRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�����ϐ�:
                PhisicalResist += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.���ϐ�:
                FireResist += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�X�ϐ�:
                IceResist += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.���ϐ�:
                ThunderResist += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.���ϐ�:
                WindResist += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.���ϐ�:
                ShiningResist += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�őϐ�:
                DarknessResist += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�U�����x:
                SpdRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.����HP��:
                AutoHpRecover += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.����HP�񕜕␳:
                AutoHpRecoverRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.����MP��:
                AutoMpRecover += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.����MP�񕜕␳:
                AutoMpRecoverRate += skillEffect.EffectAmount * lv;
                break;
            case SkillDatabase.Skill.SkillEffect.EffectType.�K�[�h�m��:
                GuardRate += skillEffect.EffectAmount * lv;
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// �e�W���u�̃X�L���Ґ����ЂƂɂ��ĕԂ�
    /// </summary>
    public int[][] GetRegistedActiveSkillArys()
    {
        int[][] arys =
        {
            DuelistActiveSkillSet,
            WarriorActiveSkillSet,
            KnightActiveSkillSet,
            WizardActiveSkillSet,
            BishopActiveSkillSet,
            MaidenActiveSkillSet
        };
        return arys;
    }
    /// <summary>
    /// �e�W���u�̃X�L�����x���z����ЂƂɂ��ĕԂ�
    /// </summary>
    public int[][] GetAllSkillLevelArys()
    {
        int[][] arys =
        {
            DuelistSkillLevelAry,
            WarriorSkilklLevelAry,
            KnightSkillLevelAry,
            WizardSkillLevelAry,
            BishopSkillLevelAry,
            MaidenSkillLevelAry
        };
        return arys;
    }
}
