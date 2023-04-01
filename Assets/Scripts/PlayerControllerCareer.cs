using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCareer : MonoBehaviour
{
    public float horizontalInput;
    public float accelerationInput;
    public float speed;
    public float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        accelerationInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(0, 0, accelerationInput * speed * Time.deltaTime));
        transform.Rotate(new Vector3(0, turnSpeed * horizontalInput * Time.deltaTime * accelerationInput));
    }
}
