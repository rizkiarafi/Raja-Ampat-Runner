using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteTrap1 : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;
    // [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rb;
    private bool isFalling = true;
    private PlayerController player;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !player.GetComponent<PlayerShieldBehaviour>().GetIsUsingShield())
        {
            gameManager.SetState(GameManager.GameState.GameOver);
            player.GetComponent<Animator>().SetTrigger("UpperFall");
        }
        else if (collision.gameObject.CompareTag("Player") && player.GetComponent<PlayerShieldBehaviour>().GetIsUsingShield())
        {
            player.GetComponent<PlayerShieldBehaviour>().SubstractStack();
        }
    }
}
