using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer_readout;
    private bool is_game_active;

    private void Start()
    {
    }

    private void Update()
    {
        timer_readout.text = GameController.Instance.GetGameTimerReadout();
    }

    public bool GetIsGameComplete()
    {
        return is_game_active;
    }

    public void CompleteLevel()
    {
        is_game_active = false;
        GameController.Instance.SetGameWon(true);
        StartCoroutine(GameEndRoutine());
    }

    private IEnumerator GameRoutine()
    {
        while (is_game_active)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator GameEndRoutine()
    {
        GameController.Instance.StopTimer();
        yield return null;

        SceneManager.LoadScene("GameEnd");
    }
}

public class Timer
{
    private bool is_active;
    private int minutes = 0;
    private int seconds = 0;
    private float centiseconds = 0f;

    public void Update()
    {
        if (is_active)
        {
            AddTime(Time.deltaTime);
        }
    }

    private void AddTime(float deltaTime)
    {
        centiseconds += deltaTime;

        while (centiseconds > 1f)
        {
            seconds++;
            centiseconds -= 1f;

            while (seconds > 60)
            {
                minutes++;
                seconds -= 60;
            }
        }
    }

    public void RestartTimer()
    {
        minutes = 0;
        seconds = 0;
        centiseconds = 0;
    }

    public void StartTimer()
    {
        is_active = true;
    }

    public void StopTimer()
    {
        is_active = false;
    }

    public string GetTimeReadout()
    {
        int centi = (int)(centiseconds * 100f);
        string readout = minutes.ToString("D2") + ":" + seconds.ToString("D2") + "." + centi.ToString("D2");

        return readout;
    }
}