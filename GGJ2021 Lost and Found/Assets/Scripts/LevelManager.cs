using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public enum LevelState
    {
        NORMAL,
        SNEAK,
        FADEOUT
    }

    [SerializeField] private TextMeshProUGUI timer_readout;
    [SerializeField] private AudioSource bgm_normal;
    [SerializeField] private AudioSource bgm_sneak;
    [SerializeField] private Player player;
    [SerializeField] private Image fade_to_black;
    [SerializeField] private float fadeout_time = 3f;
    private LevelState level_state;
    private Coroutine crossfade_routine;
    private bool is_game_active = true;

    private void Start()
    {
        bgm_normal.volume = GameController.Instance.GetBGMVolume();
        bgm_sneak.volume = 0f;
        level_state = LevelState.NORMAL;
    }

    private void Update()
    {
        if (GameController.IsInitialized)
        {
            timer_readout.text = "Time: " + GameController.Instance.GetGameTimerReadout();
        }
    }

    public bool GetIsGameComplete()
    {
        return !is_game_active;
    }

    public void ChangeLevelState(LevelState new_state)
    {
        if (level_state != new_state && is_game_active)
        {
            level_state = new_state;

            if (crossfade_routine != null)
            {
                StopCoroutine(crossfade_routine);
            }

            crossfade_routine = StartCoroutine(BGMCrossfadeRoutine());
        }
    }

    public void CompleteLevel()
    {
        if (player != null)
        {
            player.SetInactive();
        }

        ChangeLevelState(LevelState.FADEOUT);
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

    private IEnumerator BGMCrossfadeRoutine()
    {
        float target_volume = GameController.Instance.GetBGMVolume();

        switch (level_state)
        {
            case LevelState.NORMAL:
                while (bgm_normal.volume < target_volume && bgm_sneak.volume > 0f)
                {
                    bgm_normal.volume = Mathf.MoveTowards(bgm_normal.volume, target_volume, 0.2f * Time.deltaTime);
                    bgm_sneak.volume = Mathf.MoveTowards(bgm_sneak.volume, 0f, 0.2f * Time.deltaTime);
                    yield return null;
                }
                break;
            case LevelState.SNEAK:
                while (bgm_normal.volume > 0f && bgm_sneak.volume < target_volume)
                {
                    bgm_normal.volume = Mathf.MoveTowards(bgm_normal.volume, 0f, 0.2f * Time.deltaTime);
                    bgm_sneak.volume = Mathf.MoveTowards(bgm_sneak.volume, target_volume, 0.2f * Time.deltaTime);
                    yield return null;
                }
                break;
            case LevelState.FADEOUT:
                break;
            default:
                break;
        }

        crossfade_routine = null;
    }

    private IEnumerator GameEndRoutine()
    {
        Color fade_color = Color.black;
        float timer = 0f;
        float bgm_normal_start_volume = bgm_normal.volume;
        float bgm_sneak_start_volume = bgm_sneak.volume;

        while (timer < fadeout_time)
        {
            float ratio = timer / fadeout_time;

            timer += Time.deltaTime;
            fade_color.a = Mathf.Clamp(ratio, 0f, 1f);
            fade_to_black.color = fade_color;

            bgm_normal.volume = Mathf.Lerp(bgm_normal_start_volume, 0f, ratio);
            bgm_sneak.volume = Mathf.Lerp(bgm_sneak_start_volume, 0f, ratio);

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