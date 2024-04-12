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
    }

}
