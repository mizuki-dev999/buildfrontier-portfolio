using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ChangeJobInfo : MonoBehaviour, IPointerEnterHandler
{
    public TextMeshProUGUI selectJobNameText;
    public TextMeshProUGUI jobInfoText;
    public TextMeshProUGUI equipGearText;
    public GameObject jobSymbol;
    public Image jobSymbolImage;
    public Sprite[] jobSymbolSpriteAry;
    public int jobId;
    public void OnPointerEnter(PointerEventData eventData)
    {
        jobSymbol.SetActive(true);
        switch (jobId)
        {
            case 0:
                jobSymbolImage.sprite = jobSymbolSpriteAry[jobId];
                selectJobNameText.text = "デュエリスト";
                jobInfoText.text = "素早い動きと手数で敵を切り倒す戦闘狂。\nその舞いは命を刈り取る。\n武器の扱いに長けており、二刀流が主な戦闘スタイルとなる。\nゆえに二刀流の際に追加効果を発揮するスキルを所持している。\n得意な属性は物理・氷・風・闇である。\n<size=70%>※二刀流：メインハンド・オフハンド共に片手武器を装備している状態";
                equipGearText.text = "装備可能武器：片手剣/両手剣/片手斧/両手斧/片手メイス/両手メイス/ダガー/スピア/盾";
                break;
            case 1:
                selectJobNameText.text = "";
                jobInfoText.text = "";
                equipGearText.text = "装備可能武器：片手剣/両手剣/片手斧/両手斧/片手メイス/両手メイス/ダガー/スピア/盾";
                break;
            case 2:
                selectJobNameText.text = "";
                jobInfoText.text = "";
                equipGearText.text = "装備可能武器：片手剣/両手剣/片手斧/両手斧/片手メイス/両手メイス/ダガー/スピア/盾";
                break;
            case 3:
                jobSymbolImage.sprite = jobSymbolSpriteAry[jobId-2];
                selectJobNameText.text = "ウィザード";
                jobInfoText.text = "高火力な魔法を繰り出す魔術師。\n繰り出す魔法で一切合切を吹き飛ばす。\n一発の火力は高いがMP消費が激しく、連発できないのが弱点。\n得意な属性は火・氷・雷・風である。";
                equipGearText.text = "装備可能武器：片手杖/両手杖/魔道具/盾";
                break;
            case 4:
                selectJobNameText.text = "";
                jobInfoText.text = "";
                equipGearText.text = "装備可能武器：片手杖/両手杖/魔道具/盾";
                break;
            case 5:
                selectJobNameText.text = "";
                jobInfoText.text = "";
                equipGearText.text = "装備可能武器：片手杖/両手杖/魔道具/盾";
                break;
            default:
                break;
        }
    }
}
