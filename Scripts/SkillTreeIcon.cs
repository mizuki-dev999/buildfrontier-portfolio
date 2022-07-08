using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeIcon : MonoBehaviour
{
    private MyCharacterStatus myStatus;
    private HomeUIManager homeUIManager;
    private SkillDatabase.Skill skill;
    public int job;
    public int id;
    public int getLevel;
    private CanvasGroup canvasGroup;
    public CanvasGroup[] nextSkillIcon;
    public Image[] beforeLines;
    public Image[] nextLines;
    private TextMeshProUGUI iconLevelText;
    
    void Start()
    {
        myStatus = GameObject.Find("MyCharacterStatus").GetComponent<MyCharacterStatus>();
        homeUIManager = GameObject.Find("HomeUIManager").GetComponent<HomeUIManager>();
        canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        iconLevelText = this.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        skill = homeUIManager.skillDatabases[job].SkillDataList[id];
        if (skill.necessarySkillId == -1)
        {
            if (myStatus.Level < getLevel) this.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(80, 80, 80);
        }
        else if (GetSkillLevel(job, skill.necessarySkillId) == 0)
        {
            this.GetComponent<Image>().color = new Color(80, 80, 80);
        }
        if(GetSkillLevel(job, id) == 0)
        {
            this.GetComponent<Image>().color = new Color(120 / 255f, 120 / 255f, 120 / 255f);
            foreach (Image line in nextLines)
            {
                line.color = new Color(120 / 255f, 120 / 255f, 120 / 255f);
            }
        }
        if(myStatus.Level < getLevel)
        {
            this.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(100 / 255f, 100 / 255f, 100 / 255f);
            this.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = new Color(140 / 255f, 140 / 255f, 140 / 255f);
        }
        iconLevelText.text = $"<cspace=-0.1em>Lv{GetSkillLevel(job, id)}/{skill.MaxLevel}";
    }

    public void OnSkillTreeIcon()
    {
        ChangeSkillName(job, id);
        ChangeSkillInfo(GetSkillLevel(job, id));
        homeUIManager.skillLevelText.text = $"Lv{GetSkillLevel(job, id)}/{skill.MaxLevel}";
        iconLevelText.text = $"<cspace=-0.1em>Lv{GetSkillLevel(job, id)}/{skill.MaxLevel}";
        homeUIManager.skillLevelUpBtn.onClick.RemoveAllListeners();
        homeUIManager.skillLevelDownBtn.onClick.RemoveAllListeners();
        homeUIManager.skillLevelUpBtn.onClick.AddListener(() => OnSkillLevelUpBtn(this.beforeLines, this.nextLines, skill, job, id));
        homeUIManager.skillLevelDownBtn.onClick.AddListener(() => OnSkillLevelDownBtn(this.beforeLines, this.nextLines, skill, job, id));
    }

    public int GetSkillLevel(int job, int id)
    {
        int[][] allSkillLevelArys = myStatus.GetAllSkillLevelArys();
        return allSkillLevelArys[job][id];
    }

    public void ChangeSkillName(int job, int id)
    {
        if (job == 0 && (id == 23 || id == 35))
        {
            homeUIManager.skillNameText.text = $"<size=85%>{skill.Name}";
        }
        else homeUIManager.skillNameText.text = $"{skill.Name}";
    }

    public void ChangeSkillInfo(int level)
    {
        float dmgRate = 0;
        bool acquire = (level != 0);
        if (!acquire) level++;
        foreach(SkillDatabase.Skill.SkillEffect skillEffect in skill.skillEffects)
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
        homeUIManager.skillInfoText.text = $"<line-height=80%>種類：{skill.type}\n" +
             cost + rate + ct + "<line-height=70%>効果：\n<size=88%>" +
             GetSkillExplanation(level)+"</line-height>\n" + plusEffect;
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
        if(skill.EffectTime != 0) textList.Add($"{skill.EffectTime}秒間");
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
                    if(skillEffect.target == 0)textList.Add($"自身の{skillEffect.effectType} +{skillEffect.EffectAmount * level}");
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

    public void OnSkillLevelUpBtn(Image[] beforeLines, Image[] nextLines, SkillDatabase.Skill skill, int job, int id)
    {
        bool flag = true;
        homeUIManager.ClickSound();
        if (myStatus.Level < getLevel)
        {
            homeUIManager.ShowAlertImage("習得に必要なレベルに達していません");
            flag = false;
        }
        if (skill.necessarySkillId != -1)
        {
            if (GetSkillLevel(job, skill.necessarySkillId) == 0)
            {
                homeUIManager.ShowAlertImage("習得に必要なスキルを習得していません");
                flag = false;
            }
        }
        if (GetSkillLevel(job, id) == skill.MaxLevel)
        {
            homeUIManager.ShowAlertImage("このスキルのレベルは上限に達しました");
            flag = false;
        }
        if (!JpCheck(job))
        {
            homeUIManager.ShowAlertImage("ジョブポイントが足りません");
            flag = false;
        }
        if (flag)
        {
            SkillLevelChange(job, id, 1);
            JpChange(job, -1);
            homeUIManager.skillLevelText.text = $"Lv{GetSkillLevel(job, id)}/{skill.MaxLevel}";
            iconLevelText.text = $"<cspace=-0.1em>Lv{GetSkillLevel(job, id)}/{skill.MaxLevel}";
            ChangeSkillInfo(GetSkillLevel(job, id));
            if (GetSkillLevel(job, id) == 1)
            {
                this.GetComponent<Image>().color = new Color(1, 1, 1);
                foreach (Image line in nextLines)
                {
                    line.color = new Color(1, 1, 1);
                }
            }
        }
    }

    public void OnSkillLevelDownBtn(Image[] beforeLines, Image[] nextLines, SkillDatabase.Skill skill, int job, int id)
    {
        bool flag = true;
        homeUIManager.ClickSound();
        
        foreach (int nextid in skill.nextSkillId)
        {
            if (GetSkillLevel(job, nextid) > 0 && GetSkillLevel(job, this.id) == 1)
            {
                homeUIManager.ShowAlertImage("<size=80%>他のスキルの習得に必要なため未習得にできません");
                flag = false;
            }
        }
        if (GetSkillLevel(job, id) == 0 && flag)
        {
            homeUIManager.ShowAlertImage("このスキルのレベルは下限に達しました");
            flag = false;
        }
        if (myStatus.Level < getLevel && flag)
        {
            homeUIManager.ShowAlertImage("習得に必要なレベルに達していません");
            flag = false;
        }
        if (flag)
        {
            SkillLevelChange(job, id, -1);
            JpChange(job, 1);
            homeUIManager.skillLevelText.text = $"Lv{GetSkillLevel(job, id)}/{skill.MaxLevel}";
            iconLevelText.text = $"<cspace=-0.1em>Lv{GetSkillLevel(job, id)}/{skill.MaxLevel}";
            ChangeSkillInfo(GetSkillLevel(job, id));
            if (GetSkillLevel(job, id) == 0)
            {
                this.GetComponent<Image>().color = new Color(120 / 255f, 120 / 255f, 120 / 255f);
                foreach (Image line in nextLines)
                {
                    line.color = new Color(120 / 255f, 120 / 255f, 120 / 255f);
                }
                int[][] jobActiveSkillArys = myStatus.GetRegistedActiveSkillArys();
                int index = System.Array.IndexOf(jobActiveSkillArys[job], id);
                if (index != -1) jobActiveSkillArys[job][index] = -1;
            }
        }
    }

    public void SkillLevelChange(int job, int id, int lv)
    {
        switch (job)
        {
            case 0:
                myStatus.DuelistSkillLevelAry[id] += lv;
                break;
            case 1:
                myStatus.WarriorSkilklLevelAry[id] += lv;
                break;
            case 2:
                myStatus.KnightSkillLevelAry[id] += lv;
                break;
            case 3:
                myStatus.WizardSkillLevelAry[id] += lv;
                break;
            case 4:
                myStatus.BishopSkillLevelAry[id] += lv;
                break;
            case 5:
                myStatus.MaidenSkillLevelAry[id] += lv;
                break;
            default:
                break;
        }
    }
    public bool JpCheck(int job)
    {
        switch (job)
        {
            case 0:
                if (myStatus.JPDuelist == 0) return false;
                else return true;
            case 1:
                if (myStatus.JPWarrior == 0) return false;
                else return true;
            case 2:
                if (myStatus.JPKnight == 0) return false;
                else return true;
            case 3:
                if (myStatus.JPWizard == 0) return false;
                else return true;
            case 4:
                if (myStatus.JPBishop == 0) return false;
                else return true;
            case 5:
                if (myStatus.JPMaiden == 0) return false;
                else return true;
            default:
                return false;
        }
    }
    public void JpChange(int job, int jp)
    {
        switch (job)
        {
            case 0:
                myStatus.JPDuelist += jp;
                UpdateJobPointText(myStatus.JPDuelist);
                break;
            case 1:
                myStatus.JPWarrior += jp;
                UpdateJobPointText(myStatus.JPWarrior);
                break;
            case 2:
                myStatus.JPKnight += jp;
                UpdateJobPointText(myStatus.JPKnight);
                break;
            case 3:
                myStatus.JPWizard += jp;
                UpdateJobPointText(myStatus.JPWizard);
                break;
            case 4:
                myStatus.JPBishop += jp;
                UpdateJobPointText(myStatus.JPBishop);
                break;
            case 5:
                myStatus.JPMaiden += jp;
                UpdateJobPointText(myStatus.JPMaiden);
                break;
            default:
                break;
        }
    }
    public void UpdateJobPointText(int jp)
    {
        homeUIManager.jobPointText.text = $"<cspace=-0.01em>ジョブポイント：<size=160%>{jp}";
    }
}
