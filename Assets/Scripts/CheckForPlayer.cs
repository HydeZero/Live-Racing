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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SendName();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            raceManagerScript.DeactivateButtonReceiver();
        }
    }
    // Sends the set name to the RaceManager script
    public void SendName()
    {
        raceManagerScript.RaceNameSelected = RaceName;
        raceManagerScript.SetRaceName();
    }
}
