using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("Menu");
    }
}
