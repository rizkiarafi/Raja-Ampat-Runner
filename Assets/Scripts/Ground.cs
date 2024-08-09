using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float groundHeight;
    BoxCollider2D groundCollider;

    PlayerController player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        groundCollider = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (groundCollider.size.y / 1.6f);
    }
}
