using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUIManager : MonoBehaviour
{
    public GameObject DeletePopup1;
    public GameObject DeletePopup2;

    public void BeginDistance()
    {
        SceneManager.LoadScene(1);
    }

    public void BeginCareer()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void Restart()
    {
        UnityEngine.Diagnostics.Utils.ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.Abort);
    }

    public void ShowDeletePopup1()
    {
        DeletePopup1.SetActive(true);
    }

    public void ShowDeletePopup2()
    {
        DeletePopup1.SetActive(false);
        DeletePopup2.SetActive(true);
    }

    public void ExitDeletePopups()
    {
        DeletePopup1.SetActive(false);
        DeletePopup2.SetActive(false);
    }

    public void DeleteSaveData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            File.Create(path);
        }
        ExitDeletePopups();
    }
}
