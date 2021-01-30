using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer_readout;
    [SerializeField] private Player player;
    [SerializeField] private Image fade_to_black;
    [SerializeField] private float fadeout_time = 3f;
    private bool is_game_active;

    private void Start()
    {
    }

    private void Update()
    {
        if (GameController.IsInitialized)
        {
            timer_readout.text = GameController.Instance.GetGameTimerReadout();
        }
    }

    public bool GetIsGameComplete()
    {
        return is_game_active;
    }

    public void CompleteLevel()
    {
        if (player != null)
        {
            player.SetInactive();
        }

        is_game_active = false;
        GameController.Instance.SetGameComplete(true);
        GameController.Instance.SetGameWon(true);
        GameController.Instance.StopTimer();

        if (fade_to_black != null)
        {
            StartCoroutine(GameEndRoutine());
        }
        else
        {
            SceneManager.LoadScene("GameEnd");
        }
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
        float timer = 0f;
        Color fade_color = Color.black;

        while (timer < fadeout_time)
        {
            timer += Time.deltaTime;
            fade_color.a = Mathf.Clamp(timer / fadeout_time, 0f, 1f);
            fade_to_black.color = fade_color;

            yield return null;
        }

        yield return new WaitForSeconds(1f);
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