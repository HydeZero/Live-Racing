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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateRacingAI()
    {
        if (RaceName == "Stadium")
        {

        }
    }

    public void RacingAIProcess()
    {
        AITurnCalculation = (transform.rotation.eulerAngles.y - NextCheckpoint.transform.rotation.eulerAngles.y) / NextCheckpoint.transform.rotation.eulerAngles.y;
        AIForceCalculation = (transform.position - NextCheckpoint.transform.position).normalized * AISpeed;
        AIRB.AddRelativeForce(AIForceCalculation);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI Checkpoint"))
        {
            checkpointIndex++;
            NextCheckpoint = GameObject.Find($"{RaceName}Checkpoint{checkpointIndex}");
        }
    }
}
