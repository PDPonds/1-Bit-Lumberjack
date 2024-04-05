using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    delegate void OnFuncFinsh();

    [Header("===== Coin =====")]
    public Transform coinIcon;
    [SerializeField] TextMeshProUGUI coinText;
    [Header("===== Boss =====")]
    [SerializeField] Image hpFill;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI bossNameText;
    public GameObject bossTimeBorder;
    [SerializeField] Image bossTimeFill;
    [SerializeField] TextMeshProUGUI bossTimeText;
    [Header("===== Phase =====")]
    [SerializeField] TextMeshProUGUI phaseText;
    [Header("===== State =====")]
    [SerializeField] TextMeshProUGUI stateText;
    [Header("===== Damage =====")]
    [SerializeField] TextMeshProUGUI damageCountText;
    [Header("===== Menu =====")]
    [SerializeField] Button axeButton;
    [SerializeField] GameObject axeBorder;
    [SerializeField] Button teamButton;
    [SerializeField] GameObject teamBorder;
    [SerializeField] Button achievementButton;
    [SerializeField] GameObject achievementBorder;
    [SerializeField] Button[] closeMenus;
    [Header("===== Setting =====")]
    [SerializeField] Button openSettingButton;
    [SerializeField] GameObject setting;
    [SerializeField] GameObject settingBorder;
    [SerializeField] Button closeSettingButton;

    private void OnEnable()
    {
        GameManager.Instance.OnAddCoin += UpdateCoin;
        GameManager.Instance.OnRemoveCoin += UpdateCoin;
        GameManager.Instance.OnNextState += UpdateState;
        GameManager.Instance.OnNextPhase += UpdatePhase;
        EnemyController.Instance.OnEnemyTakeDamage += UpdateHP;
        EnemyController.Instance.OnEnemyDead += UpdateHP;
        GameManager.Instance.OnExitBossState += UpdateState;
    }

    private void Awake()
    {
        axeButton.onClick.AddListener(() => Show(axeBorder));
        teamButton.onClick.AddListener(() => Show(teamBorder));
        achievementButton.onClick.AddListener(() => Show(achievementBorder));
        foreach (Button b in closeMenus)
        {
            b.onClick.AddListener(CloseMenu);
        }
        openSettingButton.onClick.AddListener(OpenSetting);
        closeSettingButton.onClick.AddListener(CloseSetting);
    }

    private void Start()
    {
        UpdateCoin();
        UpdateState();
        UpdatePhase();
        UpdateHP();
        UpdateDamage();
    }

    private void Update()
    {
        UpdateBossTime();
    }

    void UpdateBossTime()
    {
        if (bossTimeBorder.activeSelf)
        {
            float cur = GameManager.Instance.curBossTime;
            float max = GameManager.Instance.bossTime;
            float pecent = cur / max;
            bossTimeFill.fillAmount = pecent;
            bossTimeText.text = cur.ToString("N2");
        }
    }

    void UpdateDamage()
    {
        damageCountText.text = PlayerManager.Instance.curAttackDamage.ToString();
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

    void UpdateHP()
    {
        int max = EnemyController.Instance.maxHP;
        int cur = EnemyController.Instance.curHP;
        float percent = (float)cur / (float)max;
        hpFill.fillAmount = percent;
        hpText.text = cur.ToString();
        bossNameText.text = EnemyController.Instance.curEnemy.enemyName;
    }

    void Move(GameObject go, Vector3 pos, float time)
    {
        LeanTween.move(go.GetComponent<RectTransform>(), pos, time)
            .setEaseInOutCubic();
    }

    void Move(GameObject go, Vector3 pos, float time, OnFuncFinsh finish)
    {
        LeanTween.move(go.GetComponent<RectTransform>(), pos, time)
            .setEaseInOutCubic()
            .setOnComplete(() => finish?.Invoke());
    }

    void Scale(GameObject go, Vector2 scale, float time)
    {
        LeanTween.scale(go, scale, time)
            .setEaseInOutCubic();
    }

    void Scale(GameObject go, Vector2 scale, float time, OnFuncFinsh finish)
    {
        LeanTween.scale(go, scale, time)
            .setEaseInOutCubic()
            .setOnComplete(() => finish());
    }

    void Show(GameObject go)
    {
        CloseMenuAll();
        go.SetActive(true);
        Move(axeBorder, new Vector3(0, 0, 0), 0.5f);
        Move(teamBorder, new Vector3(0, 0, 0), 0.5f);
        Move(achievementBorder, new Vector3(0, 0, 0), 0.5f);
    }

    void CloseMenu()
    {
        Move(axeBorder, new Vector3(0, -450, 0), 0.5f,
            () => axeBorder.SetActive(false));
        Move(teamBorder, new Vector3(0, -450, 0), 0.5f,
            () => teamBorder.SetActive(false));
        Move(achievementBorder, new Vector3(0, -450, 0), 0.5f,
            () => achievementBorder.SetActive(false));
    }

    void CloseMenuAll()
    {
        axeBorder.SetActive(false);
        teamBorder.SetActive(false);
        achievementBorder.SetActive(false);
    }

    void OpenSetting()
    {
        setting.SetActive(true);
        Scale(settingBorder, Vector3.one, 0.5f);
    }

    void CloseSetting()
    {
        Scale(settingBorder, Vector3.zero, 0.5f,
            () => setting.SetActive(false));
    }

}
