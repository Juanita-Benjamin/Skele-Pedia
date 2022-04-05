using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    public GameObject PauseMenu;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }
    public void PauseGame ()
    {
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
            PauseMenu.SetActive(true);
        }
        else 
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ResumeGame ()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }
    public void ExitGame ()
    {
        Application.Quit();
    }
}
