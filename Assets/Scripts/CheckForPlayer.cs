using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayer : MonoBehaviour
{
    public RaceManager raceManagerScript;
    public string RaceName;
    public int totalRaceLaps;
    public int totalRaceCheckpoints;

    // Start is called before the first frame update
    void Start()
    {
        raceManagerScript = GameObject.Find("GameManager").GetComponent<RaceManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    // If the player touches it, send the name.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !raceManagerScript.isRaceActive)
        {
            SendNameLapsAndCheckpoints();
        }
    }
    // If the player is not touching anymore, deactivate the button receiver.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !raceManagerScript.isRaceActive)
        {
            raceManagerScript.DeactivateButtonReceiver();
        }
    }
    // Sends the race name to the RaceManager script
    public void SendNameLapsAndCheckpoints()
    {
        raceManagerScript.RaceNameSelected = RaceName;
        raceManagerScript.SetRaceName();
        raceManagerScript.TotalLaps = totalRaceLaps;
        raceManagerScript.TotalCheckpoints = totalRaceCheckpoints;
    }
}
