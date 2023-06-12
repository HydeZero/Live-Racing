using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControllerCareer : MonoBehaviour
{
    public float horizontalInput;
    public float accelerationInput;
    public float accelerationForce;
    public float turnSpeed;
    public Rigidbody playerRB;
    public bool onGround;
    public float topSpeed;
    public TextMeshProUGUI speedometer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetPlayerRB());
        playerRB = gameObject.GetComponent<Rigidbody>();
        speedometer = GameObject.Find("Speedometer").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        speedometer.text = $"Speed: {(int)playerRB.velocity.magnitude} MPH";
        horizontalInput = Input.GetAxis("Horizontal");
        accelerationInput = Input.GetAxis("Vertical");
        if (onGround && playerRB.velocity.magnitude < .01f)
        {
            playerRB.AddRelativeForce(new Vector3(0, 0, accelerationForce * accelerationInput * Time.deltaTime), ForceMode.Acceleration);
            transform.Rotate(new Vector3(0, Time.deltaTime * turnSpeed * horizontalInput * accelerationInput));
        } else if (onGround && playerRB.velocity.magnitude < topSpeed)
        {
            playerRB.AddRelativeForce(new Vector3(0, 0, accelerationForce * (accelerationInput - .2f) * Time.deltaTime), ForceMode.Acceleration);
            transform.Rotate(new Vector3(0, Time.deltaTime * turnSpeed * horizontalInput * accelerationInput));
        } else if (onGround)
        {
            transform.Rotate(new Vector3(0, Time.deltaTime * turnSpeed * horizontalInput * accelerationInput));
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "GroundTrigger")
        {
            onGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "GroundTrigger")
        {
            onGround = false;
        }
    }

    public IEnumerator GetPlayerRB()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        yield return new WaitForSecondsRealtime(.5f);
        StartCoroutine(GetPlayerRB());
    }
}