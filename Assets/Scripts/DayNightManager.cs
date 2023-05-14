using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    public Light sun;
    public float xDirectionReview;
    public bool isIntensityZero = false;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(1.5f * Time.deltaTime, 0));
        xDirectionReview = transform.rotation.x;
        if (Mathf.Abs(transform.rotation.x) > 0.965923f && !isIntensityZero)
        {
            sun.intensity = 0;
            isIntensityZero = true;
        } else if (Mathf.Abs(transform.rotation.x) < 0.001f && isIntensityZero)
        {
            sun.intensity = 1;
            isIntensityZero = false;
        }
    }
}
