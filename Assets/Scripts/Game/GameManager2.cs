using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;

    [SerializeField] private float maxTime;
    [SerializeField] private int menuIndex;
    [SerializeField] private CanvasGroup GameOverScene;
    [SerializeField] private CanvasGroup PauseMenu;
    [SerializeField] private GameObject InGameUi;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip gameplayMusicClip;
    [SerializeField] private AudioClip gameOverMusicClip;
    private AudioClip previousClip;
    private bool pause = false;
    public static bool canPlayerUpdate = true;
    public static bool GameOver = false;
    private float time;

    private void Start()
    {
        musicSource = SoundManager.Instance.GetMusicSource();
        musicSource.loop = true;
        musicSource.clip = gameplayMusicClip;
        time = maxTime;
        DeactivateCanvasGroup(GameOverScene, false);
        DeactivateCanvasGroup(PauseMenu, false);
        Time.timeScale = 1;
        pause = false;
        canPlayerUpdate = true;
        GameOver = false;
    }

    private void DeactivateCanvasGroup(CanvasGroup cG, bool state)
    {
        cG.alpha = state ? 1 : 0;
        cG.interactable = state;
        cG.blocksRaycasts = state;
    }

    private void Update()
    {
        if (!pause)
        {
            time -= Time.deltaTime;
            int seconds = ((int)time % 60);
            int minutes = ((int)time / 60);
    
            timer.text = (string.Format("{0:00}:{1:00}", minutes, seconds));

            if (time < 0)
            {
                DeactivateCanvasGroup(GameOverScene, true);
                DeactivateCanvasGroup(PauseMenu, false);
                InGameUi.SetActive(false);
                pause = false;
                GameOver = true;
                canPlayerUpdate = false;
                Time.timeScale = 1;
        musicSource.loop = false;
            }
            canPlayerUpdate = true;
            Time.timeScale = 1;
        }
        else
        {
            canPlayerUpdate = false;
            Time.timeScale = 0.1f;
        }

        if (!GameOver)
        {
            DeactivateCanvasGroup(PauseMenu, pause);
            InGameUi.SetActive(!pause);
        }

        MusicController();
    }
    private void MusicController()
    {
        musicSource.clip = GameOver ? gameOverMusicClip : gameplayMusicClip;
        if (previousClip != musicSource.clip)
        {
            musicSource.Play();
        }

        previousClip = musicSource.clip;
    }
    public void OnPause()
    {
        pause = !pause;
    }

    public void GoToMenu()
    {
        musicSource.loop = true;
        musicSource.clip = SoundManager.Instance.mainMenu;
        SceneManager.LoadScene(0);
    }
    public void Reset()
    {
        SceneManager.LoadScene(1);
    }

}
