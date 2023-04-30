using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayer : MonoBehaviour
{
    public RaceManager raceManagerScript;
    public string RaceName;

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
        if (other.CompareTag("Player"))
        {
            SendName();
        }
    }
    // If the player is not touching anymore, deactivate the button receiver.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            raceManagerScript.DeactivateButtonReceiver();
        }
    }
    // Sends the race name to the RaceManager script
    public void SendName()
    {
        raceManagerScript.RaceNameSelected = RaceName;
        raceManagerScript.SetRaceName();
    }
}
