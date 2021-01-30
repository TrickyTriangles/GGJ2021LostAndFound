using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    [SerializeField] private AudioSource audio_source;
    private bool is_game_complete;
    private bool is_game_won;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(1);
    }

    public void StartNewGame()
    {
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
}
