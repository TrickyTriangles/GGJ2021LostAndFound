using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer_readout;
    private bool is_game_active;
    private Timer timer;

    private void Start()
    {
        timer = new Timer();
    }

    private void Update()
    {
        timer.AddTime(Time.deltaTime);
        timer_readout.text = timer.GetTimeReadout();
    }

    public void CompleteLevel()
    {
        is_game_active = false;
    }

    private IEnumerator GameRoutine()
    {
        while (is_game_active)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}

public class Timer
{
    private int minutes;
    private int seconds;
    private float centiseconds;

    public Timer()
    {
        minutes = 0;
        seconds = 0;
        centiseconds = 0;
    }

    public void AddTime(float deltaTime)
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

    public string GetTimeReadout()
    {
        int centi = (int)(centiseconds * 100f);
        string readout = minutes.ToString("D2") + ":" + seconds.ToString("D2") + "." + centi.ToString("D2");

        return readout;
    }
}