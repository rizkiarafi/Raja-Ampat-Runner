using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleCoinBehaviour : MonoBehaviour
{
    [SerializeField] float doubleCoinDuration;
    int doubleCoinStack;
    bool isUsingDoubleCoin;
    bool isCoroutineRunning;

    private void Awake()
    {
        doubleCoinDuration = PowerUpsDuration.Instance.DoubleCoinDuration;
    }

    private void Update()
    {
        if (doubleCoinStack > 0)
        {
            isUsingDoubleCoin = true;
        }
        else
        {
            isUsingDoubleCoin = false;
        }

        if (isUsingDoubleCoin)
        {
            if (!isCoroutineRunning)
            {
                StartCoroutine(TurnOffDoubleCoin(doubleCoinDuration));
                isCoroutineRunning = true;
            }
        }
    }

    IEnumerator TurnOffDoubleCoin(float time)
    {
        yield return new WaitForSeconds(time);
        doubleCoinStack--;
        isCoroutineRunning = false;
    }

    public void AddStack()
    {
        doubleCoinStack++;
    }

    public bool GetIsUsingDoubleCoin()
    {
        return isUsingDoubleCoin;
    }
}
