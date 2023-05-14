using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RaceManager : MonoBehaviour
{
    public GameObject RaceTypeSelectMenu;
    public GameObject ButtonToSelectRace;
    public GameObject Player;
    public bool IsButtonReceiverActive;
    public string RaceNameSelected;
    public TextMeshProUGUI SelectButtonText;
    public Quaternion rotationA;
    public Progress progressScript;
    public int lap;
    public string Type;
    public List<int> checkpointsUsed;
    public int TotalLaps;
    public bool isRaceActive;
    public int TotalCheckpoints;
    public int Timer;
    public GameManager gameManagerScript;
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;
    public TextMeshProUGUI LapText;

    // Start is called before the first frame update
    void Start()
    {
        progressScript = GameObject.Find("GameManager").GetComponent<Progress>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsButtonReceiverActive && Input.GetKeyDown(KeyCode.E))
        {
            PlayerPosition = Player.transform.position;
            PlayerRotation = Player.transform.rotation;
            BeginRaceButtonPressed();
        }
    }

    public void BeginRace(string raceType)
    {
        if (raceType == "regular")
        {
            Type = "Race";
            if (RaceNameSelected == "Stadium")
            {
                rotationA = Quaternion.Euler(0, 90, 0);
                Player.transform.SetPositionAndRotation(new Vector3(75, 0.5f, -360), rotationA);
                ExitRaceTypeSelectMenu();
            }
        }
        else if (raceType == "timeTrial")
        {
            Type = "Time Trial";
            if (RaceNameSelected == "Stadium")
            {
                rotationA = Quaternion.Euler(0, 90, 0);
                Player.transform.SetPositionAndRotation(new Vector3(75, 0.5f, -360), rotationA);
                ExitRaceTypeSelectMenu();
            }
        } else {
#if UNITY_EDITOR
            Debug.Log("Error R1: Race Type Doesn't Exist");
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
        InitiateCheckpointList();
        isRaceActive = true;
        lap = 1;
        LapText.text = $"Lap: {lap}/{TotalLaps}";
        StartCoroutine(TimerTick());
    }
    public void BeginRaceButtonPressed()
    {
        RaceTypeSelectMenu.SetActive(true);
        Time.timeScale = 0.001f;
    }

    public void ExitRaceTypeSelectMenu()
    {
        RaceTypeSelectMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ActivateButtonReceiver()
    {
        ButtonToSelectRace.SetActive(true);
        IsButtonReceiverActive = true;
    }

    public void DeactivateButtonReceiver()
    {
        ButtonToSelectRace.SetActive(false);
        IsButtonReceiverActive = false;
    }

    public void SetRaceName()
    {
        SelectButtonText.text = "Press E to begin the " + RaceNameSelected + " race.";
        ActivateButtonReceiver();
    }

    public void OnTimeTrialFinish()
    {
        progressScript.racesCompleteCount++;
        if (!progressScript.racesCompleteNames.Contains($"{RaceNameSelected}timeTrial"))
        {
            progressScript.racesCompleteNames.Add($"{RaceNameSelected}timeTrial");
            progressScript.uniqueEventsFinishedCount++;
        }
        isRaceActive = false;
        ShowRaceResults();
    }

    public void OnRaceFinished()
    {
        progressScript.racesCompleteCount++;
        if (!progressScript.racesCompleteNames.Contains($"{RaceNameSelected}regular"))
        {
            progressScript.racesCompleteNames.Add($"{RaceNameSelected}regular");
            progressScript.uniqueEventsFinishedCount++;
        }
        isRaceActive = false;
        ShowRaceResults();
    }

    public void ClearAndCheck()
    {
        checkpointsUsed.Clear();
        checkpointsUsed.Add(0);
        
        if (lap == TotalLaps && isRaceActive)
        {
            if (Type == "Race")
            {
                OnRaceFinished();
            }
            else if (Type == "Time Trial")
            {
                OnTimeTrialFinish();
            }
            else
            {
                Debug.Log("Uh, something's wrong here. The race type does not exist! If you are a player, you can exit the application using Alt+F4 or Command+G.");
            }
        }
    }

    public void InitiateCheckpointList()
    {
        checkpointsUsed.Clear();
        checkpointsUsed.Add(0);
    }

    IEnumerator TimerTick()
    {
        yield return new WaitForSeconds(1);
        Timer++;
        StartCoroutine(TimerTick());
    }

    public void ShowRaceResults()
    {
        StopCoroutine(TimerTick());
        Time.timeScale = 0.001f;
        gameManagerScript.ResultPanel.SetActive(true);
        if (progressScript.uniqueEventsFinishedCount == progressScript.totalUniqueEvents)
        {
            gameManagerScript.resultsText.fontSize = 26;
            gameManagerScript.resultsText.text = $"CONGRATULATIONS! The {RaceNameSelected} {Type} was the only race you needed to do to get 100% completion! You took {Timer} seconds \n Thanks for playing!";
        }
        else
        {
            gameManagerScript.resultsText.text = $"Congratulations! You finished the {RaceNameSelected} {Type} in: {Timer} seconds!";
        }
    }
}