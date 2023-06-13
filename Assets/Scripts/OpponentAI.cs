using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    public string RaceName;
    public float AITurnCalculation;
    public int TotalAICheckpoints;
    public GameObject NextCheckpoint;
    public Rigidbody AIRB;
    public bool IsCheckpointHit;
    public List<GameObject> checkpoints;
    public int checkpointIndex;
    public Vector3 AIForceCalculation;
    public int AISpeed;
    public string AIName;
    public int AIOffsetX;
    public int AIOffsetZ;
    public int RacePosition;
    public bool isRaceActive;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -5.25f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRaceActive)
        {
            RacingAIProcess();
        }
    }

    public void InitiateRacingAI()
    {
        CalculateAIOffset();
        if (RaceName == "Stadium")
        {
            transform.position = new Vector3(70 + AIOffsetX, 0.3f, -353 + AIOffsetZ);
        }
        NextCheckpoint = GameObject.Find($"{RaceName}Checkpoint{checkpointIndex}");
        isRaceActive = true;
    }

    public void RacingAIProcess()
    {
        AITurnCalculation = (transform.rotation.eulerAngles.y - NextCheckpoint.transform.rotation.eulerAngles.y) / NextCheckpoint.transform.rotation.eulerAngles.y;
        AIForceCalculation = (transform.position - NextCheckpoint.transform.position).normalized * AISpeed;
        AIRB.AddRelativeForce(AIForceCalculation);
        AIRB.AddTorque(new Vector3(0, AITurnCalculation, 0));

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI Checkpoint"))
        {
            checkpointIndex++;
            NextCheckpoint = GameObject.Find($"{RaceName}Checkpoint{checkpointIndex}");
        }
    }

    public void CalculateAIOffset()
    {
        RacePosition = (int)Random.Range(0, 6);
        if (RaceName == "Stadium")
        {
            if (RacePosition == 1)
            {
                Debug.Log($"No Offset Needed for AI: {AIName}, since they are in first.");
            } else if (RacePosition == 2)
            {
                Debug.Log("Lol this is still in development");
            }
        }
    }
}
