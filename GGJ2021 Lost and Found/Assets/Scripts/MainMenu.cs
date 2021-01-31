using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Awake()
    {
        if(!GameController.IsInitialized)
        {
            SceneManager.LoadScene(0);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void MoveToScene(string name)
    {
        GameController.Instance.RestartTimer();
        GameController.Instance.StartTimer();
        SceneManager.LoadScene(name);
    }
}
