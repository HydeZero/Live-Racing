using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    public float rotateSpeed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 2, 1);
    }

    // Update is called once per frame
    void Update()
    {
        rotateSpeed += 0.4f;
        transform.Rotate(0, rotateSpeed, 0);
    }
}
