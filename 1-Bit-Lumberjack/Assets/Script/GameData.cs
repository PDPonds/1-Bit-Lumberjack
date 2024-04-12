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
    }

}
