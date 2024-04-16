using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArchievementUI : Singleton<ArchievementUI>
{
    [SerializeField] GameObject alertIcon;
    [Header("===== Tap Archievement =====")]
    [SerializeField] TextMeshProUGUI tapCountText;
    [SerializeField] TextMeshProUGUI tapRewardText;
    [SerializeField] Image tapCountFill;
    [SerializeField] Button tapRewardButton;
    [Header("===== Kill Enemy Archievement =====")]
    [SerializeField] TextMeshProUGUI killEnemyCountText;
    [SerializeField] TextMeshProUGUI killEnemyRewardText;
    [SerializeField] Image killEnemyCountFill;
    [SerializeField] Button killEnemyRewardButton;
    [Header("===== Enter Phase Archievement =====")]
    [SerializeField] TextMeshProUGUI enterPhaseCountText;
    [SerializeField] TextMeshProUGUI enterPhaseRewardText;
    [SerializeField] Image enterPhaseCountFill;
    [SerializeField] Button enterPhaseRewardButton;
    [Header("===== Collect Gold Archievement =====")]
    [SerializeField] TextMeshProUGUI collectGoldCountText;
    [SerializeField] TextMeshProUGUI collectGoldRewardText;
    [SerializeField] Image collectGoldCountFill;
    [SerializeField] Button collectGoldRewardButton;
    [Header("===== Tap Bird Archievement =====")]
    [SerializeField] TextMeshProUGUI tapBirdCountText;
    [SerializeField] TextMeshProUGUI tapBirdRewardText;
    [SerializeField] Image tapBirdCountFill;
    [SerializeField] Button tapBirdRewardButton;
    [Header("===== Use Strike Archievement =====")]
    [SerializeField] TextMeshProUGUI useStrikeCountText;
    [SerializeField] TextMeshProUGUI useStrikeRewardText;
    [SerializeField] Image useStrikeCountFill;
    [SerializeField] Button useStrikeRewardButton;
    [Header("===== Use Looting Archievement =====")]
    [SerializeField] TextMeshProUGUI useLootingCountText;
    [SerializeField] TextMeshProUGUI useLootingRewardText;
    [SerializeField] Image useLootingCountFill;
    [SerializeField] Button useLootingRewardButton;
    [Header("===== Use Teamwork Archievement =====")]
    [SerializeField] TextMeshProUGUI useTeamworkCountText;
    [SerializeField] TextMeshProUGUI useTeamworkRewardText;
    [SerializeField] Image useTeamworkCountFill;
    [SerializeField] Button useTeamworkRewardButton;

    private void OnEnable()
    {
        UIManager.Instance.OnShowArchievement += UpdateTap;
        UIManager.Instance.OnShowArchievement += UpdateKillEnemy;
        UIManager.Instance.OnShowArchievement += UpdateEnterPhase;
        UIManager.Instance.OnShowArchievement += UpdateCollectCoin;
        UIManager.Instance.OnShowArchievement += UpdateTapBird;
        UIManager.Instance.OnShowArchievement += UpdateStrike;
        UIManager.Instance.OnShowArchievement += UpdateLooting;
        UIManager.Instance.OnShowArchievement += UpdateTeamwork;

    }

    private void Awake()
    {
        int tapTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curTapArchievementLv, ArchievementManager.Instance.tapMulPerLevel);
        tapRewardButton.onClick.AddListener(() => ArchievementManager.Instance.RewardBut(GameManager.curTapCount, tapTarget, OnGetTapArchievement));

        int killTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curKillEnemyArchievementLv, ArchievementManager.Instance.killEnemyMulPerLevel);
        killEnemyRewardButton.onClick.AddListener(() => ArchievementManager.Instance.RewardBut(GameManager.curKillEnemyCount, killTarget, OnGetKillEnemyArchievement));

        int phaseTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curEnterPhaseArchievementLv, ArchievementManager.Instance.enterPhaseMulPerLevel);
        enterPhaseRewardButton.onClick.AddListener(() => ArchievementManager.Instance.RewardBut(GameManager.curEnterPhaseCount, phaseTarget, OnGetEnterPhaseArchievement));

        int goldTarget = ArchievementManager.Instance.GetArchievementTargetForCoint(GameManager.curCollectGoldArchievementLv, ArchievementManager.Instance.collectGoldMulPerLevel);
        collectGoldRewardButton.onClick.AddListener(() => ArchievementManager.Instance.RewardBut(GameManager.curCollectGoldCount, goldTarget, OnGetCollectGoldArchievement));

        int tapBirdTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curTapBirdArchievementLv, ArchievementManager.Instance.tapBirdMulPerLevel);
        tapBirdRewardButton.onClick.AddListener(() => ArchievementManager.Instance.RewardBut(GameManager.curTapBirdCount, tapBirdTarget, OnGetTapBirdArchievement));

        int useStrikeTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curStrikeArchievementLv, ArchievementManager.Instance.useStrikeMulPerLevel);
        useStrikeRewardButton.onClick.AddListener(() => ArchievementManager.Instance.RewardBut(GameManager.curStrikeCount, useStrikeTarget, OnGetUseStrikeArchievement));

        int useLootingTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curLootingArchievementLv, ArchievementManager.Instance.useLootingMulPerLevel);
        useLootingRewardButton.onClick.AddListener(() => ArchievementManager.Instance.RewardBut(GameManager.curLootingCount, useLootingTarget, OnGetUseLootingArchievement));

        int useTeamworkTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curTeamworkArchievementLv, ArchievementManager.Instance.useTeamworkMulPerLevel);
        useTeamworkRewardButton.onClick.AddListener(() => ArchievementManager.Instance.RewardBut(GameManager.curTeamworkCount, useTeamworkTarget, OnGetUseTeamworkArchievement));
    }

    private void Update()
    {
        DisableAlert();

        int tapTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curTapArchievementLv, ArchievementManager.Instance.tapMulPerLevel);
        DisableArchievementButton(tapRewardButton, GameManager.curTapCount, tapTarget);

        int killTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curKillEnemyArchievementLv, ArchievementManager.Instance.killEnemyMulPerLevel);
        DisableArchievementButton(killEnemyRewardButton, GameManager.curKillEnemyCount, killTarget);

        int phaseTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curEnterPhaseArchievementLv, ArchievementManager.Instance.enterPhaseMulPerLevel);
        DisableArchievementButton(enterPhaseRewardButton, GameManager.curEnterPhaseCount, phaseTarget);

        int goldTarget = ArchievementManager.Instance.GetArchievementTargetForCoint(GameManager.curCollectGoldArchievementLv, ArchievementManager.Instance.collectGoldMulPerLevel);
        DisableArchievementButton(collectGoldRewardButton, GameManager.curCollectGoldCount, goldTarget);

        int tapBirdTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curTapBirdArchievementLv, ArchievementManager.Instance.tapBirdMulPerLevel);
        DisableArchievementButton(tapBirdRewardButton, GameManager.curTapBirdCount, tapBirdTarget);

        int useStrikeTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curStrikeArchievementLv, ArchievementManager.Instance.useStrikeMulPerLevel);
        DisableArchievementButton(useStrikeRewardButton, GameManager.curStrikeCount, useStrikeTarget);

        int useLootingTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curLootingArchievementLv, ArchievementManager.Instance.useLootingMulPerLevel);
        DisableArchievementButton(useLootingRewardButton, GameManager.curLootingCount, useLootingTarget);

        int useTeamworkTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curTeamworkArchievementLv, ArchievementManager.Instance.useTeamworkMulPerLevel);
        DisableArchievementButton(useTeamworkRewardButton, GameManager.curTeamworkCount, useTeamworkTarget);

    }

    #region Archievement 

    public void UpdateArchievementInfo(int curLevel, int mulTarget, TextMeshProUGUI countText, int count,
        TextMeshProUGUI rewardText, int reward, Image fill)
    {
        int target;

        if (countText == collectGoldCountText && rewardText == collectGoldRewardText && fill == collectGoldCountFill)
            target = ArchievementManager.Instance.GetArchievementTargetForCoint(curLevel, mulTarget);
        else
            target = ArchievementManager.Instance.GetArchievementTarget(curLevel, mulTarget);

        countText.text = $"{count} / {target}";
        rewardText.text = $"+ {reward}";
        float percent = (float)count / (float)target;
        fill.fillAmount = percent;
    }

    void DisableArchievementButton(Button but, int count, int target)
    {
        if (ArchievementManager.Instance.CanTakeReward(count, target))
            but.interactable = true;
        else
            but.interactable = false;
    }


    #endregion

    public void UpdateTap()
    {
        int mul = ArchievementManager.Instance.tapMulPerLevel;
        int reward = ArchievementManager.Instance.tapReward;
        UpdateArchievementInfo(GameManager.curTapArchievementLv, mul,
            tapCountText, GameManager.curTapCount, tapRewardText, reward, tapCountFill);
    }

    void OnGetTapArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.tapReward);
        GameManager.curTapArchievementLv++;
        SaveSystem.Save();
        UpdateTap();

    }

    public void UpdateKillEnemy()
    {
        int mul = ArchievementManager.Instance.killEnemyMulPerLevel;
        int reward = ArchievementManager.Instance.killEnemyReward;
        UpdateArchievementInfo(GameManager.curKillEnemyArchievementLv, mul,
        killEnemyCountText, GameManager.curKillEnemyCount, killEnemyRewardText, reward, killEnemyCountFill);
    }

    void OnGetKillEnemyArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.killEnemyReward);
        GameManager.curKillEnemyArchievementLv++;
        SaveSystem.Save();
        UpdateKillEnemy();

    }

    public void UpdateEnterPhase()
    {
        int mul = ArchievementManager.Instance.enterPhaseMulPerLevel;
        int reward = ArchievementManager.Instance.enterPhaseReward;
        UpdateArchievementInfo(GameManager.curEnterPhaseArchievementLv, mul,
           enterPhaseCountText, GameManager.curEnterPhaseCount, enterPhaseRewardText, reward, enterPhaseCountFill);
    }

    void OnGetEnterPhaseArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.enterPhaseReward);
        GameManager.curEnterPhaseArchievementLv++;
        SaveSystem.Save();
        UpdateEnterPhase();

    }

    public void UpdateCollectCoin()
    {
        int mul = ArchievementManager.Instance.collectGoldMulPerLevel;
        int reward = ArchievementManager.Instance.collectGoldReward;
        UpdateArchievementInfo(GameManager.curCollectGoldArchievementLv, mul,
           collectGoldCountText, GameManager.curCollectGoldCount, collectGoldRewardText, reward, collectGoldCountFill);

    }

    void OnGetCollectGoldArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.collectGoldReward);
        GameManager.curCollectGoldArchievementLv++;
        SaveSystem.Save();
        UpdateCollectCoin();

    }

    public void UpdateTapBird()
    {
        int mul = ArchievementManager.Instance.tapBirdMulPerLevel;
        int reward = ArchievementManager.Instance.tapBirdReward;
        UpdateArchievementInfo(GameManager.curTapBirdArchievementLv, mul,
          tapBirdCountText, GameManager.curTapBirdCount, tapBirdRewardText, reward, tapBirdCountFill);
    }

    void OnGetTapBirdArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.tapBirdReward);
        GameManager.curTapBirdArchievementLv++;
        SaveSystem.Save();
        UpdateTapBird();

    }

    public void UpdateStrike()
    {
        int mul = ArchievementManager.Instance.useStrikeMulPerLevel;
        int reward = ArchievementManager.Instance.useStrikeReward;
        UpdateArchievementInfo(GameManager.curStrikeArchievementLv, mul,
          useStrikeCountText, GameManager.curStrikeCount, useStrikeRewardText, reward, useStrikeCountFill);

    }

    void OnGetUseStrikeArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.useStrikeReward);
        GameManager.curStrikeArchievementLv++;
        SaveSystem.Save();
        UpdateStrike();

    }

    public void UpdateLooting()
    {
        int mul = ArchievementManager.Instance.useLootingMulPerLevel;
        int reward = ArchievementManager.Instance.useLootingReward;
        UpdateArchievementInfo(GameManager.curLootingArchievementLv, mul,
           useLootingCountText, GameManager.curLootingCount, useLootingRewardText, reward, useLootingCountFill);

    }

    void OnGetUseLootingArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.useLootingReward);
        GameManager.curLootingArchievementLv++;
        SaveSystem.Save();
        UpdateLooting();

    }

    public void UpdateTeamwork()
    {
        int mul = ArchievementManager.Instance.useTeamworkMulPerLevel;
        int reward = ArchievementManager.Instance.useTeamworkReward;
        UpdateArchievementInfo(GameManager.curTeamworkArchievementLv, mul,
           useTeamworkCountText, GameManager.curTeamworkCount, useTeamworkRewardText, reward, useTeamworkCountFill);

    }

    void OnGetUseTeamworkArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.useTeamworkReward);
        GameManager.curTeamworkArchievementLv++;
        SaveSystem.Save();
        UpdateTeamwork();

    }

    void DisableAlert()
    {
        int tapTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curTapArchievementLv, ArchievementManager.Instance.tapMulPerLevel);
        int killTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curKillEnemyArchievementLv, ArchievementManager.Instance.killEnemyMulPerLevel);
        int phaseTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curEnterPhaseArchievementLv, ArchievementManager.Instance.enterPhaseMulPerLevel);
        int goldTarget = ArchievementManager.Instance.GetArchievementTargetForCoint(GameManager.curCollectGoldArchievementLv, ArchievementManager.Instance.collectGoldMulPerLevel);
        int tapBirdTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curTapBirdArchievementLv, ArchievementManager.Instance.tapBirdMulPerLevel);
        int useStrikeTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curStrikeArchievementLv, ArchievementManager.Instance.useStrikeMulPerLevel);
        int useLootingTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curLootingArchievementLv, ArchievementManager.Instance.useLootingMulPerLevel);
        int useTeamworkTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curTeamworkArchievementLv, ArchievementManager.Instance.useTeamworkMulPerLevel);

        if (ArchievementManager.Instance.CanTakeReward(GameManager.curTapCount, tapTarget) ||
        ArchievementManager.Instance.CanTakeReward(GameManager.curKillEnemyCount, killTarget) ||
        ArchievementManager.Instance.CanTakeReward(GameManager.curEnterPhaseCount, phaseTarget) ||
        ArchievementManager.Instance.CanTakeReward(GameManager.curCollectGoldCount, goldTarget) ||
        ArchievementManager.Instance.CanTakeReward(GameManager.curTapBirdCount, tapBirdTarget) ||
        ArchievementManager.Instance.CanTakeReward(GameManager.curStrikeCount, useStrikeTarget) ||
        ArchievementManager.Instance.CanTakeReward(GameManager.curLootingCount, useLootingTarget) ||
        ArchievementManager.Instance.CanTakeReward(GameManager.curTeamworkCount, useTeamworkTarget))
        {
            alertIcon.SetActive(true);
        }
        else
            alertIcon.SetActive(false);
    }

}
