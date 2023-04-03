using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightManager : MonoBehaviour
{
    public bool IsLightOn = false;
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void TurnLights()
    {
        if (!IsLightOn)
        {
            gameObject.SetActive(true);
            IsLightOn = true;
        } else if (IsLightOn)
        {
            gameObject.SetActive(false);
            IsLightOn = false;
        }
    }
}
