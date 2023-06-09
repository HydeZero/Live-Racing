using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DealershipCarManager : MonoBehaviour
{
    public bool isRecieverActive; // I don't care if I misspelled this lol
    public GameObject dealershipNotification;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public GameObject dealershipGUI;
    public GameObject player;
    public GameObject[] cars;
    public string[] carNames;
    public string[] carDescriptions;
    public int[] carPrices;
    public TextMeshProUGUI carName;
    public TextMeshProUGUI carDescription;
    public TextMeshProUGUI carPrice;
    public List<string> carsPurchased;
    public int currentCash;
    public TextMeshProUGUI currentCashObject;
    public int currentCarIndexDealership;
    public TextMeshProUGUI getInButton;
    public TextMeshProUGUI getInButtonTransport;
    public TextMeshProUGUI getInButtonDriver2021;
    public int randomThing;
    public int carnameindex;
    public GameObject GetInButtonTransportbutton;
    public GameObject GetInButtonDriver2021button;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ConstantlyGetPlayer());
    }

    void Update()
    {
        if (isRecieverActive && Input.GetKeyDown(KeyCode.E))
        {
            dealershipGUI.SetActive(true);
            playerPosition = player.transform.position;
            playerRotation = player.transform.rotation;
            dealershipNotification.SetActive(false);
        }
        currentCashObject.text = $"Cash: ${currentCash}";
    }
    // If the player touches it, open a popup that asks the player to press E to enter the dealership.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isRecieverActive = true;
            dealershipNotification.SetActive(true);
        }
    }
    // If the player is not touching anymore, deactivate the receiver.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isRecieverActive = false;
            dealershipNotification.SetActive(false);
        }
    }

    public void ExitDealershipGUI()
    {
        dealershipGUI.SetActive(false);
        dealershipNotification.SetActive(true);
    }

    public void SwitchCar(string CarName)
    {
        if (carsPurchased.Contains(CarName))
        {
            carnameindex = carsPurchased.IndexOf(CarName);
            Destroy(player);
            Instantiate(cars[carnameindex], playerPosition, playerRotation);
            player = GameObject.Find($"Player{CarName}(Clone)");
            ExitDealershipGUI();
        }
        else
        {
            randomThing = Random.Range(0,7);
            if (randomThing == 0)
            {
                getInButton.text = "nope.mp3";
            }
            else if (randomThing == 1)
            {
                getInButton.text = "Don't steal!";
            }
            else if (randomThing == 2)
            {
                getInButton.text = "Operation Get-In-Car-Without-Buying-It failed.";
            }
            else if (randomThing == 3)
            {
                getInButton.text = "Buy this car first.";
            }
            else if (randomThing == 4)
            {
                getInButton.text = "getInCar.exe has stopped working.";
            }
            else if (randomThing == 5)
            {
                getInButton.text = "Access Denied.";
            }
            else if (randomThing == 6)
            {
                getInButton.text = "Error 404: car not found";
            }
            else
            {
                getInButton.text = "Error generating error message. Ironic, huh?";
            }
        }
    }

    public void LoadCarInfo(int index)
    {
        if (carNames[index] == "Kyle Driver 2021")
        {
            getInButton = getInButtonDriver2021;
            GetInButtonTransportbutton.SetActive(false);
            GetInButtonDriver2021button.SetActive(true);
        }
        else if (carNames[index] == "Lukeman Transporter 2019")
        {
            getInButton = getInButtonTransport;
            GetInButtonTransportbutton.SetActive(true);
            GetInButtonDriver2021button.SetActive(false);
        }
        currentCarIndexDealership = index;
        carDescription.text = carDescriptions[index];
        carName.text = carNames[index];
        
        if (carsPurchased.Contains(carNames[index]))
        {
            carPrice.text = "PURCHASED";
        } else
        {
            carPrice.text = $"Cost: {carPrices[index]}";
        }
    }

    public void PurchaseCars()
    {
        if (carsPurchased.Contains(carNames[currentCarIndexDealership]))
        {
            carPrice.text = "PURCHASED";
        } else if (!carsPurchased.Contains(carNames[currentCarIndexDealership]))
        {
            if (currentCash >= carPrices[currentCarIndexDealership])
            {
                carsPurchased.Add(carNames[currentCarIndexDealership]);
                currentCash -= carPrices[currentCarIndexDealership];
                Debug.Log($"Purchased {carNames[currentCarIndexDealership]} at {carPrices[currentCarIndexDealership]}");
                carPrice.text = "PURCHASED";
            } else
            {
                carPrice.text = "NOT ENOUGH CASH";
            }
        }
    }

    public IEnumerator ConstantlyGetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(ConstantlyGetPlayer());
    }
}
