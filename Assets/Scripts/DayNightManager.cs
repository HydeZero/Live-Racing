using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    public Light sun;
    public float xDirection = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.5f * Time.deltaTime, 0));
        xDirection = transform.rotation.x;
        if (xDirection > 0.96f)
        {
            sun.intensity = 0;
        } else if (xDirection < 0.001f)
        {
            transform.Rotate(new Vector3(-359, 0, 0));
            sun.intensity = 1;
        }
    }
}
