using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    public Light sun;
    public float xDirection = 0;
    public bool isIntensityZero = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(1.5f * Time.deltaTime, 0));
        xDirection = transform.rotation.x;
        if (Mathf.Abs(xDirection) > 0.96f && !isIntensityZero)
        {
            sun.intensity = 0;
            isIntensityZero = true;
        } else if (Mathf.Abs(xDirection) < 0.001f && isIntensityZero)
        {
            sun.intensity = 1;
            isIntensityZero = false;
        }
    }
}
