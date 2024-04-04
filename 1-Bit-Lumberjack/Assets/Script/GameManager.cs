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

    [Header("===== Game State =====")]
    public GameState curGameState;

    [Header("===== Coin =====")]
    public int curCoin;

    [Header("===== Phase And State =====")]
    public int maxStatePerPhase;
    [HideInInspector] public int curPhase = 1;
    [HideInInspector] public int curState = 1;

    [Header("===== SceneShake =====")]
    [SerializeField] float sceneShakeDuration;
    [SerializeField] float sceneShakeMagnitude;

    private void OnEnable()
    {
        EnemyController.Instance.OnEnemyDead += NextState;
        EnemyController.Instance.OnEnemyTakeDamage += PlaySceneShake;
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

    #region Game FeedBack

    void PlaySceneShake()
    {
        StartCoroutine(SceneShake(sceneShakeDuration, sceneShakeMagnitude));
    }

    IEnumerator SceneShake(float duration, float magnitude)
    {
        Vector3 originalPos = Camera.main.transform.localPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.localPosition = originalPos;
    }

    #endregion

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
                break;
            case GameState.Boss:
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
                break;
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

    #endregion

}
