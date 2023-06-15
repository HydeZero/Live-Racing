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
    public RaceManager raceManagerScript;
    public DealershipCarManager dealershipCarManagerScript;

    //public TMP_InputField inputField;

    void Start()
    {
        progressScript = GameObject.Find("GameManager").GetComponent<Progress>();
        raceManagerScript = GameObject.Find("GameManager").GetComponent<RaceManager>();
        dealershipCarManagerScript = GameObject.Find("CheckForPlayerDealership").GetComponent<DealershipCarManager>();
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
        public List<string> UniqueRaces;
        public List<string> carsPurchased;
    }

    public void SaveGameData()
    {
        //PlayerName = inputField.text;
        SaveData data = new()
        {
            playerName = PlayerName,
            RacesCompleted = progressScript.racesCompleteCount,
            UniqueEventsCompleted = progressScript.uniqueEventsFinishedCount,
            UniqueRaces = progressScript.racesCompleteNames,
            carsPurchased = dealershipCarManagerScript.carsPurchased
        };

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
            progressScript.racesCompleteNames = data.UniqueRaces;
            //inputField.text = PlayerName;
        } else
        {
            Debug.Log("No Save File Found! It might have been moved or deleted.");
            //inputField.text = "No Savefile Found";
        }
    }

    public IEnumerator AutoSave()
    {
        AutoSaveIndicator.SetActive(true);
        SaveGameData();
        yield return new WaitForSeconds(1);
        AutoSaveIndicator.SetActive(false);
        yield return new WaitForSeconds(30);
        StartCoroutine(AutoSave());
    }

    public void ReBeginAutoSave()
    {
        StopCoroutine(AutoSave());
        StartCoroutine(AutoSave());
    }
}
