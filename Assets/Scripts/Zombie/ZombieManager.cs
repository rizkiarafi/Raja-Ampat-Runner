using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    private Animator animator;
    private GameManager gameManager;
    public AudioSource idleAudioSource;
    public AudioSource chaseAudioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();

        idleAudioSource.Play();
    }

    void Update()
    {
        if (gameManager.currentState == GameManager.GameState.GameStart && animator != null)
        {
            chaseAudioSource.Play();
            animator.SetTrigger("IsRunning");
            idleAudioSource.Stop();
        }
    }
}
