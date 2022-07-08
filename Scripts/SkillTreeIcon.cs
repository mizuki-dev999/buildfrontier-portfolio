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
            if (skillEffect.effectType == SkillDatabase.Skill.SkillEffect.EffectType.�U���X�L��)
            {
                dmgRate = (float)(100 + skillEffect.EffectAmount * level) / 100;
            }
        }
        string cost = (skill.Cost == 0) ? $"����MP�F�[�[�[\n" : $"����MP�F{skill.Cost * level}\n";
        string rate = (dmgRate == 0) ? $"�З́F�[�[�[\n" : $"�З́F{dmgRate}�{\n";
        string ct = (skill.Cost == 0) ? $"�N�[���^�C���F�[�[�[\n" : $"�N�[���^�C���F{skill.CoolTime}�b\n";
        string plusEffect = "";
        homeUIManager.skillInfoText.text = $"<line-height=80%>��ށF{skill.type}\n" +
             cost + rate + ct + "<line-height=70%>���ʁF\n<size=88%>" +
             GetSkillExplanation(level)+"</line-height>\n" + plusEffect;
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
        if(skill.EffectTime != 0) textList.Add($"{skill.EffectTime}�b��");
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
                    if(skillEffect.target == 0)textList.Add($"���g��{skillEffect.effectType} +{skillEffect.EffectAmount * level}");
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

    public void OnSkillLevelUpBtn(Image[] beforeLines, Image[] nextLines, SkillDatabase.Skill skill, int job, int id)
    {
        bool flag = true;
        homeUIManager.ClickSound();
        if (myStatus.Level < getLevel)
        {
            homeUIManager.ShowAlertImage("�K���ɕK�v�ȃ��x���ɒB���Ă��܂���");
            flag = false;
        }
        if (skill.necessarySkillId != -1)
        {
            if (GetSkillLevel(job, skill.necessarySkillId) == 0)
            {
                homeUIManager.ShowAlertImage("�K���ɕK�v�ȃX�L�����K�����Ă��܂���");
                flag = false;
            }
        }
        if (GetSkillLevel(job, id) == skill.MaxLevel)
        {
            homeUIManager.ShowAlertImage("���̃X�L���̃��x���͏���ɒB���܂���");
            flag = false;
        }
        if (!JpCheck(job))
        {
            homeUIManager.ShowAlertImage("�W���u�|�C���g������܂���");
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
                homeUIManager.ShowAlertImage("<size=80%>���̃X�L���̏K���ɕK�v�Ȃ��ߖ��K���ɂł��܂���");
                flag = false;
            }
        }
        if (GetSkillLevel(job, id) == 0 && flag)
        {
            homeUIManager.ShowAlertImage("���̃X�L���̃��x���͉����ɒB���܂���");
            flag = false;
        }
        if (myStatus.Level < getLevel && flag)
        {
            homeUIManager.ShowAlertImage("�K���ɕK�v�ȃ��x���ɒB���Ă��܂���");
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
        homeUIManager.jobPointText.text = $"<cspace=-0.01em>�W���u�|�C���g�F<size=160%>{jp}";
    }
}
