using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealershipCarManager : MonoBehaviour
{
    public bool isRecieverActive; // I don't care if I misspelled this lol
    public GameObject dealershipNotification;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public GameObject dealershipGUI;
    public GameObject player;
    public GameObject[] cars;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (isRecieverActive && Input.GetKeyDown(KeyCode.E))
        {
            dealershipGUI.SetActive(true);
        }
    }
    // If the player touches it, open a popup that asks the player to press E to enter the dealership.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isRecieverActive = true;
        }
    }
    // If the player is not touching anymore, deactivate the receiver.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isRecieverActive = false;
        }
    }

    public void ExitDealershipGUI()
    {
        dealershipGUI.SetActive(false);
        
    }

    public void SwitchCar(int index, string CarName)
    {
        Destroy(player);
        Instantiate(cars[index]);
        player = GameObject.Find($"Player{CarName}");
    }
}
