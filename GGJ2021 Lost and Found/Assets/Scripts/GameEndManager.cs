using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEndManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI timer_text;

    private void Awake()
    {
        if (!GameController.IsInitialized)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Start()
    {
        if (GameController.Instance.GetGameWon())
        {
            text.text = "You stole a really heavy thing!";
        }
        else
        {
            text.text = "You got caught!";
        }

        timer_text.text = "Final time: " + GameController.Instance.GetGameTimerReadout();

        StartCoroutine(EndScreenRoutine());
    }

    private IEnumerator EndScreenRoutine()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }
}
