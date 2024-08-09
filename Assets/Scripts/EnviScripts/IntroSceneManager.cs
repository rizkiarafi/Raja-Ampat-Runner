using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    private Animator walkingAnimator;

    public AudioSource walkingAudioSourceIntro;
    public AudioSource screamAudioSourceIntro;
    public AudioSource droppingRocksAudioSourceIntro;

    public GameObject holeGameObj;

    private float screamVolume = 1f;
    private float screamFadeSpeed = 0.1f;

    void Start()
    {
        walkingAnimator = GetComponent<Animator>();
        StartCoroutine(TriggerWalkingAnimationAfterDelay(8f));
    }

    IEnumerator TriggerWalkingAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (walkingAnimator != null)
        {
            walkingAnimator.SetTrigger("IsWalking");
            walkingAudioSourceIntro.Play();
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void StopSoundWalking()
    {
        walkingAudioSourceIntro.Stop();
    }

    public void StartScreamSound()
    {
        screamAudioSourceIntro.volume = screamVolume;
        screamAudioSourceIntro.Play();
        StartCoroutine(FadeOutScream());
    }

    IEnumerator FadeOutScream()
    {
        while (screamVolume > 0.5f)
        {
            screamVolume -= screamFadeSpeed * Time.deltaTime;
            screamVolume = Mathf.Max(screamVolume, 0.5f);
            screamAudioSourceIntro.volume = screamVolume;
            yield return null;
        }
    }

    public void StopScreamSound()
    {
        screamAudioSourceIntro.Stop();
    }

    public void ShowTheHole()
    {
        holeGameObj.SetActive(true);
        droppingRocksAudioSourceIntro.Play();
    }
}
