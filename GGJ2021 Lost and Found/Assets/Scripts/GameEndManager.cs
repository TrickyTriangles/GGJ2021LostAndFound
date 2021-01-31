using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEndManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI timer_text;
    [SerializeField] private Image player_image;
    [SerializeField] private Image statue_image;

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
            player_image.gameObject.SetActive(true);
            statue_image.gameObject.SetActive(true);
        }
        else
        {
            text.text = "You got caught!";
            player_image.gameObject.SetActive(false);
            statue_image.gameObject.SetActive(false);
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
