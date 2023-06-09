using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public int checkpointNum;
    public RaceManager raceManagerScript;
    public bool IsCheckpointUsed;
    // Start is called before the first frame update
    void Start()
    {
        raceManagerScript = GameObject.Find("GameManager").GetComponent<RaceManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (raceManagerScript.lap < raceManagerScript.TotalLaps + 1)
            {
                if (raceManagerScript.checkpointsUsed.Contains(checkpointNum - 1))
                {
                    if (checkpointNum == raceManagerScript.TotalCheckpoints)
                    {
                        if (!IsCheckpointUsed)
                        {
                            raceManagerScript.ClearAndCheck();
                        }
                    }
                    else
                    {
                        if (!IsCheckpointUsed)
                        {
                            StartCoroutine(CheckpointUnuse());
                            raceManagerScript.checkpointsUsed.Add(checkpointNum);
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
