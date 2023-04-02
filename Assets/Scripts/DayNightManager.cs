using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    public Light sun;
    public bool isIntensityZero = false;
    // Start is called before the first frame update
    void Start()
    {
        sun = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.5f * Time.deltaTime, 0));
        if (gameObject.transform.rotation.x > 360)
        {
            gameObject.transform.Rotate(new Vector3(-360,0,0));
            sun.intensity = 1;
        } else if (gameObject.transform.rotation.x > 180)
        {
            sun.intensity = 0;
        }
    }
}
