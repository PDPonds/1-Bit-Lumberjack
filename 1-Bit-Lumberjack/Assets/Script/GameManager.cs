using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Normal, Boss
}

public class GameManager : Singleton<GameManager>
{
    public delegate void AddCoinEvent();
    public event AddCoinEvent OnAddCoin;

    public delegate void RemoveCoinEvent();
    public event RemoveCoinEvent OnRemoveCoin;

    public delegate void NextStateEvent();
    public event NextStateEvent OnNextState;

    public delegate void NextPhaseEvent();
    public event NextPhaseEvent OnNextPhase;

    public delegate void ExitBossStateEvent();
    public event ExitBossStateEvent OnExitBossState;

    public delegate void UpgradeAxeEvent();
    public event UpgradeAxeEvent OnUpgradeAxe;

    public delegate void AddManaEvent();
    public event AddManaEvent OnAddMana;

    public delegate void RemoveManaEvent();
    public event RemoveManaEvent OnRemoveMana;

    #region Static Variable
    //Finish
    public static int curCoin = 0;
    public static float curMana = 0;
    public static int curPhase = 1;
    public static int curState = 1;
    public static int curLevelAxe = 1;
    public static int curStrikeLevel = 0;
    public static int curLootingLevel = 0;
    public static int curLumberjackLevel = 0;
    public static int curWoodpeckerLevel = 0;

    //Waiting
    public static int curTeamworkLevel = 0;

    #endregion

    [Header("===== Game State =====")]
    public GameState curGameState;
    [Header("===== Boss =====")]
    public float bossTime;
    [HideInInspector] public float curBossTime;

    [Header("===== Bird Gift =====")]
    [Range(0f, 1f)] public float minBirdDropPercent;
    [Range(0f, 1f)] public float maxBirdDropPercent;

    [Header("===== Phase And State =====")]
    public int maxStatePerPhase;

    [Header("===== Mana =====")]
    public float maxMana;
    public float mulToAddMana;

    [Header("===== Axe Damage =====")]
    [SerializeField] int startAxeDamage;
    [SerializeField] int startCostUpgradeAxe;
    [Header("- MulCost")]
    [SerializeField] int mulCostUpgradeAxe;
    [Header("- MulDamage")]
    public int mulDamagePerLevel;

    [Header("===== Team =====")]
    [Header("- Lumberjack")]
    public TeamMateController lumberjack;
    [Header("- Woodpecker")]
    public TeamMateController woodpecker;

    private void OnEnable()
    {
        EnemyController.Instance.OnEnemyDead += NextState;
        EnemyController.Instance.OnEnemyTakeDamage += AddMana;
    }

    private void Awake()
    {
        SaveSystem.Load();
        SaveSystem.Save();
    }

    private void Start()
    {
        SetupSkillLevel();
    }

    private void Update()
    {
        UpdateState();

        DisableLumberjack();
        DisableWoodpecker();
    }

    private void OnApplicationQuit()
    {
        SaveSystem.Save();
    }

    #region Game State

    public bool CheckGameState(GameState state)
    {
        return curGameState == state;
    }

    public void SwitchState(GameState state)
    {
        curGameState = state;
        switch (curGameState)
        {
            case GameState.Normal:
                UIManager.Instance.bossTimeBorder.SetActive(false);
                break;
            case GameState.Boss:
                UIManager.Instance.bossTimeBorder.SetActive(true);
                curBossTime = bossTime;
                break;
        }
    }

    void UpdateState()
    {
        switch (curGameState)
        {
            case GameState.Normal:
                break;
            case GameState.Boss:
                DecreaseBossTime();
                break;
        }
    }

    void DecreaseBossTime()
    {
        curBossTime -= Time.deltaTime;
        if (curBossTime < 0)
        {
            ResetPhaseAndState();
            OnExitBossState?.Invoke();
        }
    }

    #endregion

    #region Coin

    public void AddCoin(int amount)
    {
        curCoin += amount;
        OnAddCoin?.Invoke();
        SaveSystem.Save();
    }

    public void RemoveCoin(int amount)
    {
        curCoin -= amount;
        OnRemoveCoin?.Invoke();
        SaveSystem.Save();
    }

    public bool CheckCoin(int amount)
    {
        return curCoin >= amount;
    }

    #endregion

    #region Phase And State

    public void NextState()
    {
        if (CheckGameState(GameState.Normal))
        {
            if (curState < maxStatePerPhase - 1)
            {
                curState++;
            }
            else
            {
                curState++;
                SwitchState(GameState.Boss);
            }

        }
        else if (CheckGameState(GameState.Boss))
        {
            SwitchState(GameState.Normal);
            NextPhase();
        }

        OnNextState?.Invoke();
        SaveSystem.Save();

    }

    public void NextPhase()
    {
        curPhase++;
        curState = 1;
        OnNextPhase?.Invoke();
        SaveSystem.Save();
    }

    public void ResetPhaseAndState()
    {
        SwitchState(GameState.Normal);
        curState = 1;
        SaveSystem.Save();

    }

    #endregion

    #region Mana

    void AddMana()
    {
        curMana += mulToAddMana;
        if (curMana >= maxMana) curMana = maxMana;
        OnAddMana?.Invoke();
        SaveSystem.Save();
    }

    public void RemoveMana(float amount)
    {
        if (CheckMana(amount))
        {
            curMana -= amount;
            OnRemoveMana?.Invoke();
            SaveSystem.Save();
        }
    }

    public bool CheckMana(float amount)
    {
        return curMana >= amount;
    }

    #endregion

    #region Axe

    public int CalAxeDamage()
    {
        int levelMul = 0;
        if (curLevelAxe > 1) levelMul = curLevelAxe * mulDamagePerLevel;
        int result = startAxeDamage + levelMul;
        return result;
    }

    public int CalUpgradeAxeCost()
    {
        int result = curLevelAxe * mulCostUpgradeAxe;
        return result;
    }

    public void AddAxeLevel()
    {
        int cost = CalUpgradeAxeCost();
        RemoveCoin(cost);
        curLevelAxe++;
        PlayerManager.Instance.curAttackDamage = CalAxeDamage();
        OnUpgradeAxe?.Invoke();
    }

    #endregion

    #region Skill
    void SetupSkillLevel()
    {
        Skill strike = SkillManager.Instance.GetSkill("Strike");
        Skill looting = SkillManager.Instance.GetSkill("Looting");
        for (int i = 0; i < SkillManager.Instance.skills.Length; i++)
        {
            if (SkillManager.Instance.skills[i] == strike)
                SkillManager.Instance.skills[i].curLevel = curStrikeLevel;

            if (SkillManager.Instance.skills[i] == looting)
                SkillManager.Instance.skills[i].curLevel = curLootingLevel;
        }
    }

    #endregion

    #region Teammate

    void DisableLumberjack()
    {
        if (curLumberjackLevel > 0) lumberjack.gameObject.SetActive(true);
        else lumberjack.gameObject.SetActive(false);
    }

    void DisableWoodpecker()
    {
        if (curWoodpeckerLevel > 0) woodpecker.gameObject.SetActive(true);
        else woodpecker.gameObject.SetActive(false);
    }

    #endregion

}
