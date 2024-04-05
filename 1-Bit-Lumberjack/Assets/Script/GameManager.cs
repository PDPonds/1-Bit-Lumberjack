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

    [Header("===== Game State =====")]
    public GameState curGameState;
    [Header("===== Boss =====")]
    public float bossTime;
    [HideInInspector] public float curBossTime;
    [Header("===== Coin =====")]
    public int curCoin;

    [Header("===== Phase And State =====")]
    public int maxStatePerPhase;
    [HideInInspector] public int curPhase = 1;
    [HideInInspector] public int curState = 1;

    [Header("===== SceneShake =====")]
    [SerializeField] float sceneShakeDuration;
    [SerializeField] float sceneShakeMagnitude;

    [Header("===== Axe Damage =====")]
    public int curLevelAxe = 1;
    [SerializeField] int startAxeDamage;
    [SerializeField] int startCostUpgradeAxe;
    [Header("- MulCost")]
    [SerializeField] int mulCostUpgradeAxe;
    [Header("- MulDamage")]
    public int mulDamagePerLevel;

    private void OnEnable()
    {
        EnemyController.Instance.OnEnemyDead += NextState;
    }

    private void Start()
    {
        curState = 1;
        curPhase = 1;
    }

    private void Update()
    {
        UpdateState();
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
    }

    public void RemoveCoin(int amount)
    {
        curCoin -= amount;
        OnRemoveCoin?.Invoke();
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
            else if (curState == maxStatePerPhase - 1)
            {
                SwitchState(GameState.Boss);
                curState++;
            }
        }
        else if (CheckGameState(GameState.Boss))
        {
            SwitchState(GameState.Normal);
            NextPhase();
        }

        OnNextState?.Invoke();

    }

    public void NextPhase()
    {
        curPhase++;
        curState = 1;
        OnNextPhase?.Invoke();
    }

    public void ResetPhaseAndState()
    {
        SwitchState(GameState.Normal);
        curState = 1;
    }

    #endregion

    #region Axe

    public int CalAxeDamage()
    {
        int levelMul = curLevelAxe * mulDamagePerLevel;
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
}
