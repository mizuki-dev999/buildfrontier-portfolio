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
    /// 変数にセットする
    /// </summary>
    /// <param name="job">ジョブ番号</param>
    /// <param name="id">スキル番号</param>
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
                    else textList.Add($"敵の{skillEffect.effectType} {skillEffect.EffectAmount * level}%");
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
    //スキルをスロットに登録
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
    //スキルが装備されているならば(bool)regsited->true
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
