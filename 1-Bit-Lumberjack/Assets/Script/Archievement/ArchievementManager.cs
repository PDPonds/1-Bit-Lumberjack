using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchievementManager : Singleton<ArchievementManager>
{
    public delegate void GetRewardEvent();

    [Header("===== Tap Archievement =====")]
    public int tapMulPerLevel;
    public int tapReward;

    [Header("===== Kill Enemy Archievement =====")]
    public int killEnemyMulPerLevel;
    public int killEnemyReward;

    [Header("===== Enter Phase Archievement =====")]
    public int enterPhaseMulPerLevel;
    public int enterPhaseReward;

    [Header("===== CollectGold Archievement =====")]
    public int collectGoldMulPerLevel;
    public int collectGoldReward;

    [Header("===== Tap Bird Archievement =====")]
    public int tapBirdMulPerLevel;
    public int tapBirdReward;

    [Header("===== Use Strike Archievement =====")]
    public int useStrikeMulPerLevel;
    public int useStrikeReward;

    [Header("===== Use Looting Archievement =====")]
    public int useLootingMulPerLevel;
    public int useLootingReward;

    [Header("===== Use Teamwork Archievement =====")]
    public int useTeamworkMulPerLevel;
    public int useTeamworkReward;


    public int GetArchievementTarget(int curLevel, int mulPerLv)
    {
        return curLevel * mulPerLv;
    }

    public int GetArchievementTargetForCoint(int curLevel, int mulPerLv)
    {
        return (curLevel * curLevel) * mulPerLv;
    }

    public bool CanTakeReward(int count, int target)
    {
        return count >= target;
    }

    public void RewardBut(int count, int target, GetRewardEvent onGetReward)
    {
        if (CanTakeReward(count, target))
        {
            onGetReward?.Invoke();
        }
    }

}
