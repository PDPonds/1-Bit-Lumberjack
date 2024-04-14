using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData gameData = new GameData();
        formatter.Serialize(stream, gameData);

        stream.Close();

    }

    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + "/save.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData gameData = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return gameData;
        }
        else
        {
            return new GameData();
        }
    }

    public static void Save()
    {
        SaveData();
    }

    public static void Load()
    {
        GameData data = LoadData();
        GameManager.curCoin = data.curCoin;
        GameManager.curMana = data.curMana;
        GameManager.curPhase = data.curPhase;
        GameManager.curState = data.curState;
        GameManager.curLevelAxe = data.curLevelAxe;
        GameManager.curStrikeLevel = data.curStrikeLevel;
        GameManager.curLootingLevel = data.curLootingLevel;
        GameManager.curTeamworkLevel = data.curTeamworkLevel;
        GameManager.curLumberjackLevel = data.curLumberjackLevel;
        GameManager.curWoodpeckerLevel = data.curWoodpeckerLevel;
        GameManager.curTapCount = data.curTapCount;
        GameManager.curKillEnemyCount = data.curKillEnemyCount;
        GameManager.curCollectGoldCount = data.curCollectGoldCount;
        GameManager.curEnterPhaseCount = data.curEnterPhaseCount;
        GameManager.curTapBirdCount = data.curTapBirdCount;
        GameManager.curStrikeCount = data.curStrikeCount;
        GameManager.curLootingCount = data.curLootingCount;
        GameManager.curTeamworkCount = data.curTeamworkCount;

        GameManager.curTapArchievementLv = data.curTapArchievementLv;
        GameManager.curKillEnemyArchievementLv = data.curKillEnemyArchievementLv;
        GameManager.curEnterPhaseArchievementLv = data.curEnterPhaseArchievementLv;
        GameManager.curCollectGoldArchievementLv = data.curCollectGoldArchievementLv;
        GameManager.curTapBirdArchievementLv = data.curTapBirdArchievementLv;
        GameManager.curStrikeArchievementLv = data.curStrikeArchievementLv;
        GameManager.curLootingArchievementLv = data.curLootingArchievementLv;
        GameManager.curTeamworkArchievementLv = data.curTeamworkArchievementLv;


    }

}
