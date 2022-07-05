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
    /// スキルレベルを返す
    /// </summary>
    /// <param name="job">ジョブ番号</param>
    /// <param name="id">スキル番号</param>
    /// <returns>スキルレベル</returns>
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
