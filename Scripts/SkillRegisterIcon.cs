using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class SkillRegisterIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler
{
    private MyCharacterStatus myStatus;
    private HomeUIManager homeUIManager;
    private SkillDatabase.Skill skill;
    private int job;
    private int id;
    private GameObject draggingObj;
    private bool registed;
    public GameObject registedText;
    public Image skillImage;

    /// <summary>
    /// �ϐ��ɃZ�b�g����
    /// </summary>
    /// <param name="job">�W���u�ԍ�</param>
    /// <param name="id">�X�L���ԍ�</param>
    public void InitIcon(int _job, int _id)
    {
        myStatus = GameObject.Find("MyCharacterStatus").GetComponent<MyCharacterStatus>();
        homeUIManager = GameObject.Find("HomeUIManager").GetComponent<HomeUIManager>();
        job = _job;
        id = _id;
        skill = homeUIManager.skillDatabases[job].SkillDataList[id];
        SkillRegistChecker(job, id);
        skillImage.sprite = skill.sprite;
    }
    /// <summary>
    /// �X�L�����x����Ԃ�
    /// </summary>
    /// <param name="job">�W���u�ԍ�</param>
    /// <param name="id">�X�L���ԍ�</param>
    /// <returns>�X�L�����x��</returns>
    public int GetSkillLevel(int job, int id)
    {
        return job switch
        {
            0 => myStatus.DuelistSkillLevelAry[id],
            1 => myStatus.WarriorSkilklLevelAry[id],
            2 => myStatus.KnightSkillLevelAry[id],
            3 => myStatus.WarriorSkilklLevelAry[id],
            4 => myStatus.BishopSkillLevelAry[id],
            5 => myStatus.MaidenSkillLevelAry[id],
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
                    else textList.Add($"�G��{skillEffect.effectType} {skillEffect.EffectAmount * level}%");
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
    public void OnBeginDrag(PointerEventData eventData)
    {
        draggingObj = Instantiate(this.gameObject);
        draggingObj.GetComponent<SkillRegisterIcon>().enabled = false;
        draggingObj.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        draggingObj.transform.GetChild(1).gameObject.SetActive(false);
        draggingObj.transform.SetParent(this.transform.root.gameObject.transform);
        draggingObj.name = "draggingObj";
        skillImage.GetComponent<Image>().color = Color.gray;
    }
    public void OnDrag(PointerEventData eventData)
    {
        draggingObj.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        
        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("SkillSlot1")) SkillRegister(0);
            else if (hit.gameObject.CompareTag("SkillSlot2")) SkillRegister(1);
            else if (hit.gameObject.CompareTag("SkillSlot3")) SkillRegister(2);
            else if (hit.gameObject.CompareTag("SkillSlot4")) SkillRegister(3);
            else if (hit.gameObject.CompareTag("SkillSlot5")) SkillRegister(4);
            else if (hit.gameObject.CompareTag("SkillSlot6")) SkillRegister(5);
            else if (hit.gameObject.CompareTag("SkillSlot7")) SkillRegister(6);
            else if (hit.gameObject.CompareTag("SkillSlot8")) SkillRegister(7);
        }
        Destroy(draggingObj);
        skillImage.color = Color.white;
    }
    //�X�L�����X���b�g�ɓo�^
    public void SkillRegister(int slotId)
    {
        int[][] jobActiveSkillArys = myStatus.GetRegistedActiveSkillArys();

        for (int i = 0; i < jobActiveSkillArys[job].Length; i++)
        {
            jobActiveSkillArys[job][i] = (jobActiveSkillArys[job][i] == id) ? -1 : jobActiveSkillArys[job][i];
        }
        myStatus.DuelistActiveSkillSet[slotId] = id;
        for (int i = 0; i < homeUIManager.skillSlots.Length; i++)
        {
            homeUIManager.skillSlots[i].Init(job, jobActiveSkillArys[job][i]);
        }
        homeUIManager.SkillRegistChecker();
    }
    //�X�L������������Ă���Ȃ��(bool)regsited->true
    public void SkillRegistChecker(int job, int id)
    {
        switch (job)
        {
            case 0:
                if (myStatus.DuelistActiveSkillSet.Contains(id)) registed = true;
                else registed = false;
                break;
            case 1:
                if (myStatus.WarriorActiveSkillSet.Contains(id)) registed = true;
                else registed = false;
                break;
            case 2:
                if (myStatus.KnightActiveSkillSet.Contains(id)) registed = true;
                else registed = false;
                break;
            case 3:
                if (myStatus.WizardActiveSkillSet.Contains(id)) registed = true;
                else registed = false;
                break;
            case 4:
                if (myStatus.BishopActiveSkillSet.Contains(id)) registed = true;
                else registed = false;
                break;
            case 5:
                if (myStatus.MaidenActiveSkillSet.Contains(id)) registed = true;
                else registed = false;
                break;
            default:
                break;
        }
        if (registed) registedText.SetActive(true);
        else registedText.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeSkillName(job, id, homeUIManager.skillRegisterNameText);
        ChangeSkillInfo(GetSkillLevel(job, id), homeUIManager.skillRegisterInfoText);
        homeUIManager.skillRegisterLevelText.text = $"Lv{GetSkillLevel(job, id)}";
    }
    public int GetJob()
    {
        return this.job;
    }
    public int GetId()
    {
        return this.id;
    }
}
