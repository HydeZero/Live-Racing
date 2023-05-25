using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#pragma warning disable IDE1006 // Naming Styles
public class lightManager : MonoBehaviour
#pragma warning restore IDE1006 // Naming Styles
{
    public bool IsLightOn = false;
    public GameObject Lights;

    // Start is called before the first frame update.
    private void Start()
    {
        Lights.SetActive(false);
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
            Lights.SetActive(true);
            IsLightOn = true;
        }
        else if (IsLightOn)
        {
            Lights.SetActive(false);
            IsLightOn = false;
        }
    }
}
