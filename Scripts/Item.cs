using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    // �A�C�e���̑������
    public string Name;
    public int Id;
    public Sprite sprite;
    public int Rare;
    public int Level;
    public int Money;
    public bool Equipped = false; //�������Ă��邩
    public bool New = false; //���肳��Ă���G������
    public enum ItemType
    {
        MainHand,
        OffHand,
        TwoHand,
        Armor,
        Accessory,
    }
    public ItemType itemType;
    public enum DetailType
    {
        //�Ў�
        �Ў茕,
        �Ў蕀,
        �Ў胁�C�X,
        �_�K�[,
        �Ў��,
        //����
        ���茕,
        ���蕀,
        ���胁�C�X,
        �X�s�A,
        �����,
        //�I�t�n���h
        ��,
        ������,
        //�h��
        �w����,
        �{�f�B�A�[�}�[,
        �K���g���b�g,
        ���b�O�A�[�}�[,
        //�A�N�Z
        �A�N�Z�T���[,
    }
    public DetailType detailType;

    // ��b�p�����[�^
    public int Hp;
    public int HpRate;
    public int Mp;
    public int MpRate;
    public int Atk;
    public int AtkRate;
    public int Def;
    public int DefRate;
    public float Spd;
    public int ArmorPoint;
    public int ArmorUp;
    public int ArmorRate;

    //�����_���[�W
    public int PhisicalDmg = 0;
    public int FireDmg = 0;
    public int IceDmg = 0;
    public int ThunderDmg = 0;
    public int WindDmg = 0;
    public int ShiningDmg = 0;
    public int DarknessDmg = 0;
    //�����␳�l(%)
    public int PhisicalRate = 0;
    public int FireRate = 0;
    public int IceRate = 0;
    public int ThunderRate = 0;
    public int WindRate = 0;
    public int ShiningRate = 0;
    public int DarknessRate = 0;
    //��R�l
    public int PhisicalResist = 0;
    public int FireResist = 0;
    public int IceResist = 0;
    public int ThunderResist = 0;
    public int WindResist = 0;
    public int ShiningResist = 0;
    public int DarknessResist = 0;
    //���̑�
    public int SpdRate; //�U�����x�␳
    public int AutoHpRecover; //����HP��
    public int AutoHpRecoverRate; //����HP��(%)
    public int AutoMpRecover; // ����MP��
    public int AutoMpRecoverRate; // ����MP��(%)
    public int GuardRate; //�K�[�h�m��
    public int ExpGetRate; // �擾�o���ʑ���
}
