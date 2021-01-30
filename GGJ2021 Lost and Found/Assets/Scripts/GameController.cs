using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    [SerializeField] private AudioSource audio_source;
    private Timer game_timer;
    private bool is_game_complete;
    private bool is_game_won;

    private void Start()
    {
        game_timer = new Timer();
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        game_timer.Update();
    }

    public void StartNewGame()
    {
        game_timer = new Timer();
        game_timer.StartTimer();
        is_game_complete = false;
        is_game_won = false;
    }

    public void SetGameComplete(bool gc)
    {
        is_game_complete = gc;
    }

    public bool GetGameComplete()
    {
        return is_game_complete;
    }

    public void SetGameWon(bool gw)
    {
        is_game_won = gw;
    }

    public bool GetGameWon()
    {
        return is_game_won;
    }

    public void RestartTimer()
    {
        game_timer.RestartTimer();
    }

    public void StartTimer()
    {
        game_timer.StartTimer();
    }

    public void StopTimer()
    {
        game_timer.StopTimer();
    }

    public string GetGameTimerReadout()
    {
        return game_timer.GetTimeReadout();
    }
}
