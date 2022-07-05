using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectManager : MonoBehaviour
{
    public BattleManager battleManager;
    public SkillIcon[] skillIcons;
    //-----------------------------------------------------------------------------
    //自キャラ変化用----------------------------------------
    //基礎ステ
    [System.NonSerialized] public int myAtkNum = 0;
    [System.NonSerialized] public int myAtkRate = 0;
    [System.NonSerialized] public int myDefNum = 0;
    [System.NonSerialized] public int myDefRate = 0;
    [System.NonSerialized] public int mySpdRate = 0;
    [System.NonSerialized] public int myArmorPointNum = 0;
    [System.NonSerialized] public int myArmorPointRate = 0;
    //属性ダメ
    [System.NonSerialized] public int myPhisicalDmgNum = 0;
    [System.NonSerialized] public int myPhisicalDmgRate = 0;
    [System.NonSerialized] public int myFireDmgNum = 0;
    [System.NonSerialized] public int myFireDmgRate = 0;
    [System.NonSerialized] public int myIceDmgNum = 0;
    [System.NonSerialized] public int myIceDmgRate = 0;
    [System.NonSerialized] public int myThunderDmgNum = 0;
    [System.NonSerialized] public int myThunderDmgRate = 0;
    [System.NonSerialized] public int myWindDmgNum = 0;
    [System.NonSerialized] public int myWindDmgRate = 0;
    [System.NonSerialized] public int myShiningDmgNum = 0;
    [System.NonSerialized] public int myShiningDmgRate = 0;
    [System.NonSerialized] public int myDarknessDmgNum = 0;
    [System.NonSerialized] public int myDarknessDmgRate = 0;
    //属性耐性
    [System.NonSerialized] public int myPhisicalResist = 0;
    [System.NonSerialized] public int myFireResist = 0;
    [System.NonSerialized] public int myIceResist = 0;
    [System.NonSerialized] public int myThunderResist = 0;
    [System.NonSerialized] public int myWindResist = 0;
    [System.NonSerialized] public int myShiningResist = 0;
    [System.NonSerialized] public int myDarknessResist = 0;
    //その他
    [System.NonSerialized] public int myAutoHpRecoverNum = 0;
    [System.NonSerialized] public int myAutoHpRecoverRate = 0;
    [System.NonSerialized] public int myAutoMpRecoverNum = 0;
    [System.NonSerialized] public int myAutoMpRecoverRate = 0;
    [System.NonSerialized] public int myGuardRate = 0;
    //敵キャラ変化用------------------------------------------
    //基礎ステ
    [System.NonSerialized] public int enemyAtkRate = 100;
    [System.NonSerialized] public int enemyDefRate = 100;
    [System.NonSerialized] public int enemySpdRate = 100;//1~200
    [System.NonSerialized] public int enemyArmorPointRate = 100;
    //属性ダメ
    [System.NonSerialized] public int enemyPhisicalDmg = 100;
    [System.NonSerialized] public int enemyFireDmg = 100;
    [System.NonSerialized] public int enemyIceDmg = 100;
    [System.NonSerialized] public int enemyThunderDmg = 100;
    [System.NonSerialized] public int enemyWindDmg = 100;
    [System.NonSerialized] public int enemyShiningDmg = 100;
    [System.NonSerialized] public int enemyDarknessDmg = 100;
    //属性耐性
    [System.NonSerialized] public int enemyPhisicalResist = 0;
    [System.NonSerialized] public int enemyFireResist = 0;
    [System.NonSerialized] public int enemyIceResist = 0;
    [System.NonSerialized] public int enemyThunderResist = 0;
    [System.NonSerialized] public int enemyWindResist = 0;
    [System.NonSerialized] public int enemyShiningResist= 0;
    [System.NonSerialized] public int enemyDarknessResist = 0;
    //その他
    [System.NonSerialized] public int enemyAutoHpRecover = 100; 
    [System.NonSerialized] public int enemyGuardRate = 0;

    public void MyBuffInit()
    {
        return;
    }

    private void Update()
    {
        //キー入力
        #region
        if (Input.GetKey(KeyCode.Q))
        {
            skillIcons[0].ActiveSkill();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            skillIcons[1].ActiveSkill();
        }
        else if (Input.GetKey(KeyCode.E))
        {
            skillIcons[2].ActiveSkill();
        }
        else if (Input.GetKey(KeyCode.R))
        {
            skillIcons[3].ActiveSkill();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            skillIcons[4].ActiveSkill();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            skillIcons[5].ActiveSkill();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            skillIcons[6].ActiveSkill();
        }
        else if (Input.GetKey(KeyCode.F))
        {
            skillIcons[7].ActiveSkill();
        }
        #endregion
        //クールタイム処理
        for (int i=0; i<skillIcons.Length; i++)
        {
            if (skillIcons[i].GetIsCoolTime())
            {
                skillIcons[i].CoolTimeAnimation(Time.deltaTime);
            }
        }
    }
}
