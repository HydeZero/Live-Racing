using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SaveDataManager : MonoBehaviour
{
    public string PlayerName;
    public GameObject AutoSaveIndicator;
    public Progress progressScript;

    //public TMP_InputField inputField;

    void Start()
    {
        progressScript = GameObject.Find("GameManager").GetComponent<Progress>();
        LoadGameData();
        StartCoroutine(AutoSave());
    }

    // Create a class for the save data and mark it as serializable
    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int RacesCompleted;
        public int UniqueEventsCompleted;
    }

    public void SaveGameData()
    {
        //PlayerName = inputField.text;
        SaveData data = new SaveData();
        data.playerName = PlayerName;
        data.RacesCompleted = progressScript.racesCompleteCount;
        data.UniqueEventsCompleted = progressScript.uniqueEventsFinishedCount;

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
            progressScript.racesCompleteCount = data.RacesCompleted;
            progressScript.uniqueEventsFinishedCount = data.UniqueEventsCompleted;
            //inputField.text = PlayerName;
        } else
        {
            Debug.Log("No Save File Found! It might have been moved or deleted.");
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
