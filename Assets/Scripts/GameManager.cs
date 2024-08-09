using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public enum GameState { GameIntro, GameStart, GameOver }
    public GameState currentState;

    public GameObject gameOverPanel;
    public GameObject pauseManagerButton;
    public PlayableDirector gameOverTimeline;

    private PlayerController player;
    public PauseManager pauseManager;

    private void Start()
    {
        SetState(GameState.GameIntro);
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void SetState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.GameIntro:
                Time.timeScale = 1;
                gameOverPanel.SetActive(false);
                pauseManagerButton.SetActive(false);
                if (gameOverTimeline != null)
                    gameOverTimeline.gameObject.SetActive(false);
                break;
            case GameState.GameStart:
                pauseManagerButton.SetActive(true);
                break;
            case GameState.GameOver:
                Time.timeScale = 1;
                player.velocity.x = 0;
                if (gameOverTimeline != null)
                {
                    gameOverTimeline.gameObject.SetActive(true);

                    StartCoroutine(WaitForTimelineThenPause());
                }
                break;
        }
    }

    public void StartGame()
    {
        SetState(GameState.GameStart);
    }

    private IEnumerator WaitForTimelineThenPause()
    {
        while (gameOverTimeline.state != PlayState.Paused)
        {
            yield return null;
        }

        pauseManagerButton.SetActive(false);
        gameOverPanel.SetActive(true);

        AdModAdsScript.Instance.ShowInterstitialAd();

        pauseManager.zombieSoundChaseManager.Stop();
        pauseManager.zombieSoundIdleManager.Stop();
        pauseManager.zombieSoundEatManager.Stop();
        pauseManager.screamPlayerSoundManager.Stop();
        pauseManager.audioPlayer1Manager.Stop();
        pauseManager.audioPlayer2Manager.Stop();
        pauseManager.fallingSoundManager.Stop();
        pauseManager.heartBeatSoundManager.Stop();

        Time.timeScale = 0;
    }
}