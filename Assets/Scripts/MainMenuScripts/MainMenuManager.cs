using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Slider volumeSlider;
    public Animator fadeAnimator;

    private bool isSettingsPanelActive = false;
    private bool isShopPanelActive = false;

    public AudioSource backgroundMusic;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            fadeAnimator.SetTrigger("IsFade");
            StartCoroutine(LoadAfterFade());
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        settingsButton.onClick.AddListener(() =>
        {
            // Toggle visibilitas panel pengaturan
            isSettingsPanelActive = !isSettingsPanelActive;
            settingsPanel.SetActive(isSettingsPanelActive);

            if (isShopPanelActive)
            {
                isShopPanelActive = false;
                shopPanel.SetActive(isShopPanelActive);
            }
        });

        shopButton.onClick.AddListener(() =>
        {
            // Toggle visibilitas panel shop
            isShopPanelActive = !isShopPanelActive;
            shopPanel.SetActive(isShopPanelActive);

            if (isSettingsPanelActive)
            {
                isSettingsPanelActive = false;
                settingsPanel.SetActive(isSettingsPanelActive);
            }
        });

        // Menambahkan listener untuk perubahan nilai pada Toggle musik
        musicToggle.onValueChanged.AddListener((value) =>
        {
            SetMusic(value);
        });

        // Menambahkan listener untuk perubahan nilai pada Slider volume
        volumeSlider.onValueChanged.AddListener((value) =>
        {
            SetVolume(value);
        });

        SetMusic(PlayerPrefs.GetInt("MusicOn", 1) == 1);
        SetVolume(PlayerPrefs.GetFloat("Volume", 1f));
    }

    private void Load(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private IEnumerator LoadAfterFade()
    {
        yield return new WaitForSeconds(1f);
        Load("IntroScene");
    }

    // Method untuk mengatur musik (ON/OFF)
    private void SetMusic(bool isMusicOn)
    {
        PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
        // Tambahkan logika untuk mengatur musik sesuai nilai isMusicOn
        if (isMusicOn)
        {
            backgroundMusic.Play();
        }
        else
        {
            backgroundMusic.Pause();
        }
    }

    // Method untuk mengatur volume
    private void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();

        backgroundMusic.volume = volume;
    }
}
