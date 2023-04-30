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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsButtonReceiverActive && Input.GetKeyDown(KeyCode.E))
        {
            BeginRaceButtonPressed();
        }
    }

    public void BeginRace(string raceType)
    {
        if (raceType == "regular")
        {
            if (RaceNameSelected == "Stadium")
            {
                rotationA = Quaternion.Euler(0, 90, 0);
                Player.transform.SetPositionAndRotation(new Vector3(75, 0.5f, -360), rotationA);
            }
        }
        else if (raceType == "timeTrial")
        {

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
}