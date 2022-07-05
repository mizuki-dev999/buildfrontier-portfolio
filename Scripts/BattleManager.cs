using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using KanKikuchi.AudioManager;
using DG.Tweening;

public class BattleManager : MonoBehaviour
{
    [System.NonSerialized] public float myAtkWaitTime = 0; // ���L�����̍U���҂�����
    [System.NonSerialized] public float enemyAtkWaitTime = 0; // �G�̂̍U���҂�����
    //-----------------------------------------------------------------------------
    [System.NonSerialized] public GameObject myCharacterStatus; // ���L�������̃I�u�W�F�N�g
    [System.NonSerialized] public MyCharacterStatus myStatus; // ���L�������̃X�N���v�g
    //-----------------------------------------------------------------------------
    public EnemyDatebase enemyDatabase; // �G���(�X�N���v�^�u���I�u�W�F�N�g)
    [System.NonSerialized] public EnemyDatebase.EnemyStatus enemyStatus = null; // �G���J�E���g�����G����퓬�Ɏg�p���邽�߂̃C���X�^���X
    [System.NonSerialized] public int enemyId; //�G��ID
    public Image enemyImage; // �G�̉摜(�Q�[���I�u�W�F�N�g)
    public Transform enemyImageTrans; // �G�̉摜(�Q�[���I�u�W�F�N�g)
    //-----------------------------------------------------------------------------
    [System.NonSerialized] public GameObject inventoryObj; // �C���x���g���I�u�W�F�N�g
    [System.NonSerialized] public Inventory inventory; // �C���x���g���I�u�W�F�N�g�̃X�N���v�g
    //----------------------------------------------------------------------------
    public ItemDatabase itemDatabase; // �A�C�e�����(�X�N���v�^�u���I�u�W�F�N�g)
    [System.NonSerialized] public ItemDatabase.ItemData dropItemData; // �h���b�v�����A�C�e�����
    [System.NonSerialized] public int dropItemId; // �h���b�v�����A�C�e��ID
    //-----------------------------------------------------------------------------
    public MapDatabase mapDatabase; // �}�b�v���(�X�N���v�^�u���I�u�W�F�N�g)
    [System.NonSerialized] public MapDatabase.MapData mapData; // �}�b�v���
    [System.NonSerialized] public int mapId = MapManager.staticMapId; // �}�b�vId (MapManager����擾)
    [System.NonSerialized] public int currentstage = 0; // �}�b�v�i�s�x
    //------------------------------------------------------------------------------------------
    public List<int> waitSkillList = new(); //�X�L���ҋ@���X�g
    public StageUIManager stageUI; // �X�e�[�WUI���i��I�u�W�F�N�g
    public Button reStartButton; // �Ē���{�^��
    [System.NonSerialized] public int myCurrentHp; // ������HP
    [System.NonSerialized] public int myCurrentMp; // ������MP
    [System.NonSerialized] public int enemyCurrentHp; // �G�̗̑�
    [System.NonSerialized] public int maxStage; // �ő�X�e�[�W��
    [System.NonSerialized] public float totalTime;
    [System.NonSerialized] public int minute;
    [System.NonSerialized] public float second;
    [System.NonSerialized] public float oldSecond;
    public float fadetime = 0; // �t�F�[�h�o�ߎ���
    public float fadeEndTime = 0.4f; // �t�F�[�h�I���܂ł̎���
    public float alpha; // Image�̃A���t�@�l
    public float deltaTime = 0; //�@�o�ߎ���
    public float stopTime = 0.5f; // �ҋ@����
    public Image attackEffectImage; //���L�����̃G�t�F�N�g�摜.��������A�j���[�^�[�R���|�[�l���g���擾���G�t�F�N�g�����s����
    public GameObject damagePanel;
    public BattleBgmManager battleBgmManager;
    [Header("�_���[�W���e�L�X�g�I�u�W�F�N�g�v���n�u")]
    public GameObject damagePrefab;
    public GameObject enemyDamagePrefab;
    public GameObject myAttackMissPrefab;
    public GameObject enemyAttackMissPrefab;
    public GameObject hpRecoverPrefab;
    public GameObject enemyhpRecoverPrefab;
    public GameObject mpRecoverPrefab;
    public GameObject getExpPregab;
    public SkillDatabase[] skillDatabases;
    public SkillEffectManager skillEffectManager;

    public Phase phase;
    public enum Phase
    {
        EncountPhase, // �G���J�E���g
        BattlePhase, // �퓬
        EnemyFadeOut, // �G���j���o
        Defeat, // �G���j
        Death,
        Clear,
        InventoryMax,
        Stop,
        TimeUp,
    }
    private void Awake()
    {
        reStartButton.interactable = false;
    }

    void Start()
    {
        myCharacterStatus = GameObject.Find("MyCharacterStatus"); // Home�V�[���Ő�������MyCharacterStatus���擾
        myStatus = myCharacterStatus.GetComponent<MyCharacterStatus>(); // script���擾
        inventoryObj = GameObject.Find("Inventory");
        inventory = inventoryObj.GetComponent<Inventory>();
        //---------------------------------------------------------
        GetMapData(mapId); //MapData�̎擾
        battleBgmManager.StartBattleBgm(mapId);
        DOTween.Clear(true);
        stageUI.UpdateBackGround(mapData.sprite); // �w�i�摜�̍X�V
        currentstage = 0;
        myStatus.SetStatus(skillDatabases);
        myCurrentHp = myStatus.GetHp();
        myCurrentMp = myStatus.GetMp();
        stageUI.UpdateMyHpText(myCurrentHp);
        stageUI.UpdateMyMpText(myCurrentMp);
        stageUI.UpdateExpText();
        stageUI.HasMoneyText.text = $"<cspace=-0.1em>{myStatus.Money:N0}";
        stageUI.dropCommonItemText.text = $"<cspace=-0.1em>{myStatus.dropCommonItemNum}";
        stageUI.dropRareItemText.text = $"<cspace=-0.1em>{myStatus.dropRareItemNum}";
        stageUI.dropEpicItemText.text = $"<cspace=-0.1em>{myStatus.dropEpicItemNum}";
        stageUI.dropLegendaryItemText.text = $"<cspace=-0.1em>{myStatus.dropLegendaryItemNum}";
        string difficultyLevel = (mapData.Id <= 42) ? "Noraml" : "Hard";
        stageUI.stageNameText.text = $"{mapData.Name} ({difficultyLevel})";
        stageUI.hpBar.fillAmount = 1;
        stageUI.mpBar.fillAmount = 1;
        stageUI.expBar.fillAmount = (float)myStatus.Exp / (float)stageUI.expTable.GetNextExp(myStatus.Level);
        stageUI.UpdateLevelText(myStatus.Level);
        stopTime = 1.0f; // �V�[���J�ڎ��̃t�F�[�h�̑ҋ@���Ԃ̏�����
        TimerInit();
        phase = Phase.EncountPhase;
    }
    void Update()
    {
        switch (phase)
        {
            case Phase.EncountPhase:
                BattleStart();
                break;
            case Phase.BattlePhase:
                BattleSystem();
                CountDown();
                myAtkWaitTime += Time.deltaTime;
                enemyAtkWaitTime += Time.deltaTime;
                break;
            case Phase.EnemyFadeOut: //�G�t�F�[�h�A�E�g�X�e�b�v
                if (fadetime <= fadeEndTime)
                {

                    alpha = 1.0f - fadetime / fadeEndTime; // 0:���� 1:�s����
                    stageUI.enemyImage.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
                    fadetime += Time.deltaTime;
                }
                else
                {
                    fadetime = 0;
                    phase = Phase.Defeat;
                }
                break;
            case Phase.Defeat:
                DefeatEnemy();
                break;
            case Phase.Death:
                break;
            case Phase.Clear:
                break;
            case Phase.InventoryMax:
                break;
            case Phase.Stop:
                deltaTime += Time.deltaTime;
                if (deltaTime > stopTime)
                {
                    deltaTime = 0;
                    phase = Phase.BattlePhase;
                }
                break;
            case Phase.TimeUp:
                break;
            default:
                break;

        }
    }

    // �o�g���J�n����
    public void BattleStart()
    {
        stageUI.UpdateStageData(currentstage, mapData.StageNum); // �X�e�[�W�i�s�x�̍X�V
        EncountEnemy(currentstage); // �G�Ƃ̃G���J�E���g���������擾
        if (currentstage == maxStage && mapData.Id % 3 == 0) battleBgmManager.StartBossBattleBgm(mapData.Id);
        alpha = 255.0f; // �����x������
        stageUI.enemyImage.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        enemyCurrentHp = enemyStatus.Hp;
        stageUI.enemyHpBar.fillAmount = 1;
        stageUI.enemyPeacentageText.text = $"<size=75%>{(int)Mathf.Ceil((float)enemyCurrentHp * 100 / (float)enemyStatus.Hp)}%</size>";
        myAtkWaitTime = 0;
        enemyAtkWaitTime = 0;
        TimerInit();
        //�G�̃o�t���Z�b�g
        if (currentstage == 0)
        {
            phase = Phase.Stop;
        }
        else
        {
            phase = Phase.BattlePhase;
        }
    }

    public void TimerInit()
    {
        minute = 1;
        second = 30;
        totalTime = minute * 60 + second;
        oldSecond = 0;
        stageUI.timerText.text = minute.ToString() + ":" + ((int)second).ToString("00");
    }

    public void CountDown()
    {
        //�@��U�g�[�^���̐������Ԃ��v���G
        totalTime = minute * 60 + second;
        totalTime -= Time.deltaTime;

        //�@�Đݒ�
        minute = (int)totalTime / 60;
        second = totalTime - minute * 60;

        //�@�^�C�}�[�\���pUI�e�L�X�g�Ɏ��Ԃ�\������
        if ((int)second != (int)oldSecond)
        {
            stageUI.timerText.text = minute.ToString() + ":" + ((int)second).ToString("00");
            AutoRecover();
        }
        oldSecond = second;
        //�@�������Ԉȉ��ɂȂ�����R���\�[���Ɂw�������ԏI���x�Ƃ����������\������
        if (totalTime <= 0f)
        {
            phase = Phase.TimeUp;
        }
    }

    // �G���J�E���g����
    public void EncountEnemy(int currentstage)
    {
        maxStage = mapData.StageNum - 1;
        int enemyId = (currentstage < maxStage) ? mapData.EncountEnemyIdList[Random.Range(0, mapData.EncountEnemyIdList.Length)] : mapData.BossEnemyId;
        GetEnemyStatus(enemyId);
    }

    // �G���J�E���g�����G�̃X�e�[�^�X���擾
    public void GetEnemyStatus(int enemyId)
    {
        foreach (EnemyDatebase.EnemyStatus data in enemyDatabase.enemyStatusList)
        {
            // ���X�g�ɓo�^����Ă���GID�Əƍ�
            if (data.Id == enemyId)
            {
                stageUI.UpdateEnemyImage(data.sprite);
                stageUI.UpdateEnemyNameText(data.Name);
                enemyStatus = DeepCopyEnemyStatus(data); //�f�B�[�v�R�s�[
                break;
            }
        }
    }

    public EnemyDatebase.EnemyStatus DeepCopyEnemyStatus(EnemyDatebase.EnemyStatus data)
    {
        EnemyDatebase.EnemyStatus deepCopyEnemyStatus = new()
        {
            Exp = data.Exp,
            Money = data.Money,
            DropItemIdList = data.DropItemIdList,
            Hp = data.Hp,
            Atk = data.Atk,
            Def = data.Def,
            Spd = data.Spd,
            ArmorPoint = data.ArmorPoint,
            PhisicalDmg = data.PhisicalDmg,
            FireDmg = data.PhisicalDmg,
            IceDmg = data.IceDmg,
            ThunderDmg = data.ThunderDmg,
            WindDmg = data.WindDmg,
            ShiningDmg = data.ShiningDmg,
            DarknessDmg = data.DarknessDmg,
            PhisicalResist = data.PhisicalResist,
            FireResist = data.FireResist,
            IceResist = data.IceResist,
            ThunderResist = data.ThunderResist,
            WindResist = data.WindResist,
            ShiningResist = data.ShiningResist,
            DarknessResist = data.DarknessResist,
            CastTime = data.CastTime,
            SkillId = data.SkillId,
            AutoHpRecover = data.AutoHpRecover,
            GuardRate = data.GuardRate,
        };
        return deepCopyEnemyStatus;
    }

    // �h���b�v�����A�C�e�������擾
    public void GetDropItemData(int dropItemId)
    {
        foreach (ItemDatabase.ItemData data in itemDatabase.ItemDataList)
        {
            // ���X�g�ɓo�^����Ă���A�C�e��ID�Əƍ�
            if (data.Id == dropItemId)
            {
                dropItemData = data;
                break;
            }
        }
    }

    // �}�b�v�����擾
    public void GetMapData(int mapId)
    {
        foreach (MapDatabase.MapData data in mapDatabase.MapDataList)
        {
            if (data.Id == mapId)
            {
                mapData = data;
                break;
            }
        }
    }

    // �퓬����---------------------------------------------------------------------------------------
    #region
    public void BattleSystem()
    {
        if (myAtkWaitTime >= 1f / myStatus.GetSpd(skillEffectManager.mySpdRate) && enemyAtkWaitTime >= 1 / enemyStatus.GetSpd(skillEffectManager.enemySpdRate))
        {
            MyAttack();
            EnemyAttack();
        }
        // �����̍U��
        else if (myAtkWaitTime >= 1f / myStatus.GetSpd(skillEffectManager.mySpdRate))
        {
            MyAttack();
        }
        // �G�̍U��
        else if (enemyAtkWaitTime >= 1f / enemyStatus.GetSpd(skillEffectManager.enemySpdRate))
        {
            EnemyAttack();
        }
    }
    /// <summary>
    /// �_���[�W�e�L�X�g�̃A�j���[�V����
    /// </summary>
    /// <param name="dmgTextObj">�e�L�X�g�I�u�W�F�N�g</param>
    public void DamageTextEffect(GameObject dmgTextObj)
    {
        dmgTextObj.transform.SetParent(damagePanel.transform);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(dmgTextObj.transform.DOScale(new Vector3(1.5f, 1.5f), 0.2f))
            .Append(dmgTextObj.transform.DOScale(new Vector3(1f, 1f), 0.2f))
            .Append(dmgTextObj.transform.DOMoveY(dmgTextObj.GetComponent<RectTransform>().position.y + 80, 1))
            .Join(dmgTextObj.GetComponent<CanvasGroup>().DOFade(0, 1))
            .AppendCallback(() =>
            {
                Destroy(dmgTextObj);
            });
        sequence.Play().SetLink(dmgTextObj);
    }
    /// <summary>
    /// �����̒ʏ�U��
    /// </summary>
    public void MyAttack()
    {
        myAtkWaitTime = 0;
        float damageRate = DamageRateCalculation(myStatus.GetAtk(skillEffectManager.myAtkNum, skillEffectManager.myAtkRate), enemyStatus.GetAtk(skillEffectManager.enemyAtkRate));
        if (Random.value < (float)(enemyStatus.GetGuardRate(skillEffectManager.enemyGuardRate)) / 100) //Guard
        {
            Debug.Log("guard");
            GameObject damageObject = Instantiate(myAttackMissPrefab, new Vector3(Random.Range(910, 1011), Random.Range(490, 590), 0), Quaternion.identity);
            DamageTextEffect(damageObject);
            damageObject.GetComponent<TextMeshProUGUI>().text = "Guard";
        }
        else if (damageRate == 0) //miss
        {
            AttackEffect("NormalSord");
            SEManager.Instance.Play(SEPath.MISS_SORD);
            GameObject damageObject = Instantiate(myAttackMissPrefab, new Vector3(Random.Range(910, 1011), Random.Range(490, 590), 0), Quaternion.identity);
            DamageTextEffect(damageObject);
        }
        else if (damageRate > 1.0) //critical
        {
            //���b�l����
            int finalDmg = (int)System.Math.Round(GetMyBaseDmg() * damageRate, System.MidpointRounding.AwayFromZero);
            finalDmg = (finalDmg < 0) ? 0 : finalDmg;
            AttackEffect("CriticalSord");
            SEManager.Instance.Play(SEPath.CRITICAL_SORD_ATTACK);
            HitEnenyReaction();
            GameObject damageObject = Instantiate(damagePrefab, new Vector3(Random.Range(910, 1011), Random.Range(490, 590), 0), Quaternion.identity);
            DamageTextEffect(damageObject);
            damageObject.GetComponent<TextMeshProUGUI>().text = $"<line-height=55%><size=70%><cspace=-0.1em>CRITICAL</cspace></size>\n<cspace=-0.05em>{finalDmg}</cspace>";
            enemyCurrentHp = Mathf.Clamp(enemyCurrentHp - finalDmg, 0, enemyStatus.Hp);
        }
        else //normal
        {
            int finalDmg = (int)System.Math.Round(GetMyBaseDmg() * damageRate, System.MidpointRounding.AwayFromZero) - enemyStatus.GetArmorPoint(skillEffectManager.enemyArmorPointRate);
            finalDmg = (finalDmg < 0) ? 0 : finalDmg;
            AttackEffect("NormalSord");
            SEManager.Instance.Play(SEPath.SORD_ATTACK);
            HitEnenyReaction();
            GameObject damageObject = Instantiate(damagePrefab, new Vector3(Random.Range(910, 1011), Random.Range(490, 590), 0), Quaternion.identity);
            DamageTextEffect(damageObject);
            damageObject.GetComponent<TextMeshProUGUI>().text = $"<cspace=-0.05em>{finalDmg}</cspace>";
            enemyCurrentHp = Mathf.Clamp(enemyCurrentHp - finalDmg, 0, enemyStatus.Hp);
        }
        stageUI.enemyHpBar.fillAmount = (float)enemyCurrentHp / (float)enemyStatus.Hp;
        stageUI.enemyPeacentageText.text = $"<size=75%>{(int)Mathf.Ceil((float)enemyCurrentHp * 100 / (float)enemyStatus.Hp)}%</size>";
        //�G�̐�������
        if (enemyCurrentHp == 0)
        {
            SEManager.Instance.Play(SEPath.DFEAT_ENEMY);
            phase = Phase.EnemyFadeOut;
        }
    }
    /// <summary>
    /// �G�̒ʏ�U��
    /// </summary>
    public void EnemyAttack()
    {
        SEManager.Instance.Play(SEPath.ENEMY_ATTACK);
        enemyAtkWaitTime = 0;
        float damageRate = DamageRateCalculation(enemyStatus.GetAtk(skillEffectManager.enemyAtkRate), myStatus.GetDef(skillEffectManager.myDefNum, skillEffectManager.myDefRate));
        if (Random.value < (float)(myStatus.GetGuardRate(skillEffectManager.myGuardRate)) / 100) //Guard
        {
            GameObject damageObject = Instantiate(enemyAttackMissPrefab, new Vector3(Random.Range(600, 885), Random.Range(91, 131), 0), Quaternion.identity);
            DamageTextEffect(damageObject);
            damageObject.GetComponent<TextMeshProUGUI>().text = "Guard";
        }
        else if (damageRate == 0) //miss
        {
            GameObject damageObject = Instantiate(enemyAttackMissPrefab, new Vector3(Random.Range(600, 885), Random.Range(91, 131), 0), Quaternion.identity);
            DamageTextEffect(damageObject);
        }
        else if (damageRate > 1.0) //critical
        {
            int finalDmg = (int)System.Math.Round(GetEnemyBaseDmg() * damageRate, System.MidpointRounding.AwayFromZero);
            finalDmg = (finalDmg < 0) ? 0 : finalDmg;
            GameObject damageObject = Instantiate(enemyDamagePrefab, new Vector3(Random.Range(600, 885), Random.Range(91, 131), 0), Quaternion.identity);
            DamageTextEffect(damageObject);
            damageObject.GetComponent<TextMeshProUGUI>().text = $"<line-height=55%><size=70%><cspace=-0.1em>CRITICAL</cspace></size>\n<cspace=-0.05em>{finalDmg}</cspace>";
            myCurrentHp = Mathf.Clamp(myCurrentHp - finalDmg, 0, myStatus.Hp);
        }
        else //normal
        {
            int finalDmg = (int)System.Math.Round(GetEnemyBaseDmg() * damageRate, System.MidpointRounding.AwayFromZero) - myStatus.GetArmorPoint(skillEffectManager.myArmorPointNum, skillEffectManager.myArmorPointRate);
            finalDmg = (finalDmg < 0) ? 0 : finalDmg;
            GameObject damageObject = Instantiate(enemyDamagePrefab, new Vector3(Random.Range(600, 885), Random.Range(91, 131), 0), Quaternion.identity);
            DamageTextEffect(damageObject);
            damageObject.GetComponent<TextMeshProUGUI>().text = $"<cspace=-0.05em>{finalDmg}</cspace>";
            myCurrentHp = Mathf.Clamp(myCurrentHp - finalDmg, 0, myStatus.Hp);
        }
        stageUI.hpBar.fillAmount = (float)myCurrentHp / (float)myStatus.Hp;
        stageUI.UpdateMyHpText(myCurrentHp);
        if (myCurrentHp == 0)
        {
            //Debug.Log($"{enemyStatus.Name}�ɓ|���ꂽ�c");
            reStartButton.interactable = true; // �Ē���{�^���̗L����
            phase = Phase.Death;
        }
    }
    /// <summary>
    /// �G���_���[�W���󂯂��Ƃ��̓G�̃��A�N�V�������o
    /// </summary>
    public void HitEnenyReaction()
    {
        Sequence sequenceColor = DOTween.Sequence();
        Sequence sequenceJump = DOTween.Sequence();
        sequenceColor.Append(enemyImage.DOColor(Color.red, 0.05f))
            .Append(enemyImage.DOColor(Color.white, 0.05f));
        sequenceJump.Append(enemyImageTrans.DOMove(new Vector3(enemyImageTrans.position.x, enemyImageTrans.position.y + 5f, 0f), 0.05f))
            .Append(enemyImageTrans.DOMove(new Vector3(enemyImageTrans.position.x, 540 + 13, 0f), 0.05f));
        sequenceColor.Play().SetLink(enemyImage.gameObject);
        sequenceJump.Play().SetLink(enemyImage.gameObject);
    }
    /// <summary>
    /// �����̃_���[�W�ɓG�̒�R�l������
    /// </summary>
    /// <returns>��R�l������̓G�ւ̃_���[�W</returns>
    public int GetMyBaseDmg()
    {
        int totalDmg = (int)System.Math.Round(
            myStatus.GetPhisicalDmg(skillEffectManager.myPhisicalDmgNum, skillEffectManager.myPhisicalDmgRate) * (100 - enemyStatus.GetPhisicalResist(skillEffectManager.enemyPhisicalResist) / 100f)
            + myStatus.GetFireDmg(skillEffectManager.myFireDmgNum, skillEffectManager.myFireDmgRate) * (100 - enemyStatus.GetFireResist(skillEffectManager.enemyFireResist) / 100f)
            + myStatus.GetIceDmg(skillEffectManager.myIceDmgNum, skillEffectManager.myIceDmgRate) * (100 - enemyStatus.GetIceResist(skillEffectManager.enemyIceResist) / 100f)
            + myStatus.GetThunderDmg(skillEffectManager.myThunderDmgNum, skillEffectManager.myThunderDmgRate) * (100 - enemyStatus.GetThunderResist(skillEffectManager.enemyThunderResist) / 100f)
            + myStatus.GetWindDmg(skillEffectManager.myWindDmgNum, skillEffectManager.myWindDmgRate) * (100 - enemyStatus.GetWindResist(skillEffectManager.enemyWindResist) / 100f)
            + myStatus.GetShiningDmg(skillEffectManager.myShiningDmgNum, skillEffectManager.myShiningDmgRate) * (100 - enemyStatus.GetShiningResist(skillEffectManager.enemyShiningResist) / 100f)
            + myStatus.GetDarknessDmg(skillEffectManager.myDarknessDmgNum, skillEffectManager.myDarknessDmgRate) * (100 - enemyStatus.GetDarknessResist(skillEffectManager.enemyDarknessResist) / 100f), System.MidpointRounding.AwayFromZero);
        totalDmg += Random.Range(0, totalDmg / 10 + 1);
        return totalDmg;
    }
    /// <summary>
    /// �G�̃_���[�W�Ɏ����̒�R�l������
    /// </summary>
    /// <returns>��R�l������̎����ւ̃_���[�W</returns>
    public int GetEnemyBaseDmg()
    {
        int totalDmg = (int)System.Math.Round(
            enemyStatus.GetPhisicalDmg(skillEffectManager.enemyPhisicalDmg) * (100 - myStatus.GetPhisicalResist(skillEffectManager.enemyPhisicalResist) / 100f)
            + enemyStatus.GetFireDmg(skillEffectManager.enemyFireDmg) * (100 - myStatus.GetFireResist(skillEffectManager.enemyFireResist) / 100f)
            + enemyStatus.GetIceDmg(skillEffectManager.enemyIceDmg) * (100 - myStatus.GetIceResist(skillEffectManager.enemyIceResist) / 100f)
            + enemyStatus.GetThunderDmg(skillEffectManager.enemyThunderDmg) * (100 - myStatus.GetThunderResist(skillEffectManager.enemyThunderResist) / 100f)
            + enemyStatus.GetWindDmg(skillEffectManager.enemyWindDmg) * (100 - myStatus.GetWindResist(skillEffectManager.enemyWindResist) / 100f)
            + enemyStatus.GetShiningDmg(skillEffectManager.enemyShiningDmg) * (100 - myStatus.GetShiningResist(skillEffectManager.enemyShiningResist) / 100f)
            + enemyStatus.GetDarknessDmg(skillEffectManager.enemyDarknessDmg) * (100 - myStatus.GetDarknessResist(skillEffectManager.enemyDarknessResist) / 100f), System.MidpointRounding.AwayFromZero);
        totalDmg += Random.Range(0, totalDmg / 10 + 1);
        return totalDmg;
    }

    /// <summary>
    /// �_���[�W�{����Ԃ�
    /// </summary>
    /// <param name="oa">�U�����̍U���\��</param>
    /// <param name="da">�h�䑤�̖h��\��</param>
    /// <returns>�_���[�W�{��</returns>
    public float DamageRateCalculation(int oa, int da)
    {
        if (oa <= da) return 0;
        int pth = oa - da;
        //�U����������
        if (Random.value > pth / 100)
        {
            return 0;//miss
        }
        if (pth <= 70)
        {
            return pth / 70;
        }
        else if (pth > 100)
        {
            int cliticalChance = Random.Range(1, pth + 1);
            if (cliticalChance >= 90)
            {
                if (cliticalChance <= 104) return 1.1f;
                else if (cliticalChance <= 119) return 1.2f;
                else if (cliticalChance <= 129) return 1.3f;
                else if (cliticalChance <= 134) return 1.4f;
                else return 1.5f;
            }
            else return 1;
        }
        else
        {
            if (Random.value > pth / 100) return 1.1f;
            else return 1;
        }
    }

    public void AutoRecover()
    {
        int recoverHp;
        int recoverMp;
        int recoverEnemyHp;
        if (myCurrentHp + myStatus.GetAutoHpRecover(skillEffectManager.myAutoHpRecoverNum, skillEffectManager.myAutoHpRecoverRate) > myStatus.Hp)
        {
            myCurrentHp = myStatus.Hp;
            recoverHp = myStatus.Hp - myCurrentHp;
        }
        else
        {
            myCurrentHp += myStatus.GetAutoHpRecover(skillEffectManager.myAutoHpRecoverNum, skillEffectManager.myAutoHpRecoverRate);
            recoverHp = myStatus.GetAutoHpRecover(skillEffectManager.myAutoHpRecoverNum, skillEffectManager.myAutoHpRecoverRate);
        }

        if (myCurrentMp + myStatus.GetAutoMpRecover(skillEffectManager.myAutoMpRecoverNum, skillEffectManager.myAutoMpRecoverRate) > myStatus.Mp)
        {
            myCurrentMp = myStatus.Mp;
            recoverMp = myStatus.Mp - myCurrentMp;
        }
        else
        {
            myCurrentMp += myStatus.GetAutoMpRecover(skillEffectManager.myAutoMpRecoverNum, skillEffectManager.myAutoMpRecoverRate);
            recoverMp = myStatus.GetAutoMpRecover(skillEffectManager.myAutoMpRecoverNum, skillEffectManager.myAutoMpRecoverRate);
        }

        if (enemyCurrentHp + enemyStatus.GetAutoHpRecover(skillEffectManager.enemyAutoHpRecover) > enemyStatus.Hp)
        {
            enemyCurrentHp = enemyStatus.Hp;
            recoverEnemyHp = enemyStatus.Hp - enemyCurrentHp;
        }
        else
        {
            enemyCurrentHp += enemyStatus.GetAutoHpRecover(skillEffectManager.enemyAutoHpRecover);
            recoverEnemyHp = enemyStatus.GetAutoHpRecover(skillEffectManager.enemyAutoHpRecover);
        }
        /*�_���[�W�\�L�����邽�߂̃R�[�h
        GameObject recoverHpObject = Instantiate(hpRecoverPrefab, new Vector3(Random.Range(600, 885), Random.Range(91, 131), 0), Quaternion.identity);
        DamageTextEffect(recoverHpObject);
        recoverHpObject.GetComponent<TextMeshProUGUI>().text = $"<cspace=-0.05em>+{recoverHp}</cspace>";
        GameObject recoverMpObject = Instantiate(mpRecoverPrefab, new Vector3(1328.8f, 111, 0), Quaternion.identity);
        DamageTextEffect(recoverMpObject);
        recoverMpObject.GetComponent<TextMeshProUGUI>().text = $"<cspace=-0.05em>+{recoverMp}</cspace>";
        GameObject recoverEnemyHpObject = Instantiate(enemyhpRecoverPrefab, new Vector3(Random.Range(910, 1011), Random.Range(490, 590), 0), Quaternion.identity);
        DamageTextEffect(recoverEnemyHpObject);
        recoverEnemyHpObject.GetComponent<TextMeshProUGUI>().text = $"<cspace=-0.05em>+{recoverEnemyHp}</cspace>";
        */
    }
    #endregion
    // �G���j���̏���---------------------------------------------------------------------------------------
    public void DefeatEnemy()
    {
        SEManager.Instance.Play(SEPath.MONEY_DROP);
        float getExp = (enemyStatus.Exp * 1) * (100 + Mathf.Max(myStatus.ExpGetRate, 0)) / 100f;
        if (myStatus.Level != 100) stageUI.LevelUp((int)System.Math.Round(getExp, System.MidpointRounding.AwayFromZero));
        myStatus.Money = Mathf.Min(myStatus.Money + enemyStatus.Money, 999999999);
        stageUI.HasMoneyText.text = $"<cspace=-0.1em>{myStatus.Money:N0}";
        stageUI.UpdateExpText();
        if (currentstage == maxStage)
        {
            // �����h���b�v�������L��(�m��h���b�v)
            if (!myStatus.ClearMapIdList.Contains(mapId))
            {
                myStatus.ClearMapIdList.Add(mapId);
            }
            // �N���A���b�Z�[�W�\��
            reStartButton.interactable = true; // �Ē���{�^���̗L����
            phase = Phase.Clear; //�@�}�b�v�N���A
        }
        else
        {
            // �h���b�v����(30%)
            if (Random.value > 0.7f)
            {
                SEManager.Instance.Play(SEPath.ITEM_DROP);
                // �����h���b�v�������L��
                int dropItemId = enemyStatus.DropItemIdList[Random.Range(0, enemyStatus.DropItemIdList.Length)];
                GetDropItemData(dropItemId);
                inventory.ItemDropAndGet(dropItemData);
            }
            // �q�ɂ����^�����ǂ�������
            if (inventory.WeaponInventory.Count > inventory.maxNum || inventory.ArmorInventory.Count > inventory.maxNum || inventory.AccessoryInventory.Count > inventory.maxNum)
            {
                phase = Phase.InventoryMax;
            }
            else // ���̃X�e�[�W��
            {
                currentstage++; // �X�e�[�W�i�s�x����
                phase = Phase.EncountPhase;
            }
        }
    }

    public void ChangePhaseStop()
    {
        phase = Phase.Stop;
    }

    /// <summary>
    /// �����ɖ��O�����Ďw�肵���U���G�t�F�N�g�����s
    /// </summary>
    public void AttackEffect(string efName)
    {
        attackEffectImage.GetComponent<Animator>().SetTrigger(efName);
    }
    public int GetSkillLevel(int job, int id)
    {
        int[][] allSkillLevelArys = myStatus.GetAllSkillLevelArys();
        return allSkillLevelArys[job][id];
    }
    /// <summary>
    /// �U���X�L��
    /// </summary>
    /// <param name="skillDamageRate">�X�L�����ʗ�</param>
    public void MySkillAttack(float skillDamageRate)
    {
        int finalDmg = 0;
        float damageRate = DamageRateCalculation(myStatus.GetAtk(skillEffectManager.myAtkNum, skillEffectManager.myAtkRate), enemyStatus.GetAtk(skillEffectManager.enemyAtkRate));
        if (Random.value < (float)(enemyStatus.GetGuardRate(skillEffectManager.enemyGuardRate)) / 100) //Guard
        {
            Debug.Log("guard");
            GameObject damageObject = Instantiate(myAttackMissPrefab, new Vector3(Random.Range(910, 1011), Random.Range(490, 590), 0), Quaternion.identity);
            DamageTextEffect(damageObject);
            damageObject.GetComponent<TextMeshProUGUI>().text = "Guard";
        }
        else if (damageRate == 0) //miss
        {
            AttackEffect("NormalSord");
            SEManager.Instance.Play(SEPath.MISS_SORD);
            GameObject damageObject = Instantiate(myAttackMissPrefab, new Vector3(Random.Range(910, 1011), Random.Range(490, 590), 0), Quaternion.identity);
            DamageTextEffect(damageObject);
        }
        else if (damageRate > 1.0) //critical
        {
            //���b�l����
            finalDmg = (int)System.Math.Round(GetMyBaseDmg() * damageRate * (100f + skillDamageRate) / 100f, System.MidpointRounding.AwayFromZero);
            finalDmg = (finalDmg < 0) ? 0 : finalDmg;
            AttackEffect("CriticalSord");
            SEManager.Instance.Play(SEPath.CRITICAL_SORD_ATTACK);
            HitEnenyReaction();
            GameObject damageObject = Instantiate(damagePrefab, new Vector3(Random.Range(910, 1011), Random.Range(490, 590), 0), Quaternion.identity);
            DamageTextEffect(damageObject);
            damageObject.GetComponent<TextMeshProUGUI>().text = $"<line-height=55%><size=70%><cspace=-0.1em>CRITICAL</cspace></size>\n<cspace=-0.05em>{finalDmg}</cspace>";
            enemyCurrentHp = Mathf.Clamp(enemyCurrentHp - finalDmg, 0, enemyStatus.Hp);
        }
        else //normal
        {
            finalDmg = (int)System.Math.Round(GetMyBaseDmg() * damageRate, System.MidpointRounding.AwayFromZero) - enemyStatus.GetArmorPoint(skillEffectManager.enemyArmorPointRate);
            finalDmg = (finalDmg < 0) ? 0 : finalDmg;
            AttackEffect("NormalSord");
            SEManager.Instance.Play(SEPath.SORD_ATTACK);
            HitEnenyReaction();
            GameObject damageObject = Instantiate(damagePrefab, new Vector3(Random.Range(910, 1011), Random.Range(490, 590), 0), Quaternion.identity);
            DamageTextEffect(damageObject);
            damageObject.GetComponent<TextMeshProUGUI>().text = $"<cspace=-0.05em>{finalDmg}</cspace>";
            enemyCurrentHp = Mathf.Clamp(enemyCurrentHp - finalDmg, 0, enemyStatus.Hp);
        }
        stageUI.enemyHpBar.fillAmount = (float)enemyCurrentHp / (float)enemyStatus.Hp;
        stageUI.enemyPeacentageText.text = $"<size=75%>{(int)Mathf.Ceil((float)enemyCurrentHp * 100 / (float)enemyStatus.Hp)}%</size>";
        //�G�̐�������
        if (enemyCurrentHp == 0)
        {
            SEManager.Instance.Play(SEPath.DFEAT_ENEMY);
            phase = Phase.EnemyFadeOut;
        }
    }
    /// <summary>
    /// HP�񕜃X�L��
    /// </summary>
    public void RecoverMyHp(float skillAmount)
    {
        int recoverHp = (int)System.Math.Round(myStatus.GetHp()*(skillAmount/100f), System.MidpointRounding.AwayFromZero);
        myCurrentHp = Mathf.Clamp(myCurrentHp + recoverHp, 0, myStatus.GetHp()); 
        GameObject recoverHpObject = Instantiate(hpRecoverPrefab, new Vector3(Random.Range(600, 885), Random.Range(91, 131), 0), Quaternion.identity);
        DamageTextEffect(recoverHpObject);
        recoverHpObject.GetComponent<TextMeshProUGUI>().text = $"<cspace=-0.05em>+{recoverHp}</cspace>";
    }
    /// <summary>
    /// MP�񕜃X�L��
    /// </summary>
    public void RecoverMyMp(float skillAmount)
    {
        int recoverMp = (int)System.Math.Round(myStatus.GetMp() * (skillAmount / 100f), System.MidpointRounding.AwayFromZero);
        myCurrentMp = Mathf.Clamp(myCurrentMp + recoverMp, 0, myStatus.GetMp());
        GameObject recoverMpObject = Instantiate(mpRecoverPrefab, new Vector3(1328.8f, 111, 0), Quaternion.identity);
        DamageTextEffect(recoverMpObject);
        recoverMpObject.GetComponent<TextMeshProUGUI>().text = $"<cspace=-0.05em>+{recoverMp}</cspace>";
    }
}
