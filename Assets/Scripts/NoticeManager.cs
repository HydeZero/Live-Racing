using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoticeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitThenLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitThenLoad()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);
    }
}
