using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGameSceneManager : MonoBehaviour
{
    private Animator fallingAnimator;
    public AudioSource heartBeatAudioSource;
    public AudioSource fallingAudioSource;
    private float fallingVolume = 1f;
    private float fallingFadeSpeed = 0.1f;

    public GameManager gameManager;

    void Start()
    {
        fallingAnimator = GetComponent<Animator>();
    }

    public void StartFallingSound()
    {
        fallingAudioSource.volume = fallingVolume;
        fallingAudioSource.Play();
        StartCoroutine(FadeOutFalling());
    }

    public void StartRunningAnimation()
    {
        fallingAnimator.SetTrigger("IsRunning");
        gameManager.StartGame();
    }

    public void StartHeartBeatSound()
    {
        heartBeatAudioSource.Play();
    }

    IEnumerator FadeOutFalling()
    {
        while (fallingVolume > 0.5f)
        {
            fallingVolume -= fallingFadeSpeed * Time.deltaTime;
            fallingVolume = Mathf.Max(fallingVolume, 0.5f);
            fallingAudioSource.volume = fallingVolume;
            yield return null;
        }
    }

    public void StopFallingSound()
    {
        fallingAudioSource.Stop();
    }

    public void StopHeartBeatSound()
    {
        heartBeatAudioSource.Stop();
    }
}
