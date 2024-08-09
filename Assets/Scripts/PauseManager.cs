using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public bool isPaused = false;
    private bool isSettingsPanelActive = false;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider volumeSlider;

    // all sound settings
    public AudioSource zombieSoundChaseManager;
    public AudioSource zombieSoundIdleManager;
    public AudioSource zombieSoundEatManager;
    public AudioSource screamPlayerSoundManager;
    public AudioSource audioPlayer1Manager;
    public AudioSource audioPlayer2Manager;
    public AudioSource fallingSoundManager;
    public AudioSource heartBeatSoundManager;

    void Start()
    {
        pauseMenu.SetActive(false);

        // Menambahkan listener untuk perubahan nilai pada Slider volume
        volumeSlider.onValueChanged.AddListener((value) =>
        {
            SetVolume(value);
        });

        SetVolume(PlayerPrefs.GetFloat("Volume", 1f));
    }

    public void PauseMenuButton()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);

            audioPlayer1Manager.Pause();
            audioPlayer2Manager.Pause();
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            settingsPanel.SetActive(false);
            
            audioPlayer1Manager.Play();
            audioPlayer2Manager.Play();
        }
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SettingsButton()
    {
        isSettingsPanelActive = !isSettingsPanelActive;
        settingsPanel.SetActive(isSettingsPanelActive);
    }

    public  void CloseSettingsButton()
    {
        settingsPanel.SetActive(false);
    }

    // Method untuk mengatur volume
    private void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
        
        zombieSoundChaseManager.volume = volume;
        zombieSoundIdleManager.volume = volume;
        zombieSoundEatManager.volume = volume;
        screamPlayerSoundManager.volume = volume;
        audioPlayer1Manager.volume = volume;
        audioPlayer2Manager.volume = volume;
        fallingSoundManager.volume = volume;
        heartBeatSoundManager.volume = volume;
    }
}
