using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SaveDataManager : MonoBehaviour
{
    public string PlayerName = "sample";
    public MissionManager MissionManagerScript;
    public GameObject AutoSaveIndicator;
    //public TMP_InputField inputField;

    void Start()
    {
        LoadGameData();
        StartCoroutine(AutoSave());
    }

    // Create a class for the save data and mark it as serializable
    [System.Serializable]
    class SaveData
    {
        public string playerName;
    }

    public void SaveGameData()
    {
        //PlayerName = inputField.text;
        SaveData data = new SaveData();
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

    IEnumerator AutoSave()
    {
        yield return new WaitForSeconds(60);
        SaveGameData();
        AutoSaveIndicator.SetActive(true);
        yield return new WaitForSeconds(1);
        AutoSaveIndicator.SetActive(false);
        StartCoroutine(AutoSave());
    }
}
