using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody2D rb;
    public int coinValue = 1;
    private Animator animator;

    [Header("Magnet Effector")]
    [SerializeField] float magnetStrength = 10f;
    private bool isAffectedByMagnet;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player.GetComponent<PlayerDoubleCoinBehaviour>().GetIsUsingDoubleCoin())
        {
            coinValue = 2;
        }
        else
        {
            coinValue = 1;
        }
    }

    void FixedUpdate()
    {
        if (isAffectedByMagnet)
            rb.position = Vector2.MoveTowards(rb.position, player.transform.position, magnetStrength * Time.deltaTime);
        else
            rb.velocity = new Vector2(-player.velocity.x, rb.velocity.y);
    }


    void CollectCoin()
    {
        PlayerManager.numberOfCoins += coinValue;
        PlayerPrefs.SetInt("NumberOfCoins", PlayerPrefs.GetInt("NumberOfCoins", 0) + coinValue);
        PlayerPrefs.Save();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoin();
        }
        else if (other.CompareTag("Limit"))
        {
            Destroy(gameObject);
        }
    }

    public void SetIsAffectedByMagnet(bool condition)
    {
        isAffectedByMagnet = condition;
    }
}