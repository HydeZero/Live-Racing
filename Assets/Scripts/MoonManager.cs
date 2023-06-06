using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonManager : MonoBehaviour
{
    public Light moon;
    public GameObject sun;
    public float xDirectionReview;
    public bool isIntensityZero = false;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(sun.transform.rotation.eulerAngles.x + 180, sun.transform.rotation.eulerAngles.y, sun.transform.rotation.eulerAngles.z);
        if (Mathf.Abs(transform.rotation.x) > 0.965923f && !isIntensityZero)
        {
            moon.intensity = 0;
            isIntensityZero = true;
        }
        else if (Mathf.Abs(transform.rotation.x) < 0.001f && isIntensityZero)
        {
            moon.intensity = .5f;
            isIntensityZero = false;
        }
    }
}
