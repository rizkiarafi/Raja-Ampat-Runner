using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteTrap : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;
    // [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rb;
    private bool isFalling = true;
    private PlayerController player;

    private GameManager gameManager;

    private AudioSource audioSource;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (player.GetComponent<PlayerInvisibilityBehaviour>().GetIsUsingInvisibility())
            transform.GetComponent<BoxCollider2D>().enabled = false;
        else
            transform.GetComponent<BoxCollider2D>().enabled = true;
    }

    void FixedUpdate()
    {
        if (isFalling)
        {
            rb.velocity = new Vector2(0f, -fallSpeed);
        }
        else
        {
            rb.velocity = new Vector2(-player.velocity.x, rb.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PlayImpactSound();
            isFalling = false;
        }
        else if (collision.gameObject.CompareTag("Player") && !player.GetComponent<PlayerShieldBehaviour>().GetIsUsingShield())
        {
            gameManager.SetState(GameManager.GameState.GameOver);
            player.GetComponent<Animator>().SetTrigger("DownerFall");
        }
        else if (collision.gameObject.CompareTag("Player") && player.GetComponent<PlayerShieldBehaviour>().GetIsUsingShield())
        {
            player.GetComponent<PlayerShieldBehaviour>().SubstractStack();
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            GetComponent<BoxCollider2D>().isTrigger = enabled;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Limit"))
        {
            Destroy(gameObject);
        }
    }

    void PlayImpactSound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
