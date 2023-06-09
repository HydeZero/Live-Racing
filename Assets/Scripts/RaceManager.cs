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
    public GameObject DowntownRaceWalls;
    public GameObject DowntownPlayerDetector;
    public PlayerControllerCareer playerControllerCareerScript;
    public OpponentAI opponentAIScript;
    public DealershipCarManager dealershipCarManagerScript;
    public GameObject notice;
    public GameObject notice2;
    public bool isTestModeActive;
    public SaveDataManager saveDataManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        dealershipCarManagerScript = GameObject.Find("CheckForPlayerDealership").GetComponent<DealershipCarManager>();
        progressScript = GameObject.Find("GameManager").GetComponent<Progress>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerControllerCareerScript = GameObject.Find("PlayerLukeman Transporter 2019").GetComponent<PlayerControllerCareer>();
        opponentAIScript = GameObject.Find("AISteven").GetComponent<OpponentAI>();
        saveDataManagerScript = GameObject.Find("SaveDataManager").GetComponent<SaveDataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsButtonReceiverActive && Input.GetKeyDown(KeyCode.E))
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerPosition = Player.transform.position;
            PlayerRotation = Player.transform.rotation;
            BeginRaceButtonPressed();
            gameManagerScript.cashIndicator.SetActive(false);
            playerControllerCareerScript.speedometer.gameObject.SetActive(false);
        }
    }

    public void ExitNotice()
    {
        if (!Input.GetKey(KeyCode.RightShift))
        {
            notice.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.RightShift))
        {
            notice2.SetActive(true);
        }
    }

    public void ExitNotice2(int exitType)
    {
        if (exitType == 0)
        {
            notice2.SetActive(false);
        } else if (exitType == 1)
        {
            saveDataManagerScript.SaveGameData();
            isTestModeActive = true;
            StopCoroutine(saveDataManagerScript.AutoSave());
            notice2.SetActive(false);
            notice.SetActive(false);
            progressScript.totalUniqueEvents *= 2;
        }
    }
    public void BeginRace(string raceType)
    {
        if (raceType == "regular")
        {
            if (!isTestModeActive)
            {
                notice.SetActive(true);
            } else if (isTestModeActive)
            {
                Type = "Race";
                CheckRaceName(RaceNameSelected);
                InitiateCheckpointList();
                SelectButtonText.gameObject.SetActive(false);
                gameManagerScript.cashIndicator.SetActive(false);
                isRaceActive = true;
                lap = 1;
                LapText.gameObject.SetActive(true);
                LapText.text = $"Lap: {lap}/{TotalLaps}";
                StartCoroutine(TimerTick());
                playerControllerCareerScript.speedometer.gameObject.SetActive(true);
            }
        }
        else if (raceType == "timeTrial")
        {
            Type = "Time Trial";
            CheckRaceName(RaceNameSelected);
            InitiateCheckpointList();
            SelectButtonText.gameObject.SetActive(false);
            gameManagerScript.cashIndicator.SetActive(false);
            isRaceActive = true;
            lap = 1;
            LapText.gameObject.SetActive(true);
            LapText.text = $"Lap: {lap}/{TotalLaps}";
            StartCoroutine(TimerTick());
            playerControllerCareerScript.speedometer.gameObject.SetActive(true);
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Error R1: Race Type Doesn't Exist");
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }

    public void CheckRaceName(string raceName)
    {
        if (raceName == "Stadium")
        {
            rotationA = Quaternion.Euler(0, 90, 0);
            Player.transform.SetPositionAndRotation(new Vector3(75, 0.5f, -360), rotationA);
            ExitRaceTypeSelectMenu();
        }
        if (raceName == "Downtown Race")
        {
            rotationA = Quaternion.Euler(0, 0, 0);
            Player.transform.SetPositionAndRotation(new Vector3(-8.4f, 0.16f, -33.9f), rotationA);
            ExitRaceTypeSelectMenu();
            DowntownRaceWalls.SetActive(true);
            DowntownPlayerDetector.SetActive(false);
        }
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
        gameManagerScript.cashIndicator.SetActive(true);
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
        dealershipCarManagerScript.currentCash += 10000;
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
        dealershipCarManagerScript.currentCash += 20000;
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
            LapText.gameObject.SetActive(false);
        }
        lap++;
        LapText.text = "Lap: " + lap + "/" + TotalLaps;
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
            gameManagerScript.resultsText.text = $"CONGRATULATIONS! The {RaceNameSelected} {Type} was the only race you needed to do to get 100% completion! You took {Timer} seconds to complete this race. \n Thanks for playing!";
        }
        else
        {
            gameManagerScript.resultsText.text = $"Congratulations! You finished the {RaceNameSelected} {Type} in: {Timer} seconds!";
        }
        Timer = 0;
        playerControllerCareerScript.playerRB.velocity = new Vector3(0, 0, 0);
    }
    
    public void ExitRace()
    {
        if (isRaceActive)
        {
            isRaceActive = false;
            LapText.gameObject.SetActive(false);
            Player.transform.SetPositionAndRotation(PlayerPosition, PlayerRotation);
            DowntownRaceWalls.SetActive(false);
            DowntownPlayerDetector.SetActive(true);
        }
        gameManagerScript.ResumeGame();
    }

    public void SendRaceInfoToAI()
    {
        opponentAIScript.RaceName = RaceNameSelected;
    }
}