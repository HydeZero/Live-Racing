using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceManager : MonoBehaviour
{
    public float distance = 0f;
    private PlayerController playerControllerScript;
    public TextMeshProUGUI distanceText;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            StartCoroutine(distanceAdd());
            distanceText.text = "Distance: " + distance + "m";
        }
    }

    IEnumerator distanceAdd()
    {
        yield return new WaitForSeconds(1);
        distance += 2;
    }
}
