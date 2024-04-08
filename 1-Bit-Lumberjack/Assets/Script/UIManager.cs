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
    [Header("===== Mana =====")]
    [SerializeField] Image manaFill;
    [SerializeField] TextMeshProUGUI manaText;
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
    [Header("===== Upgrade Axe =====")]
    [SerializeField] TextMeshProUGUI axeLevelText;
    [SerializeField] TextMeshProUGUI axeDamageText;
    [SerializeField] TextMeshProUGUI axeCostText;
    [SerializeField] TextMeshProUGUI axeMulDamageText;
    [SerializeField] Button upgradeAxeButton;
    [Header("===== Upgrade Strike =====")]
    [SerializeField] TextMeshProUGUI strikeLevelText;
    [SerializeField] TextMeshProUGUI strikeDiscriptionText;
    [SerializeField] TextMeshProUGUI strikeCostText;
    [SerializeField] TextMeshProUGUI strikeMulDamageText;
    [SerializeField] Button upgradeStrikeButton;
    [SerializeField] Button strikeSkillButton;
    [SerializeField] TextMeshProUGUI strikeManaCost;
    [SerializeField] Image strikeSkillTimeFill;
    [SerializeField] Image strikeSkillDelayFill;
    [Header("===== Upgrade Looting =====")]
    [SerializeField] TextMeshProUGUI lootingLevelText;
    [SerializeField] TextMeshProUGUI lootingDiscriptionText;
    [SerializeField] TextMeshProUGUI lootingCostText;
    [SerializeField] TextMeshProUGUI lootingMulDamageText;
    [SerializeField] Button upgradeLootingButton;
    [SerializeField] Button lootingSkillButton;
    [SerializeField] TextMeshProUGUI lootingManaCost;
    [SerializeField] Image lootingSkillTimeFill;
    [SerializeField] Image lootingSkillDelayFill;


    private void OnEnable()
    {
        GameManager.Instance.OnAddCoin += UpdateCoin;
        GameManager.Instance.OnRemoveCoin += UpdateCoin;
        GameManager.Instance.OnNextState += UpdateState;
        GameManager.Instance.OnNextPhase += UpdatePhase;
        EnemyController.Instance.OnEnemyTakeDamage += UpdateHP;
        EnemyController.Instance.OnEnemyDead += UpdateHP;
        GameManager.Instance.OnExitBossState += UpdateState;

        GameManager.Instance.OnUpgradeAxe += UpdateDamage;
        GameManager.Instance.OnUpgradeAxe += UpdateAxeUpgrade;

        GameManager.Instance.OnAddMana += UpdateMana;
        GameManager.Instance.OnRemoveMana += UpdateMana;

        #region Strike Skill
        SkillManager.Instance.GetSkill("Strike").OnUpgradeSkill += UpdateStrikeSkill;
        SkillManager.Instance.OnStrikeReady += () => strikeSkillTimeFill.gameObject.SetActive(false);
        SkillManager.Instance.OnStrikeReady += () => strikeSkillDelayFill.gameObject.SetActive(false);

        SkillManager.Instance.OnStrikeUse += () => strikeSkillTimeFill.gameObject.SetActive(true);

        SkillManager.Instance.OnStrikeDelay += () => strikeSkillTimeFill.gameObject.SetActive(false);
        SkillManager.Instance.OnStrikeDelay += () => strikeSkillDelayFill.gameObject.SetActive(true);
        #endregion

        #region Looting Skill
        SkillManager.Instance.GetSkill("Looting").OnUpgradeSkill += UpdateLootingSkill;
        SkillManager.Instance.OnLootingReady += () => lootingSkillTimeFill.gameObject.SetActive(false);
        SkillManager.Instance.OnLootingReady += () => lootingSkillDelayFill.gameObject.SetActive(false);

        SkillManager.Instance.OnLootingUse += () => lootingSkillTimeFill.gameObject.SetActive(true);

        SkillManager.Instance.OnLootingDelay += () => lootingSkillTimeFill.gameObject.SetActive(false);
        SkillManager.Instance.OnLootingDelay += () => lootingSkillDelayFill.gameObject.SetActive(true);
        #endregion


    }

    private void Awake()
    {
        axeButton.onClick.AddListener(ShowUpgradeAxe);
        teamButton.onClick.AddListener(ShowUpgradeTeam);
        achievementButton.onClick.AddListener(ShowGetAchievement);
        foreach (Button b in closeMenus)
        {
            b.onClick.AddListener(CloseMenu);
        }
        openSettingButton.onClick.AddListener(OpenSetting);
        closeSettingButton.onClick.AddListener(CloseSetting);

        upgradeAxeButton.onClick.AddListener(UpgradeAxeBut);

        upgradeStrikeButton.onClick.AddListener(UpgradeStrikeSkill);
        strikeSkillButton.onClick.AddListener(UseStrikeSkill);

        upgradeLootingButton.onClick.AddListener(UpgradeLootingSkill);
        lootingSkillButton.onClick.AddListener(UseLootingSkill);
    }

    private void Start()
    {
        UpdateCoin();
        UpdateState();
        UpdatePhase();
        UpdateHP();
        UpdateDamage();
        UpdateMana();
    }

    private void Update()
    {
        UpdateBossTime();
        DisableUpgradeAxe();
        DisableUpgradeStrikeSkill();
        DisableUpgradeLootingSkill();
    }

    #region Mana

    void UpdateMana()
    {
        float max = GameManager.Instance.maxMana;
        float cur = GameManager.Instance.curMana;
        float percent = cur / max;
        manaFill.fillAmount = percent;
        manaText.text = $"{cur.ToString("N1")} / {max.ToString("N1")}";
    }

    #endregion

    #region Upgrade Axe

    void UpdateAxeUpgrade()
    {
        axeLevelText.text = $"Lv. {GameManager.Instance.curLevelAxe}";
        axeDamageText.text = $"Damage : {GameManager.Instance.CalAxeDamage()}";
        axeCostText.text = $"{GameManager.Instance.CalUpgradeAxeCost()}";
        axeMulDamageText.text = $"Attack +{GameManager.Instance.mulDamagePerLevel}";
    }

    void UpgradeAxeBut()
    {
        GameManager.Instance.AddAxeLevel();
    }

    void DisableUpgradeAxe()
    {
        if (GameManager.Instance.CheckCoin(GameManager.Instance.CalUpgradeAxeCost()))
            upgradeAxeButton.interactable = true;
        else
            upgradeAxeButton.interactable = false;
    }

    #endregion

    #region Upgrade Strike

    void UpdateStrikeSkill()
    {
        Skill skill = SkillManager.Instance.GetSkill("Strike");
        strikeLevelText.text = $"Lv. {SkillManager.Instance.GetSkillLevel(skill)}";
        strikeDiscriptionText.text = $"{SkillManager.Instance.GetSkillDiscription(skill)} {SkillManager.Instance.GetValue(skill).ToString("N2")}%";
        strikeCostText.text = $"{SkillManager.Instance.GetCostToUpgrade(skill)}";
        strikeMulDamageText.text = $"Damage : {SkillManager.Instance.GetMulValue(skill)}";
        strikeManaCost.text = $"{SkillManager.Instance.GetSkillMana(skill)}";
    }

    void UpgradeStrikeSkill()
    {
        Skill skill = SkillManager.Instance.GetSkill("Strike");
        SkillManager.Instance.UpgradeSkill(skill);
    }

    void DisableUpgradeStrikeSkill()
    {
        Skill skill = SkillManager.Instance.GetSkill("Strike");
        if (SkillManager.Instance.GetCanUpgrade(skill))
            upgradeStrikeButton.interactable = true;
        else
            upgradeStrikeButton.interactable = false;

        if (SkillManager.Instance.HasSkill(skill))
        {
            strikeSkillButton.gameObject.SetActive(true);
            switch (SkillManager.Instance.strikeSkillState)
            {
                case SkillState.Ready:
                    strikeSkillDelayFill.fillAmount = 1;
                    strikeSkillTimeFill.fillAmount = 1;

                    if (SkillManager.Instance.GetCanUse(skill))
                        strikeSkillDelayFill.gameObject.SetActive(false);
                    else
                        strikeSkillDelayFill.gameObject.SetActive(true);
                    break;
                case SkillState.Use:
                    float curTime = SkillManager.Instance.curStrikeTime;
                    float maxTime = skill.useTime;
                    float timePercent = curTime / maxTime;
                    strikeSkillTimeFill.fillAmount = timePercent;
                    break;
                case SkillState.Delay:
                    float curDelay = SkillManager.Instance.curStrikeDelay;
                    float maxDelay = skill.delayTime;
                    float delayPercent = curDelay / maxDelay;
                    strikeSkillDelayFill.fillAmount = delayPercent;
                    break;
            }
        }
        else
            strikeSkillButton.gameObject.SetActive(false);

    }

    void UseStrikeSkill()
    {
        SkillManager.Instance.UseStrikeSkill();
    }

    #endregion

    #region Upgrade Looting

    void UpdateLootingSkill()
    {
        Skill skill = SkillManager.Instance.GetSkill("Looting");
        lootingLevelText.text = $"Lv. {SkillManager.Instance.GetSkillLevel(skill)}";
        lootingDiscriptionText.text = $"{SkillManager.Instance.GetSkillDiscription(skill)} {SkillManager.Instance.GetValue(skill).ToString("N2")}%";
        lootingCostText.text = $"{SkillManager.Instance.GetCostToUpgrade(skill)}";
        lootingMulDamageText.text = $"Damage : {SkillManager.Instance.GetMulValue(skill)}";
        lootingManaCost.text = $"{SkillManager.Instance.GetSkillMana(skill)}";
    }

    void UpgradeLootingSkill()
    {
        Skill skill = SkillManager.Instance.GetSkill("Looting");
        SkillManager.Instance.UpgradeSkill(skill);
    }

    void DisableUpgradeLootingSkill()
    {
        Skill skill = SkillManager.Instance.GetSkill("Looting");
        if (SkillManager.Instance.GetCanUpgrade(skill))
            upgradeLootingButton.interactable = true;
        else
            upgradeLootingButton.interactable = false;

        if (SkillManager.Instance.HasSkill(skill))
        {
            lootingSkillButton.gameObject.SetActive(true);
            switch (SkillManager.Instance.lootingSkillState)
            {
                case SkillState.Ready:
                    lootingSkillDelayFill.fillAmount = 1;
                    lootingSkillTimeFill.fillAmount = 1;

                    if (SkillManager.Instance.GetCanUse(skill))
                        lootingSkillDelayFill.gameObject.SetActive(false);
                    else
                        lootingSkillDelayFill.gameObject.SetActive(true);
                    break;
                case SkillState.Use:
                    float curTime = SkillManager.Instance.curLootingTime;
                    float maxTime = skill.useTime;
                    float timePercent = curTime / maxTime;
                    lootingSkillTimeFill.fillAmount = timePercent;
                    break;
                case SkillState.Delay:
                    float curDelay = SkillManager.Instance.curLootingDelay;
                    float maxDelay = skill.delayTime;
                    float delayPercent = curDelay / maxDelay;
                    lootingSkillDelayFill.fillAmount = delayPercent;
                    break;
            }
        }
        else
            lootingSkillButton.gameObject.SetActive(false);

    }

    void UseLootingSkill()
    {
        SkillManager.Instance.UseLootingSkill();
    }

    #endregion

    #region Boss
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
    #endregion

    #region UpdateHub
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

    #endregion

    #region Function
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
    #endregion

    #region Button
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

    void ShowUpgradeAxe()
    {
        Show(axeBorder);
        UpdateAxeUpgrade();
        UpdateStrikeSkill();
    }

    void ShowUpgradeTeam()
    {
        Show(teamBorder);
    }

    void ShowGetAchievement()
    {
        Show(achievementBorder);
    }

    #endregion


}
