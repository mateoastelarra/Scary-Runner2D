using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    private int pau = 1;

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ReallyPlayGame()
    {
        SceneManager.LoadScene(1);
        GameManager.Instance.RestartScene();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void PauseGame()
    {
        if (pau == 0)
        {
            pau = 1;
            Time.timeScale = 1f;
        }
        else
        {
            pau = 0;
            Time.timeScale = 0f;
        }
        EventSystem.current.SetSelectedGameObject(null);
    }
}
