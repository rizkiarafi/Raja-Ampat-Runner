using System.Collections;
using UnityEngine;

public class HoleTrapManager : MonoBehaviour
{
    private PlayerController player;
    private GameManager gameManager;
    private Animator holeAnimator;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        holeAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        transform.Translate(-player.velocity.x * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger enter: " + other.tag);

        if (other.CompareTag("Limit"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Kenak player si lubang");
            holeAnimator.SetTrigger("IsDestroyed");
        }
    }

    void EventAnim()
    {
        gameManager.SetState(GameManager.GameState.GameOver);
    }
}