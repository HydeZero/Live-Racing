using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float horizontalInput;
    private Rigidbody playerRb;
    public bool gameOver = false;
    public bool powerupStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody component
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            MovePlayer();
        }
    }

    // Move the player around based on key inputs
    void MovePlayer()
    {
        // Get the value of the Horizontal Axis
        horizontalInput = Input.GetAxis("Horizontal");

        // Apply a force to the player to move to the right or the left
        playerRb.AddForce(horizontalInput * speed * Vector3.right);
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        powerupStatus = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !powerupStatus)
        {
            Debug.Log("Game Over! You hit an enemy.");
            gameOver = true;
        } else if (!collision.gameObject.CompareTag("Plane"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Debug.Log("Powerup Activated!");
            Destroy(other.gameObject);
            powerupStatus = true;
            StartCoroutine(PowerupCountdownRoutine());
        }
    }
}
