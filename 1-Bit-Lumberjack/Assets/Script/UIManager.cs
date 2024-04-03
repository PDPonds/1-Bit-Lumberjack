using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("===== Coin =====")]
    [SerializeField] TextMeshProUGUI coinText;
    [Header("===== Phase =====")]
    [SerializeField] TextMeshProUGUI phaseText;
    [Header("===== State =====")]
    [SerializeField] TextMeshProUGUI stateText;

    private void OnEnable()
    {
        GameManager.Instance.OnAddCoin += UpdateCoin;
        GameManager.Instance.OnRemoveCoin += UpdateCoin;
        GameManager.Instance.OnNextState += UpdateState;
        GameManager.Instance.OnNextPhase += UpdatePhase;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnAddCoin -= UpdateCoin;
        GameManager.Instance.OnRemoveCoin -= UpdateCoin;
        GameManager.Instance.OnNextState -= UpdateState;
        GameManager.Instance.OnNextPhase -= UpdatePhase;

    }

    private void Start()
    {
        UpdateCoin();
        UpdateState();
        UpdatePhase();
    }

    void UpdateCoin()
    {
        coinText.text = GameManager.Instance.curCoin.ToString();
    }

    void UpdatePhase()
    {
        phaseText.text = GameManager.Instance.curPhase.ToString();
    }

    void UpdateState()
    {
        stateText.text =
            $"{GameManager.Instance.curState} / " +
            $"{GameManager.Instance.maxStatePerPhase}";

    }

}
