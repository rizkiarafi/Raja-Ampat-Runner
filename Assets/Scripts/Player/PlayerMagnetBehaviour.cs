using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnetBehaviour : MonoBehaviour
{
    [SerializeField] float magnetRadiusEffectivity;
    [SerializeField] float magnetDuration;

    bool isUsingMagnet;
    bool isCoroutineRunning;

    int magnetStack;

    private void Awake()
    {
        magnetDuration = PowerUpsDuration.Instance.MagnetDuration;
    }

    private void Update()
    {
        if (magnetStack > 0)
        {
            isUsingMagnet = true;
        }
        else
        {
            isUsingMagnet = false;
        }
    }

    void FixedUpdate()
    {
        if (isUsingMagnet)
        {
            if (!isCoroutineRunning)
            {
                StartCoroutine(TurnOffMagnet(magnetDuration));
                isCoroutineRunning = true;
            }
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, magnetRadiusEffectivity);
            foreach (var hit in hitColliders)
            {
                if (hit.CompareTag("Coin"))
                {
                    hit.GetComponent<CoinManager>().SetIsAffectedByMagnet(true);
                }
            }
        }
    }

    IEnumerator TurnOffMagnet(float time)
    {
        yield return new WaitForSeconds(time);
        magnetStack--;
        isCoroutineRunning = false;
    }

    public void SetIsUsingMagnet(bool condition)
    {
        isUsingMagnet = condition;
    }

    public bool GetIsUsingMagnet()
    {
        return isUsingMagnet;
    }

    public void AddStack()
    {
        magnetStack++;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, magnetRadiusEffectivity);
    }
}
