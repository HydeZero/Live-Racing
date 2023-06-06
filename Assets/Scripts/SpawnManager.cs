using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnItems;

    readonly float zSpawn = 12.0f;
    readonly float xSpawnRange = 12.0f;
    public float timeRepeatRate = 2.0f;
    public float timeUntilRepeatChange = 5;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnItem", 2, timeRepeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilRepeatChange > 0)
        {
            timeUntilRepeatChange -= Time.deltaTime;
        } else if (timeUntilRepeatChange < 0)
        {
            timeRepeatRate -= 0.1f;
            timeUntilRepeatChange = 5;
        }
    }

    void SpawnItem()
    {
        if (!playerControllerScript.gameOver)
        {
            float randomX = Random.Range(-xSpawnRange, xSpawnRange);
            int randomIndex = Random.Range(0, spawnItems.Length);

            Vector3 spawnPos = new(randomX, 0.75f, zSpawn);

            Instantiate(spawnItems[randomIndex], spawnPos, spawnItems[randomIndex].transform.rotation);

        }
    }
}
