using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArchievementUI : Singleton<ArchievementUI>
{
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
        UIManager.Instance.OnShowArchievement += () => UpdateTap();
        UIManager.Instance.OnShowArchievement += () => UpdateKillEnemy();
        UIManager.Instance.OnShowArchievement += () => UpdateEnterPhase();
        UIManager.Instance.OnShowArchievement += () => UpdateCollectCoin();
        UIManager.Instance.OnShowArchievement += () => UpdateTapBird();
        UIManager.Instance.OnShowArchievement += () => UpdateStrike();
        UIManager.Instance.OnShowArchievement += () => UpdateLooting();
        UIManager.Instance.OnShowArchievement += () => UpdateTeamwork();

    }

    private void Awake()
    {
        int tapTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curTapArchievementLv, ArchievementManager.Instance.tapMulPerLevel);
        tapRewardButton.onClick.AddListener(() => ArchievementManager.Instance.RewardBut(GameManager.curTapCount, tapTarget, OnGetTapArchievement));

        int killTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curKillEnemyArchievementLv, ArchievementManager.Instance.killEnemyMulPerLevel);
        killEnemyRewardButton.onClick.AddListener(() => ArchievementManager.Instance.RewardBut(GameManager.curKillEnemyCount, killTarget, OnGetKillEnemyArchievement));

        int phaseTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curEnterPhaseArchievementLv, ArchievementManager.Instance.enterPhaseMulPerLevel);
        enterPhaseRewardButton.onClick.AddListener(() => ArchievementManager.Instance.RewardBut(GameManager.curEnterPhaseCount, phaseTarget, OnGetEnterPhaseArchievement));

        int goldTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curCollectGoldArchievementLv, ArchievementManager.Instance.collectGoldMulPerLevel);
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
        int tapTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curTapArchievementLv, ArchievementManager.Instance.tapMulPerLevel);
        DisableArchievementButton(tapRewardButton, GameManager.curTapCount, tapTarget);

        int killTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curKillEnemyArchievementLv, ArchievementManager.Instance.killEnemyMulPerLevel);
        DisableArchievementButton(killEnemyRewardButton, GameManager.curKillEnemyCount, killTarget);

        int phaseTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curEnterPhaseArchievementLv, ArchievementManager.Instance.enterPhaseMulPerLevel);
        DisableArchievementButton(enterPhaseRewardButton, GameManager.curEnterPhaseCount, phaseTarget);

        int goldTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curCollectGoldArchievementLv, ArchievementManager.Instance.collectGoldMulPerLevel);
        DisableArchievementButton(collectGoldRewardButton, GameManager.curCollectGoldCount, goldTarget);

        int tapBirdTarget = ArchievementManager.Instance.GetArchievementTarget(GameManager.curTapBirdArchievementLv, ArchievementManager.Instance.tapBirdMulPerLevel);
        DisableArchievementButton(tapBirdRewardButton, GameManager.curTapCount, tapBirdTarget);

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
        int target = ArchievementManager.Instance.GetArchievementTarget(curLevel, mulTarget);
        countText.text = $"{count} / {target}";
        rewardText.text = $"+ {reward}";
        float percent = count / target;
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
        UpdateArchievementInfo(GameManager.curTapArchievementLv, ArchievementManager.Instance.tapMulPerLevel,
            tapCountText, GameManager.curTapCount, tapRewardText, ArchievementManager.Instance.tapReward, tapCountFill);
    }

    void OnGetTapArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.tapReward);
        GameManager.curTapArchievementLv++;
        UpdateTap();
        SaveSystem.Save();

    }

    public void UpdateKillEnemy()
    {
        UpdateArchievementInfo(GameManager.curKillEnemyArchievementLv, ArchievementManager.Instance.killEnemyMulPerLevel,
    killEnemyCountText, GameManager.curKillEnemyCount, killEnemyRewardText, ArchievementManager.Instance.killEnemyReward, killEnemyCountFill);
    }

    void OnGetKillEnemyArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.killEnemyReward);
        GameManager.curKillEnemyArchievementLv++;
        UpdateKillEnemy();
        SaveSystem.Save();

    }

    public void UpdateEnterPhase()
    {
        UpdateArchievementInfo(GameManager.curEnterPhaseArchievementLv, ArchievementManager.Instance.enterPhaseMulPerLevel,
           enterPhaseCountText, GameManager.curEnterPhaseCount, enterPhaseRewardText, ArchievementManager.Instance.enterPhaseReward, enterPhaseCountFill);
    }

    void OnGetEnterPhaseArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.enterPhaseReward);
        GameManager.curEnterPhaseArchievementLv++;
        UpdateEnterPhase();
        SaveSystem.Save();

    }

    public void UpdateCollectCoin()
    {
        UpdateArchievementInfo(GameManager.curCollectGoldArchievementLv, ArchievementManager.Instance.collectGoldMulPerLevel,
           collectGoldCountText, GameManager.curCollectGoldCount, collectGoldRewardText, ArchievementManager.Instance.collectGoldReward, collectGoldCountFill);

    }

    void OnGetCollectGoldArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.collectGoldReward);
        GameManager.curCollectGoldArchievementLv++;
        UpdateCollectCoin();
        SaveSystem.Save();

    }

    public void UpdateTapBird()
    {
        UpdateArchievementInfo(GameManager.curTapBirdArchievementLv, ArchievementManager.Instance.tapBirdMulPerLevel,
          tapBirdCountText, GameManager.curTapBirdCount, tapBirdRewardText, ArchievementManager.Instance.tapBirdReward, tapBirdCountFill);
    }

    void OnGetTapBirdArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.tapBirdReward);
        GameManager.curTapBirdArchievementLv++;
        UpdateTapBird();
        SaveSystem.Save();

    }

    public void UpdateStrike()
    {
        UpdateArchievementInfo(GameManager.curStrikeArchievementLv, ArchievementManager.Instance.useStrikeMulPerLevel,
          useStrikeCountText, GameManager.curStrikeCount, useStrikeRewardText, ArchievementManager.Instance.useStrikeReward, useStrikeCountFill);

    }

    void OnGetUseStrikeArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.useStrikeReward);
        GameManager.curStrikeArchievementLv++;
        UpdateStrike();
        SaveSystem.Save();

    }

    public void UpdateLooting()
    {
        UpdateArchievementInfo(GameManager.curLootingArchievementLv, ArchievementManager.Instance.useLootingMulPerLevel,
           useLootingCountText, GameManager.curLootingCount, useLootingRewardText, ArchievementManager.Instance.useLootingReward, useLootingCountFill);

    }

    void OnGetUseLootingArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.useLootingReward);
        GameManager.curLootingArchievementLv++;
        UpdateLooting();
        SaveSystem.Save();

    }

    public void UpdateTeamwork()
    {
        UpdateArchievementInfo(GameManager.curTeamworkArchievementLv, ArchievementManager.Instance.useTeamworkMulPerLevel,
           useTeamworkCountText, GameManager.curTeamworkCount, useTeamworkRewardText, ArchievementManager.Instance.useTeamworkReward, useTeamworkCountFill);

    }

    void OnGetUseTeamworkArchievement()
    {
        CoinGenerator.Instance.SpawnAndSetupCoin(ArchievementManager.Instance.useTeamworkReward);
        GameManager.curTeamworkArchievementLv++;
        UpdateTeamwork();
        SaveSystem.Save();

    }

}
