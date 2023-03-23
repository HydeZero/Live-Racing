using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnItems;

    private float zSpawn = 12.0f;
    private float xSpawnRange = 12.0f;
    public float timeRepeatRate = 2.0f;
    public float timeUntilRepeatChange = 5;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnItem", 2, timeRepeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilRepeatChange > 0)
        {
            timeUntilRepeatChange = timeUntilRepeatChange - Time.deltaTime;
        } else if (timeUntilRepeatChange < 0)
        {
            timeRepeatRate = timeRepeatRate - 0.1f;
            timeUntilRepeatChange = 5;
        }
    }

    void SpawnItem()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        int randomIndex = Random.Range(0, spawnItems.Length);

        Vector3 spawnPos = new Vector3(randomX, 0.75f, zSpawn);

        Instantiate(spawnItems[randomIndex], spawnPos, spawnItems[randomIndex].transform.rotation);
    }
}