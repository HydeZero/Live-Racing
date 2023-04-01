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
    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        accelerationInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(0, 0, Time.deltaTime * speed * accelerationInput));
        transform.Rotate(new Vector3(0, Time.deltaTime * turnSpeed * horizontalInput * accelerationInput));
    }
}