using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyDatebase : ScriptableObject
{
    public List<EnemyStatus> enemyStatusList = new List<EnemyStatus>();

    [System.Serializable]
    public class EnemyStatus
    {
        // �G�̑������
        public string Name;
        public Sprite sprite;
        public int Id;
        public int Rare;
        public int Exp = 0;
        public int Money = 0;
        public int[] DropItemIdList;
        //��v�X�e�[�^�X
        public int Hp = 0;
        public int Atk = 0; //�U���\��
        public int Def = 0; //�h��\��
        public float Spd = 0; //�U�����x 
        public int ArmorPoint = 0;
        //�����_���[�W
        public int PhisicalDmg = 0;
        public int FireDmg = 0;
        public int IceDmg = 0;
        public int ThunderDmg = 0;
        public int WindDmg = 0;
        public int ShiningDmg = 0;
        public int DarknessDmg = 0;
        //��R�l(0~80%)
        public int PhisicalResist = 0;
        public int FireResist = 0;
        public int IceResist = 0;
        public int ThunderResist = 0;
        public int WindResist = 0;
        public int ShiningResist = 0;
        public int DarknessResist = 0;
        //���̑�
        public float CastTime = 0; //�X�L�������܂ł̎���
        public int SkillId;
        public int AutoHpRecover = 0; // ����HP��
        public int GuardRate = 0; //�K�[�h�m��

        //��b�X�e
        #region
        public int GetAtk(int buffRate)
        {
            return (buffRate <= 0) ? 0 : (int)System.Math.Round((float)(Atk) * (float)(buffRate) / 100, System.MidpointRounding.AwayFromZero);
        }
        public int GetDef(int buffRate)
        {
            return (buffRate <= 0) ? 0 : (int)System.Math.Round((float)(Def) * (float)(buffRate) / 100, System.MidpointRounding.AwayFromZero);
        }
        public int GetSpd(int buffRate)
        {
            return (int)System.Math.Round((float)(Spd) * (float)(Mathf.Clamp(buffRate, 1, 200)) / 100, System.MidpointRounding.AwayFromZero);
        }
        public int GetArmorPoint(int buffRate)
        {
            return (buffRate <= 0) ? 0 : (int)System.Math.Round((float)(ArmorPoint) * (float)(buffRate) / 100, System.MidpointRounding.AwayFromZero);
        }
        #endregion
        //�����_��
        #region
        public int GetPhisicalDmg(int buffRate = 100)
        {
            return (buffRate <= 0) ? 0 : (int)System.Math.Round((float)(PhisicalDmg) * (float)(buffRate) / 100, System.MidpointRounding.AwayFromZero);
        }
        public int GetFireDmg(int buffRate = 100)
        {
            return (buffRate <= 0) ? 0 : (int)System.Math.Round((float)(FireDmg) * (float)(buffRate) / 100, System.MidpointRounding.AwayFromZero);
        }
        public int GetIceDmg(int buffRate = 100)
        {
            return (buffRate <= 0) ? 0 : (int)System.Math.Round((float)(IceDmg) * (float)(buffRate) / 100, System.MidpointRounding.AwayFromZero);
        }
        public int GetThunderDmg(int buffRate = 100)
        {
            return (buffRate <= 0) ? 0 : (int)System.Math.Round((float)(ThunderDmg) * (float)(buffRate) / 100, System.MidpointRounding.AwayFromZero);
        }
        public int GetWindDmg(int buffRate = 100)
        {
            return (buffRate <= 0) ? 0 : (int)System.Math.Round((float)(WindDmg) * (float)(buffRate) / 100, System.MidpointRounding.AwayFromZero);
        }
        public int GetShiningDmg(int buffRate = 100)
        {

            return (buffRate <= 0) ? 0 : (int)System.Math.Round((float)(ShiningDmg) * (float)(buffRate) / 100, System.MidpointRounding.AwayFromZero);
        }
        public int GetDarknessDmg(int buffRate = 100)
        {
            return (buffRate <= 0) ? 0 : (int)System.Math.Round((float)(DarknessDmg) * (float)(buffRate) / 100, System.MidpointRounding.AwayFromZero);
        }
        #endregion
        //�����ϐ�
        #region
        public int GetPhisicalResist(int buffNum = 0)
        {
            return Mathf.Min(PhisicalResist + buffNum, 80);
        }
        public int GetFireResist(int buffNum = 0)
        {
            return Mathf.Min(FireResist + buffNum, 80);
        }
        public int GetIceResist(int buffNum = 0)
        {
            return Mathf.Min(IceResist + buffNum, 80);
        }
        public int GetThunderResist(int buffNum = 0)
        {
            return Mathf.Min(ThunderResist + buffNum, 80);
        }
        public int GetWindResist(int buffNum = 0)
        {
            return Mathf.Min(WindResist + buffNum, 80);
        }
        public int GetShiningResist(int buffNum = 0)
        {
            return Mathf.Min(ShiningResist + buffNum, 80);
        }
        public int GetDarknessResist(int buffNum = 0)
        {
            return Mathf.Min(DarknessResist + buffNum, 80);
        }
        #endregion
        //���̑�
        #region
        public int GetAutoHpRecover(int buffRate = 100)
        {
            return (buffRate <= 0) ? 0 : (int)System.Math.Round((float)(AutoHpRecover) * (float)(buffRate) / 100, System.MidpointRounding.AwayFromZero);
        }
        public int GetGuardRate(int buffRate = 0)
        {
            return Mathf.Clamp(GuardRate + buffRate, 0, 100);
        }
        #endregion
        public void juju()
        {
           double i =  Random.value;
        }
    }
}
