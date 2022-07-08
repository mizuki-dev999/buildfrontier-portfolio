using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillEffectManager : MonoBehaviour
{
    public BattleManager battleManager;
    public SkillIcon[] skillIcons;
    private List<Coroutine> myCoroutines = new();
    private List<Coroutine> enemyCoroutines = new();
    private int[] myValiableStatus = Enumerable.Repeat<int>(0, 33).ToArray();
    private int[] enemyValiableStatus = Enumerable.Repeat<int>(0, 20).ToArray();

    public void SkillSet()
    {
        for (int i = 0; i < skillIcons.Length; i++)
        {
            int[][] arys = battleManager.myStatus.GetRegistedActiveSkillArys();
            skillIcons[i].Init((int)battleManager.myStatus.job, arys[(int)battleManager.myStatus.job][i]);
        }
    }
    private void Update()
    {
        //オートスキル処理
        if (battleManager.myStatus.AutoSkillMode)
        {
            for (int i = 0; i < skillIcons.Length; i++)
            {
                if (!skillIcons[i].GetIsCoolTime())
                {
                    skillIcons[i].ActiveSkill();
                }
            }
        }
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
    public void MyValiableInit()
    {
        myValiableStatus = Enumerable.Repeat<int>(0, 33).ToArray();
    }
    public void EnemyValiableInit()
    {
        enemyValiableStatus = Enumerable.Repeat<int>(0, 20).ToArray();
    }
    //味方
    public void BeginMyEffect(int index, int amount, float effectTime)
    {
        myValiableStatus[index] += amount;
        myCoroutines.Add(StartCoroutine(FinishMyEffect(index, amount, effectTime)));
    }
    public IEnumerator FinishMyEffect(int index, int amount, float effectTime)
    {
        yield return new WaitForSeconds(effectTime);
        myValiableStatus[index] -= amount;
    }
    /// <summary>
    /// 自身にかかっている効果をすべて初期化する
    /// </summary>
    public void ResetMyEffect()
    {
        for (int i = 0; i < myCoroutines.Count; i++)
        {
            StopCoroutine(myCoroutines[i]);
        }
        myCoroutines.Clear();
        MyValiableInit();
    }
    //敵
    public void BeginEnemyEffect(int index, int amount, float effectTime)
    {
        enemyValiableStatus[index] += amount;
        myCoroutines.Add(StartCoroutine(FinishEnemyEffect(index, amount, effectTime)));
    }
    public IEnumerator FinishEnemyEffect(int index, int amount, float effectTime)
    {
        yield return new WaitForSeconds(effectTime);
        enemyValiableStatus[index] -= amount;
    }
    /// <summary>
    /// 敵にかかっている効果をすべて初期化する
    /// </summary>
    public void ResetEnemyEffect()
    {
        for (int i = 0; i < enemyCoroutines.Count; i++)
        {
            StopCoroutine(enemyCoroutines[i]);
        }
        enemyCoroutines.Clear();
        EnemyValiableInit();
    }
    //ゲッター
    //自分
    #region
    //基礎ステ
    public int myAtkNum()
    {
        return myValiableStatus[0];
    }
    public int myAtkRate()
    {
        return myValiableStatus[1];
    }
    public int myDefNum()
    {
        return myValiableStatus[2];
    }
    public int myDefRate()
    {
        return myValiableStatus[3];
    }
    public int myArmorPointNum()
    {
        return myValiableStatus[4];
    }
    public int myArmorPointRate()
    {
        return myValiableStatus[5];
    }
    //属性ダメ
    public int myPhisicalDmgNum()
    {
        return myValiableStatus[6];
    }
    public int myPhisicalDmgRate()
    {
        return myValiableStatus[7];
    }
    public int myFireDmgNum()
    {
        return myValiableStatus[8];
    }
    public int myFireDmgRate()
    {
        return myValiableStatus[9];
    }
    public int myIceDmgNum()
    {
        return myValiableStatus[10];
    }
    public int myIceDmgRate()
    {
        return myValiableStatus[11];
    }
    public int myThunderDmgNum()
    {
        return myValiableStatus[12];
    }
    public int myThunderDmgRate()
    {
        return myValiableStatus[13];
    }
    public int myWindDmgNum()
    {
        return myValiableStatus[14];
    }
    public int myWindDmgRate()
    {
        return myValiableStatus[15];
    }
    public int myShiningDmgNum()
    {
        return myValiableStatus[16];
    }
    public int myShiningDmgRate()
    {
        return myValiableStatus[17];
    }
    public int myDarknessDmgNum()
    {
        return myValiableStatus[18];
    }
    public int myDarknessDmgRate()
    {
        return myValiableStatus[19];
    }
    //耐性
    public int myPhisicalResist()
    {
        return myValiableStatus[20];
    }
    public int myFireResist()
    {
        return myValiableStatus[21];
    }
    public int myIceResist()
    {
        return myValiableStatus[22];
    }
    public int myThunderResist()
    {
        return myValiableStatus[23];
    }
    public int myWindResist()
    {
        return myValiableStatus[24];
    }
    public int myShiningResist()
    {
        return myValiableStatus[25];
    }
    public int myDarknessResist()
    {
        return myValiableStatus[26];
    }
    //その他
    public int mySpdRate()
    {
        return myValiableStatus[27];
    }
    public int myAutoHpRecoverNum()
    {
        return myValiableStatus[28];
    }
    public int myAutoHpRecoverRate()
    {
        return myValiableStatus[29];
    }
    public int myAutoMpRecoverNum()
    {
        return myValiableStatus[30];
    }
    public int myAutoMpRecoverRate()
    {
        return myValiableStatus[31];
    }
    public int myGuardRate()
    {
        return myValiableStatus[32];
    }
    #endregion
    //敵
    #region
    //基礎ステ
    public int enemyAtkRate()
    {
        return enemyValiableStatus[0]+100;
    }
    public int enemyDefRate()
    {
        return enemyValiableStatus[1]+100;
    }
    public int enemyArmorPointRate()
    {
        return enemyValiableStatus[2]+100;
    }
    //属性ダメ
    public int enemyPhisicalDmg()
    {
        return enemyValiableStatus[3] + 100;
    }
    public int enemyFireDmg()
    {
        return enemyValiableStatus[4] + 100;
    }
    public int enemyIceDmg()
    {
        return enemyValiableStatus[5] + 100;
    }
    public int enemyThunderDmg()
    {
        return enemyValiableStatus[6] + 100;
    }
    public int enemyWindDmg()
    {
        return enemyValiableStatus[7] + 100;
    }
    public int enemyShiningDmg()
    {
        return enemyValiableStatus[8] + 100;
    }
    public int enemyDarknessDmg()
    {
        return enemyValiableStatus[9] + 100;
    }
    //耐性
    public int enemyPhisicalResist()
    {
        return enemyValiableStatus[10];
    }
    public int enemyFireResist()
    {
        return enemyValiableStatus[11];
    }
    public int enemyIceResist()
    {
        return enemyValiableStatus[12];
    }
    public int enemyThunderResist()
    {
        return enemyValiableStatus[13];
    }
    public int enemyWindResist()
    {
        return enemyValiableStatus[14];
    }
    public int enemyShiningResist()
    {
        return enemyValiableStatus[15];
    }
    public int enemyDarknessResist()
    {
        return enemyValiableStatus[16];
    }
    //その他
    public int enemySpdRate()
    {
        return enemyValiableStatus[17] + 100;
    }
    public int enemyAutoHpRecover()
    {
        return enemyValiableStatus[18] + 100;
    }
    public int enemyGuardRate()
    {
        return enemyValiableStatus[19];
    }
    #endregion
}
