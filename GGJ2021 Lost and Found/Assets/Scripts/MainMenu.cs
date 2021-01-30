using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MoveToScene("_BEN_TESTINGSCENE");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MoveToScene("Museum");
        }

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
