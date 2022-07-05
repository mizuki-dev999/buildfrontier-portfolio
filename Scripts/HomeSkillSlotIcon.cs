using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class HomeSkillSlotIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler
{
    private MyCharacterStatus myStatus;
    private HomeUIManager homeUIManager;
    private SkillDatabase.Skill skill;
    private GameObject draggingObj;
    public Image skillImage;
    public int slotId;
    private int job;
    private int id;
    private bool canDrag;

    public void Init(int _job, int _id)
    {
        myStatus = GameObject.Find("MyCharacterStatus").GetComponent<MyCharacterStatus>();
        homeUIManager = GameObject.Find("HomeUIManager").GetComponent<HomeUIManager>();
        job = _job;
        id = _id;
        if (id == -1)
        {
            canDrag = false;
            skill = null;
            skillImage.color = new Color(1, 1, 1, 0);
        }
        else
        {
            canDrag = true;
            skill = homeUIManager.skillDatabases[job].SkillDataList[id];
            skillImage.color = new Color(1, 1, 1, 1);
            skillImage.sprite = skill.sprite;
        }
    }
    public int[] GetJobAndId()
    {
        int[] ary = {job, id};
        return ary;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canDrag) return;
        draggingObj = Instantiate(this.gameObject);
        draggingObj.GetComponent<HomeSkillSlotIcon>().enabled = false;
        draggingObj.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        draggingObj.transform.GetChild(1).gameObject.SetActive(false);
        draggingObj.transform.SetParent(this.transform.root.gameObject.transform);
        draggingObj.name = "draggingObj";
        skillImage.GetComponent<Image>().color = Color.gray;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag) return;
        draggingObj.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canDrag) return;
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        bool canRemove = true;
        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.name.Contains("SkillSlot")) canRemove = false;
        }
        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("SkillSlot1") && !(this.transform.GetChild(0).GetChild(0).GetChild(0).CompareTag("SkillSlot1"))) SkillSlotChanger(hit.gameObject.transform);
            else if (hit.gameObject.CompareTag("SkillSlot2") && !(this.transform.GetChild(0).GetChild(0).GetChild(0).CompareTag("SkillSlot2"))) SkillSlotChanger(hit.gameObject.transform);
            else if (hit.gameObject.CompareTag("SkillSlot3") && !(this.transform.GetChild(0).GetChild(0).GetChild(0).CompareTag("SkillSlot3"))) SkillSlotChanger(hit.gameObject.transform);
            else if (hit.gameObject.CompareTag("SkillSlot4") && !(this.transform.GetChild(0).GetChild(0).GetChild(0).CompareTag("SkillSlot4"))) SkillSlotChanger(hit.gameObject.transform);
            else if (hit.gameObject.CompareTag("SkillSlot5") && !(this.transform.GetChild(0).GetChild(0).GetChild(0).CompareTag("SkillSlot5"))) SkillSlotChanger(hit.gameObject.transform);
            else if (hit.gameObject.CompareTag("SkillSlot6") && !(this.transform.GetChild(0).GetChild(0).GetChild(0).CompareTag("SkillSlot6"))) SkillSlotChanger(hit.gameObject.transform);
            else if (hit.gameObject.CompareTag("SkillSlot7") && !(this.transform.GetChild(0).GetChild(0).GetChild(0).CompareTag("SkillSlot7"))) SkillSlotChanger(hit.gameObject.transform);
            else if (hit.gameObject.CompareTag("SkillSlot8") && !(this.transform.GetChild(0).GetChild(0).GetChild(0).CompareTag("SkillSlot8"))) SkillSlotChanger(hit.gameObject.transform);
            else if (canRemove) SkillRemove();
        }
        Destroy(draggingObj);
        if(skillImage.color.a != 0) skillImage.color = new Color(1, 1, 1, 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!canDrag) return;
        ChangeSkillName(job, id, homeUIManager.skillRegisterNameText);
        ChangeSkillInfo(GetSkillLevel(job, id), homeUIManager.skillRegisterInfoText);
        homeUIManager.skillRegisterLevelText.text = $"Lv{GetSkillLevel(job, id)}";
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
    public void SkillSlotChanger(Transform target)
    {
        int[][] jobActiveSkillArys = myStatus.GetRegistedActiveSkillArys();
        HomeSkillSlotIcon homeSkillSlotIcon = target.parent.parent.parent.GetComponent<HomeSkillSlotIcon>();
        int[] jobAndIdOfTarget = homeSkillSlotIcon.GetJobAndId();
        jobActiveSkillArys[job][homeSkillSlotIcon.slotId] = this.id; 
        homeSkillSlotIcon.Init(this.job, this.id);
        this.Init(jobAndIdOfTarget[0], jobAndIdOfTarget[1]);
        jobActiveSkillArys[job][slotId] = id;
    }
    public void SkillRemove()
    {
        int[][] jobActiveSkillArys = myStatus.GetRegistedActiveSkillArys();
        jobActiveSkillArys[job][slotId] = -1;
        foreach (Transform obj in homeUIManager.hasActiveSkillPanel.transform)
        {
            SkillRegisterIcon target = obj.gameObject.GetComponent<SkillRegisterIcon>();
            target.SkillRegistChecker(target.GetJob(), target.GetId());
        }
        Init(job, -1);
    }
}
