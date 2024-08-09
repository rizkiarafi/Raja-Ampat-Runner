using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatreManager : MonoBehaviour
{
    private PlayerController player;

    private Rigidbody2D rb;
    public float batteryPowerPercentage = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(-player.velocity.x, rb.velocity.y);
    }

    void CollectBatre()
    {
        PlayerManager.batteryPower += batteryPowerPercentage;

        PlayerManager.batteryPower = Mathf.Clamp(PlayerManager.batteryPower, 0f, 1f);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectBatre();
        }
        else if (other.CompareTag("Limit"))
        {
            Destroy(gameObject);
        }
    }
}
