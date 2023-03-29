using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    // Create a class for the save data and mark it as serializable
    [System.Serializable]
    class SaveData
    {
        public string playerName;
    }

    public void SaveGameData()
    {
        SaveData data = new SaveData();
        data.playerName = "Hey";

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void OnSaveButtonClicked()
    {
        SaveGameData();
    }
}
