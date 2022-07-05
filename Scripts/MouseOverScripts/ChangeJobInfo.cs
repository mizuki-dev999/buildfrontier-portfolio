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
                selectJobNameText.text = "�f���G���X�g";
                jobInfoText.text = "�f���������Ǝ萔�œG��؂�|���퓬���B\n���̕����͖���������B\n����̈����ɒ����Ă���A�񓁗�����Ȑ퓬�X�^�C���ƂȂ�B\n�䂦�ɓ񓁗��̍ۂɒǉ����ʂ𔭊�����X�L�����������Ă���B\n���ӂȑ����͕����E�X�E���E�łł���B\n<size=70%>���񓁗��F���C���n���h�E�I�t�n���h���ɕЎ蕐��𑕔����Ă�����";
                equipGearText.text = "�����\����F�Ў茕/���茕/�Ў蕀/���蕀/�Ў胁�C�X/���胁�C�X/�_�K�[/�X�s�A/��";
                break;
            case 1:
                selectJobNameText.text = "";
                jobInfoText.text = "";
                equipGearText.text = "�����\����F�Ў茕/���茕/�Ў蕀/���蕀/�Ў胁�C�X/���胁�C�X/�_�K�[/�X�s�A/��";
                break;
            case 2:
                selectJobNameText.text = "";
                jobInfoText.text = "";
                equipGearText.text = "�����\����F�Ў茕/���茕/�Ў蕀/���蕀/�Ў胁�C�X/���胁�C�X/�_�K�[/�X�s�A/��";
                break;
            case 3:
                jobSymbolImage.sprite = jobSymbolSpriteAry[jobId-2];
                selectJobNameText.text = "�E�B�U�[�h";
                jobInfoText.text = "���Η͂Ȗ��@���J��o�����p�t�B\n�J��o�����@�ň�؍��؂𐁂���΂��B\n�ꔭ�̉Η͍͂�����MP����������A�A���ł��Ȃ��̂���_�B\n���ӂȑ����͉΁E�X�E���E���ł���B";
                equipGearText.text = "�����\����F�Ў��/�����/������/��";
                break;
            case 4:
                selectJobNameText.text = "";
                jobInfoText.text = "";
                equipGearText.text = "�����\����F�Ў��/�����/������/��";
                break;
            case 5:
                selectJobNameText.text = "";
                jobInfoText.text = "";
                equipGearText.text = "�����\����F�Ў��/�����/������/��";
                break;
            default:
                break;
        }
    }
}
