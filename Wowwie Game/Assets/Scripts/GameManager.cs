using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(01);
    }

    public void GoToBugScene()
    {
        SceneManager.LoadScene(05);
    }

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene(06);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
