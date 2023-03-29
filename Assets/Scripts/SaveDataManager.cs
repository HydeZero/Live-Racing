using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SaveDataManager : MonoBehaviour
{
    public string PlayerName;
    public TextMeshProUGUI inputField;

    // Create a class for the save data and mark it as serializable
    [System.Serializable]
    class SaveData
    {
        public string playerName;
    }

    public void SaveGameData()
    {
        PlayerName = inputField.text;
        SaveData data = new SaveData();
        data.playerName = PlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void OnSaveButtonClicked()
    {
        SaveGameData();
    }
}
