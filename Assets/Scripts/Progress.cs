using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public TextMeshProUGUI racesComplete;
    public TextMeshProUGUI uniqueEventsFinished;
    public TextMeshProUGUI percentComplete;

    public List<string> racesCompleteNames;

    public int racesCompleteCount;
    public int uniqueEventsFinishedCount;
    public int totalUniqueEvents;
    public float percentCompleteCalculation;
    // Start is called before the first frame update
    void Start()
    {
        percentCompleteCalculation = ((uniqueEventsFinishedCount * 100) / totalUniqueEvents);
    }

    // Update is called once per frame
    void Update()
    {
        percentComplete.text = "Game Completed: " + percentCompleteCalculation + "%";
        racesComplete.text = "Races Finished: " + racesCompleteCount;
        uniqueEventsFinished.text = "Unique Events Finished: " + uniqueEventsFinishedCount;
        percentCompleteCalculation = ((uniqueEventsFinishedCount * 100) / totalUniqueEvents);
    }
}
