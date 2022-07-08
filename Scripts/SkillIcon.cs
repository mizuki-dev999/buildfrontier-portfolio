using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image skillImage;
    public BattleManager battleManager;
    public SkillEffectManager skillEffectManager;
    public StageUIManager stageUIManager;
    public Image coolTimeImage;
    public GameObject skillHoverCover;
    private SkillDatabase.Skill skill;
    private bool canSkill;
    private bool isCooltime;
    private int job;
    private int id;
    private int level;
    
    public void Init(int job = -1, int id = -1)
    {
        if(job == -1 || id == -1)
        {
            skillImage.color = new Color(1, 1, 1, 0);
            canSkill = false;
            isCooltime = false;
        }
        else
        {
            this.job = job;
            this.id = id;
            this.level = GetSkillLevel(job, id);
            this.skill = skillEffectManager.battleManager.skillDatabases[job].SkillDataList[id];
            skillImage.sprite = this.skill.sprite;
            isCooltime = false;
            canSkill = true;
            coolTimeImage.fillAmount = 0;
        } 
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        ActiveSkill();
    }
    public void ActiveSkill()
    {
        if (!canSkill) return;
        if (skillEffectManager.battleManager.phase != BattleManager.Phase.BattlePhase) return;
        if (isCooltime)
        {
            Debug.Log("�N�[���^�C����");
            return;
        }
        float hpStealRate = 0;
        float mpStealRate = 0;
        foreach (SkillDatabase.Skill.SkillEffect skillEffect in skill.skillEffects)
        {
            switch (skillEffect.effectType)
            {
                case SkillDatabase.Skill.SkillEffect.EffectType.���C�t�X�e�B�[��:
                    hpStealRate += skillEffect.EffectAmount;
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.�}�i�X�e�B�[��:
                    mpStealRate += skillEffect.EffectAmount;
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.�U���X�L��:
                    battleManager.MySkillAttack(skillEffect.EffectAmount*level, hpStealRate, mpStealRate);
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.HP�񕜃X�L��:
                    battleManager.RecoverMyHp(skillEffect.EffectAmount * level);
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.MP�񕜃X�L��:
                    battleManager.RecoverMyMp(skillEffect.EffectAmount * level);
                    break;
                default:
                    if (skillEffect.target == 0) skillEffectManager.BeginMyEffect((int)skillEffect.effectType - 9, skillEffect.EffectAmount * level, skill.EffectTime);
                    else
                    {
                        if((int)skillEffect.effectType <= 27) skillEffectManager.BeginEnemyEffect(((int)skillEffect.effectType - 9) / 2, skillEffect.EffectAmount * level, skill.EffectTime);
                        else if ((int)skillEffect.effectType == 41) skillEffectManager.BeginEnemyEffect(19, skillEffect.EffectAmount * level, skill.EffectTime);
                        else if ((int)skillEffect.effectType >= 29) skillEffectManager.BeginEnemyEffect((int)skillEffect.effectType - 19, skillEffect.EffectAmount * level, skill.EffectTime);
                    }
                    break;
            }
        }
        isCooltime = true;
        skillImage.color = Color.gray;
    }
    public void CoolTimeAnimation(float deltaTime)
    {
        coolTimeImage.fillAmount += deltaTime / skill.CoolTime;
        if (coolTimeImage.fillAmount >= 1) FinishCoolTime();
    }
    public void FinishCoolTime()
    {
        isCooltime = false;
        coolTimeImage.fillAmount = 0;
        skillImage.color = Color.white;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!canSkill) return;
        if (!skillEffectManager.battleManager.myStatus.showSkillInfo) return;
        skillHoverCover.SetActive(true);
        stageUIManager.skillInfoPanel.SetActive(true);
        ChangeSkillName(job, id, stageUIManager.skillNameText);
        ChangeSkillInfo(GetSkillLevel(job, id), stageUIManager.skillInfoText);
        stageUIManager.skillLevelText.text = $"Lv{GetSkillLevel(job, id)}";
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!canSkill) return;
        if (!skillEffectManager.battleManager.myStatus.showSkillInfo) return;
        skillHoverCover.SetActive(false);
        stageUIManager.skillInfoPanel.SetActive(false);
    }

    public int GetSkillLevel(int job, int id)
    {
        return job switch
        {
            0 => skillEffectManager.battleManager.myStatus.DuelistSkillLevelAry[id],
            1 => skillEffectManager.battleManager.myStatus.WarriorSkilklLevelAry[id],
            2 => skillEffectManager.battleManager.myStatus.KnightSkillLevelAry[id],
            3 => skillEffectManager.battleManager.myStatus.WarriorSkilklLevelAry[id],
            4 => skillEffectManager.battleManager.myStatus.BishopSkillLevelAry[id],
            5 => skillEffectManager.battleManager.myStatus.MaidenSkillLevelAry[id],
            _ => 0,
        };
    }
    public void ChangeSkillName(int job, int id, TextMeshProUGUI text)
    {
        if (job == 0 && (id == 23 || id == 35))
        {
            text.text = $"<size=85%>{skill.Name}";
        }
        else text.text = $"{skill.Name}";
    }
    public void ChangeSkillInfo(int level, TextMeshProUGUI text)
    {
        float dmgRate = 0;
        bool acquire = (level != 0);
        if (!acquire) level++;
        foreach (SkillDatabase.Skill.SkillEffect skillEffect in skill.skillEffects)
        {
            if (skillEffect.effectType == SkillDatabase.Skill.SkillEffect.EffectType.�U���X�L��)
            {
                dmgRate = (float)(100 + skillEffect.EffectAmount * level) / 100;
            }
        }
        string cost = (skill.Cost == 0) ? $"����MP�F�[�[�[\n" : $"����MP�F{skill.Cost * level}\n";
        string rate = (dmgRate == 0) ? $"�З́F�[�[�[\n" : $"�З́F{dmgRate}�{\n";
        string ct = (skill.Cost == 0) ? $"�N�[���^�C���F�[�[�[\n" : $"�N�[���^�C���F{skill.CoolTime}�b\n";
        string plusEffect = "";
        text.text = $"<line-height=80%>��ށF{skill.type}\n" +
             cost + rate + ct + "<line-height=70%>���ʁF\n<size=88%>" +
             GetSkillExplanation(level) + "</line-height>\n" + plusEffect;
    }
    public string GetSkillExplanation(int level)
    {
        List<string> textList = new();
        foreach (SkillDatabase.Skill.SkillEffect skillEffect in skill.skillEffects)
        {
            switch (skillEffect.effectType)
            {
                case SkillDatabase.Skill.SkillEffect.EffectType.HP�񕜃X�L��:
                    textList.Add($"�ő�HP��{skillEffect.EffectAmount * level}%��");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.MP�񕜃X�L��:
                    textList.Add($"�ő�MP{skillEffect.EffectAmount * level}%��");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.���C�t�X�e�B�[��:
                    textList.Add($"�^�����_���[�W��{skillEffect.EffectAmount * level}%��(�ő�HP��1�������E�l)");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.�}�i�X�e�B�[��:
                    textList.Add($"�^�����_���[�W��{skillEffect.EffectAmount * level}%��(�ő�HP��1�������E�l)");
                    break;
            }
        }
        if (skill.EffectTime != 0) textList.Add($"{skill.EffectTime}�b��");
        foreach (SkillDatabase.Skill.SkillEffect skillEffect in skill.skillEffects)
        {
            switch (skillEffect.effectType)
            {
                case SkillDatabase.Skill.SkillEffect.EffectType.HP:
                case SkillDatabase.Skill.SkillEffect.EffectType.MP:
                case SkillDatabase.Skill.SkillEffect.EffectType.�U���\��:
                case SkillDatabase.Skill.SkillEffect.EffectType.�h��\��:
                case SkillDatabase.Skill.SkillEffect.EffectType.���b�l:
                case SkillDatabase.Skill.SkillEffect.EffectType.�����_���[�W:
                case SkillDatabase.Skill.SkillEffect.EffectType.���_���[�W:
                case SkillDatabase.Skill.SkillEffect.EffectType.�X�_���[�W:
                case SkillDatabase.Skill.SkillEffect.EffectType.���_���[�W:
                case SkillDatabase.Skill.SkillEffect.EffectType.���_���[�W:
                case SkillDatabase.Skill.SkillEffect.EffectType.���_���[�W:
                case SkillDatabase.Skill.SkillEffect.EffectType.�Ń_���[�W:
                case SkillDatabase.Skill.SkillEffect.EffectType.����HP��:
                case SkillDatabase.Skill.SkillEffect.EffectType.����MP��:
                    if (skillEffect.target == 0) textList.Add($"���g��{skillEffect.effectType} +{skillEffect.EffectAmount * level}");
                    else textList.Add($"�G��{skillEffect.effectType} {skillEffect.EffectAmount * level}");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.�����ϐ�:
                case SkillDatabase.Skill.SkillEffect.EffectType.���ϐ�:
                case SkillDatabase.Skill.SkillEffect.EffectType.�X�ϐ�:
                case SkillDatabase.Skill.SkillEffect.EffectType.���ϐ�:
                case SkillDatabase.Skill.SkillEffect.EffectType.���ϐ�:
                case SkillDatabase.Skill.SkillEffect.EffectType.���ϐ�:
                case SkillDatabase.Skill.SkillEffect.EffectType.�őϐ�:
                case SkillDatabase.Skill.SkillEffect.EffectType.�����␳:
                case SkillDatabase.Skill.SkillEffect.EffectType.���␳:
                case SkillDatabase.Skill.SkillEffect.EffectType.�X�␳:
                case SkillDatabase.Skill.SkillEffect.EffectType.���␳:
                case SkillDatabase.Skill.SkillEffect.EffectType.���␳:
                case SkillDatabase.Skill.SkillEffect.EffectType.���␳:
                case SkillDatabase.Skill.SkillEffect.EffectType.�ŕ␳:
                case SkillDatabase.Skill.SkillEffect.EffectType.�U�����x:
                case SkillDatabase.Skill.SkillEffect.EffectType.�K�[�h�m��:
                    if (skillEffect.target == 0) textList.Add($"���g��{skillEffect.effectType} +{skillEffect.EffectAmount * level}%");
                    else textList.Add($"�G��{skillEffect.effectType} {skillEffect.EffectAmount * level}%");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.HP�␳:
                    textList.Add($"���g��HP +{skillEffect.EffectAmount * level}%");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.MP�␳:
                    textList.Add($"���g��MP +{skillEffect.EffectAmount * level}%");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.�U���\�͕␳:
                    textList.Add($"���g�̍U���\�� +{skillEffect.EffectAmount * level}%");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.�h��\�͕␳:
                    textList.Add($"���g�̖h��\�� +{skillEffect.EffectAmount * level}%");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.���b�l�␳:
                    textList.Add($"���g�̑��b�l +{skillEffect.EffectAmount * level}%");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.����HP�񕜕␳:
                    textList.Add($"���g�̎���HP�� +{skillEffect.EffectAmount * level}%");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.����MP�񕜕␳:
                    textList.Add($"���g�̎���MP�� +{skillEffect.EffectAmount * level}%");
                    break;
                default:
                    break;
            }
        }
        return string.Join("\n", textList);
    }
    public bool GetIsCoolTime()
    {
        return isCooltime;
    }
}
