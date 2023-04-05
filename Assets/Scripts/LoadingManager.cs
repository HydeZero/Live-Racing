using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public float rotateSpeed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitForLoad");
    }

    // Update is called once per frame
    void Update()
    {
        rotateSpeed = 90;
        transform.Rotate(0, rotateSpeed, 0);
    }



    IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(4);
    }
}
