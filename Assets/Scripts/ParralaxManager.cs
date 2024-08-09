using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxManager : MonoBehaviour
{
    public float depth = 1;

    PlayerController player;
    private GameManager gameManager;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        if (gameManager.currentState != GameManager.GameState.GameStart)
        {
            return;
        }

        float realVelocity = player.velocity.x / depth;
        Vector2 pos = transform.position;

        pos.x -= realVelocity * Time.fixedDeltaTime;

        if (pos.x <= -18)
            pos.x = 18;

        transform.position = pos;
    }
}
