using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SaveDataManager : MonoBehaviour
{
    public string PlayerName = "sample";
    public MissionManager MissionManagerScript;
    //public TMP_InputField inputField;



    // Create a class for the save data and mark it as serializable
    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public List<string> CurrentMissionNames;
        public List<string> CompletedMissionNames;
        public List<string> UpcomingMissionNames;
        public List<string> MissionRequirements;
    }

    public void SaveGameData()
    {
        //PlayerName = inputField.text;
        SaveData data = new SaveData();
        data.CurrentMissionNames = MissionManagerScript.CurrentMissionNames2;
        data.CompletedMissionNames = MissionManagerScript.CompletedMissionNames2;
        data.UpcomingMissionNames = MissionManagerScript.UpcomingMissionNames2;
        data.MissionRequirements = MissionManagerScript.MissionRequirements2;
        data.playerName = PlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerName = data.playerName;
            //inputField.text = PlayerName;
        } else
        {
            //inputField.text = "No Savefile Found";
        }
    }
}
