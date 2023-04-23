using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightManager : MonoBehaviour
{
    public bool IsLightOn = false;
    public GameObject Lights;

    // Start is called before the first frame update
    private void Start()
    {
        Lights.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TurnLights();
        }
    }
    public void TurnLights()
    {
        if (!IsLightOn)
        {
            Lights.gameObject.SetActive(true);
            IsLightOn = true;
        }
        else if (IsLightOn)
        {
            Lights.gameObject.SetActive(false);
            IsLightOn = false;
        }
    }
}
