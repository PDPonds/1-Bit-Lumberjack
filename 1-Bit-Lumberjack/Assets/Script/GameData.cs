using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int curCoin;
    public float curMana;
    public int curPhase;
    public int curState;
    public int curLevelAxe;
    public int curStrikeLevel;
    public int curLootingLevel;
    public int curTeamworkLevel;
    public int curLumberjackLevel;
    public int curWoodpeckerLevel;
    public int curTapCount;
    public int curKillEnemyCount;
    public int curEnterPhaseCount;
    public int curCollectGoldCount;
    public int curTapBirdCount;
    public int curStrikeCount;
    public int curLootingCount;
    public int curTeamworkCount;
    public int curTapArchievementLv;
    public int curKillEnemyArchievementLv;
    public int curEnterPhaseArchievementLv;
    public int curCollectGoldArchievementLv;
    public int curTapBirdArchievementLv;
    public int curStrikeArchievementLv;
    public int curLootingArchievementLv;
    public int curTeamworkArchievementLv;

    public GameData()
    {
        curCoin = GameManager.curCoin;
        curMana = GameManager.curMana;
        curPhase = GameManager.curPhase;
        curState = GameManager.curState;
        curLevelAxe = GameManager.curLevelAxe;
        curStrikeLevel = GameManager.curStrikeLevel;
        curLootingLevel = GameManager.curLootingLevel;
        curTeamworkLevel = GameManager.curTeamworkLevel;
        curLumberjackLevel = GameManager.curLumberjackLevel;
        curWoodpeckerLevel = GameManager.curWoodpeckerLevel;
        curTapCount = GameManager.curTapCount;
        curKillEnemyCount = GameManager.curKillEnemyCount;
        curEnterPhaseCount = GameManager.curEnterPhaseCount;
        curCollectGoldCount = GameManager.curCollectGoldCount;
        curTapBirdCount = GameManager.curTapBirdCount;
        curStrikeCount = GameManager.curStrikeCount;
        curLootingCount = GameManager.curLootingCount;
        curTeamworkCount = GameManager.curTeamworkCount;

        curTapArchievementLv = GameManager.curTapArchievementLv;
        curKillEnemyArchievementLv = GameManager.curKillEnemyArchievementLv;
        curEnterPhaseArchievementLv = GameManager.curEnterPhaseArchievementLv;
        curCollectGoldArchievementLv = GameManager.curCollectGoldArchievementLv;
        curTapBirdArchievementLv = GameManager.curTapBirdArchievementLv;
        curStrikeArchievementLv = GameManager.curStrikeArchievementLv;
        curLootingArchievementLv = GameManager.curLootingArchievementLv;
        curTeamworkArchievementLv = GameManager.curTeamworkArchievementLv;
    }

}
