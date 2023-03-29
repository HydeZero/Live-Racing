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
        // sets the serializable save data
        SaveData data = new SaveData();
        data.playerName = "This is a savefile test.";
        // converts the variables into JSON format.
        string json = JsonUtility.ToJson(data);
        // Writes the json data to a file
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void OnSaveButtonClicked()
    {
        SaveGameData();
    }
}
