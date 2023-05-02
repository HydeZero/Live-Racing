using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public int checkpointNum;
    public int TotalLaps;
    public bool IsCheckpointUsed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (lap < TotalLaps)
            {
                if (checkpointNum == 3)
                {
                    if (!IsCheckpointUsed)
                    {
                        lap++;
                        if (lap == TotalLaps)
                        {

                        }
                    }
                } else
                {
                    if (!IsCheckpointUsed)
                    {

                    }
                }
            }
        }
    }
}
