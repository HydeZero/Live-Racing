using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCareer : MonoBehaviour
{
    public float horizontalInput;
    public float accelerationInput;
    public float speed;
    public float turnSpeed;
    public Rigidbody playerRB;
    public Vector3 direction;
    public bool onGround;
    public Vector3 carSpeed;
    public Vector3 carspeedcalcA;
    public Vector3 carspeedcalcB;
    public int switching;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (switching == 0)
        {
            carspeedcalcA = playerRB.gameObject.transform.position;
            switching = 1;
        } else if (switching == 1)
        {
            carspeedcalcB = playerRB.gameObject.transform.position;
            carSpeed = carspeedcalcA - carspeedcalcB;
            switching = 0;
        }
        horizontalInput = Input.GetAxis("Horizontal");
        accelerationInput = Input.GetAxis("Vertical");
        if (onGround && !(carSpeed.x <= .01f) && !(carSpeed.y <= .01f) && !(carSpeed.z <= .01f))
        {
            playerRB.AddRelativeForce(new Vector3(0, 0, speed * (accelerationInput - .2f) * Time.deltaTime), ForceMode.Acceleration);
            transform.Rotate(new Vector3(0, Time.deltaTime * turnSpeed * horizontalInput * accelerationInput));
        }
        else
        {
            playerRB.AddRelativeForce(new Vector3(0, 0, speed * accelerationInput * Time.deltaTime), ForceMode.Acceleration);
            transform.Rotate(new Vector3(0, Time.deltaTime * turnSpeed * horizontalInput * accelerationInput));
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}