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
        
    }

    // Update is called once per frame
    void Update()
    {
        float relativeAngle = ((transform.rotation.eulerAngles.x + 540) % 360) - 180;
        transform.Rotate(new Vector3(0.5f * Time.deltaTime, 0));
        if (Mathf.Abs(relativeAngle) > 180)
        {
            if (Mathf.Abs(relativeAngle) > 360)
            {
                gameObject.transform.Rotate(new Vector3(-360, 0, 0));
                sun.intensity = 1;
            }
            else
            {
                sun.intensity = 0;
            }
        }
    }
}
