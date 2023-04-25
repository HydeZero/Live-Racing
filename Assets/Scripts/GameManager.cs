using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject ProgressMenu;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0.001f;
            PauseMenu.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }

    public void LoadProgressMenu()
    {
        PauseMenu.SetActive(false);
        ProgressMenu.SetActive(true);
    }

    public void ReturnToPause()
    {
        ProgressMenu.SetActive(false);
        PauseMenu.SetActive(true);
    }
}
