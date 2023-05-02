using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public int checkpointNum;
    public int TotalLaps;
    public int TotalCheckpoints;
    public bool IsCheckpointUsed;
    public RaceManager raceManagerScript;
    public List<int> checkpointsUsed;
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
            if (raceManagerScript.lap < TotalLaps)
            {
                if (checkpointsUsed.Contains(checkpointNum - 1))
                {
                    if (checkpointNum == TotalCheckpoints)
                    {
                        if (!IsCheckpointUsed)
                        {
                            raceManagerScript.lap++;
                            checkpointsUsed.Clear();
                            checkpointsUsed.Add(0);
                            if (raceManagerScript.lap == TotalLaps)
                            {
                                if (raceManagerScript.Type == "regular")
                                {
                                    raceManagerScript.OnRaceFinished();
                                }
                                else if (raceManagerScript.Type == "timeTrial")
                                {
                                    raceManagerScript.OnTimeTrialFinish();
                                }
                                else
                                {
                                    Debug.Log("Uh, something's wrong here. The race type does not exist! If you are a player, you can exit the application using Alt+F4 or Command+G.");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!IsCheckpointUsed)
                        {
                            StartCoroutine(CheckpointUnuse());
                            checkpointsUsed.Add(checkpointNum);
                        }
                    }
                }
            }
        }
    }

    IEnumerator CheckpointUnuse()
    {
        yield return new WaitForSeconds(5);
        IsCheckpointUsed = false;
    }
}
