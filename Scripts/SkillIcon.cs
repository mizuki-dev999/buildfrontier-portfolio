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
            Debug.Log("クールタイム中");
            return;
        }
        float hpStealRate = 0;
        float mpStealRate = 0;
        foreach (SkillDatabase.Skill.SkillEffect skillEffect in skill.skillEffects)
        {
            switch (skillEffect.effectType)
            {
                case SkillDatabase.Skill.SkillEffect.EffectType.ライフスティール:
                    hpStealRate += skillEffect.EffectAmount;
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.マナスティール:
                    mpStealRate += skillEffect.EffectAmount;
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.攻撃スキル:
                    battleManager.MySkillAttack(skillEffect.EffectAmount*level, hpStealRate, mpStealRate);
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.HP回復スキル:
                    battleManager.RecoverMyHp(skillEffect.EffectAmount * level);
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.MP回復スキル:
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
            if (skillEffect.effectType == SkillDatabase.Skill.SkillEffect.EffectType.攻撃スキル)
            {
                dmgRate = (float)(100 + skillEffect.EffectAmount * level) / 100;
            }
        }
        string cost = (skill.Cost == 0) ? $"消費MP：ーーー\n" : $"消費MP：{skill.Cost * level}\n";
        string rate = (dmgRate == 0) ? $"威力：ーーー\n" : $"威力：{dmgRate}倍\n";
        string ct = (skill.Cost == 0) ? $"クールタイム：ーーー\n" : $"クールタイム：{skill.CoolTime}秒\n";
        string plusEffect = "";
        text.text = $"<line-height=80%>種類：{skill.type}\n" +
             cost + rate + ct + "<line-height=70%>効果：\n<size=88%>" +
             GetSkillExplanation(level) + "</line-height>\n" + plusEffect;
    }
    public string GetSkillExplanation(int level)
    {
        List<string> textList = new();
        foreach (SkillDatabase.Skill.SkillEffect skillEffect in skill.skillEffects)
        {
            switch (skillEffect.effectType)
            {
                case SkillDatabase.Skill.SkillEffect.EffectType.HP回復スキル:
                    textList.Add($"最大HPの{skillEffect.EffectAmount * level}%回復");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.MP回復スキル:
                    textList.Add($"最大MP{skillEffect.EffectAmount * level}%回復");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.ライフスティール:
                    textList.Add($"与えたダメージの{skillEffect.EffectAmount * level}%回復(最大HPの1割が限界値)");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.マナスティール:
                    textList.Add($"与えたダメージの{skillEffect.EffectAmount * level}%回復(最大HPの1割が限界値)");
                    break;
            }
        }
        if (skill.EffectTime != 0) textList.Add($"{skill.EffectTime}秒間");
        foreach (SkillDatabase.Skill.SkillEffect skillEffect in skill.skillEffects)
        {
            switch (skillEffect.effectType)
            {
                case SkillDatabase.Skill.SkillEffect.EffectType.HP:
                case SkillDatabase.Skill.SkillEffect.EffectType.MP:
                case SkillDatabase.Skill.SkillEffect.EffectType.攻撃能力:
                case SkillDatabase.Skill.SkillEffect.EffectType.防御能力:
                case SkillDatabase.Skill.SkillEffect.EffectType.装甲値:
                case SkillDatabase.Skill.SkillEffect.EffectType.物理ダメージ:
                case SkillDatabase.Skill.SkillEffect.EffectType.炎ダメージ:
                case SkillDatabase.Skill.SkillEffect.EffectType.氷ダメージ:
                case SkillDatabase.Skill.SkillEffect.EffectType.雷ダメージ:
                case SkillDatabase.Skill.SkillEffect.EffectType.風ダメージ:
                case SkillDatabase.Skill.SkillEffect.EffectType.光ダメージ:
                case SkillDatabase.Skill.SkillEffect.EffectType.闇ダメージ:
                case SkillDatabase.Skill.SkillEffect.EffectType.自動HP回復:
                case SkillDatabase.Skill.SkillEffect.EffectType.自動MP回復:
                    if (skillEffect.target == 0) textList.Add($"自身の{skillEffect.effectType} +{skillEffect.EffectAmount * level}");
                    else textList.Add($"敵の{skillEffect.effectType} {skillEffect.EffectAmount * level}");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.物理耐性:
                case SkillDatabase.Skill.SkillEffect.EffectType.炎耐性:
                case SkillDatabase.Skill.SkillEffect.EffectType.氷耐性:
                case SkillDatabase.Skill.SkillEffect.EffectType.雷耐性:
                case SkillDatabase.Skill.SkillEffect.EffectType.風耐性:
                case SkillDatabase.Skill.SkillEffect.EffectType.光耐性:
                case SkillDatabase.Skill.SkillEffect.EffectType.闇耐性:
                case SkillDatabase.Skill.SkillEffect.EffectType.物理補正:
                case SkillDatabase.Skill.SkillEffect.EffectType.炎補正:
                case SkillDatabase.Skill.SkillEffect.EffectType.氷補正:
                case SkillDatabase.Skill.SkillEffect.EffectType.雷補正:
                case SkillDatabase.Skill.SkillEffect.EffectType.風補正:
                case SkillDatabase.Skill.SkillEffect.EffectType.光補正:
                case SkillDatabase.Skill.SkillEffect.EffectType.闇補正:
                case SkillDatabase.Skill.SkillEffect.EffectType.攻撃速度:
                case SkillDatabase.Skill.SkillEffect.EffectType.ガード確率:
                    if (skillEffect.target == 0) textList.Add($"自身の{skillEffect.effectType} +{skillEffect.EffectAmount * level}%");
                    else textList.Add($"敵の{skillEffect.effectType} {skillEffect.EffectAmount * level}%");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.HP補正:
                    textList.Add($"自身のHP +{skillEffect.EffectAmount * level}%");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.MP補正:
                    textList.Add($"自身のMP +{skillEffect.EffectAmount * level}%");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.攻撃能力補正:
                    textList.Add($"自身の攻撃能力 +{skillEffect.EffectAmount * level}%");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.防御能力補正:
                    textList.Add($"自身の防御能力 +{skillEffect.EffectAmount * level}%");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.装甲値補正:
                    textList.Add($"自身の装甲値 +{skillEffect.EffectAmount * level}%");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.自動HP回復補正:
                    textList.Add($"自身の自動HP回復 +{skillEffect.EffectAmount * level}%");
                    break;
                case SkillDatabase.Skill.SkillEffect.EffectType.自動MP回復補正:
                    textList.Add($"自身の自動MP回復 +{skillEffect.EffectAmount * level}%");
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
