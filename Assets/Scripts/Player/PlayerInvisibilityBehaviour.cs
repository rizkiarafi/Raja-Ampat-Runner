using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvisibilityBehaviour : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] playerBodyParts;
    [SerializeField] float invisibleDuration = 7f;

    bool isUsingInvisibility;
    bool isCoroutineRunning;

    int invisibilityStack;

    private void Awake()
    {
        invisibleDuration = PowerUpsDuration.Instance.InvisibilityDuration;
    }

    private void Update()
    {
        if (invisibilityStack > 0)
        {
            isUsingInvisibility = true;
        }
        else
        {
            isUsingInvisibility = false;
        }

        if (isUsingInvisibility)
        {
            if (!isCoroutineRunning)
            {
                StartCoroutine(TurnOffInvisibility(invisibleDuration));
                isCoroutineRunning = true;
            }
            SetBodyPartsOpacity(0.5f);
        }
        else
        {
            SetBodyPartsOpacity(1);
        }
    }

    private void SetBodyPartsOpacity(float opacity)
    {
        foreach (var bodyPart in playerBodyParts)
        {
            bodyPart.color = new Color(bodyPart.color.r, bodyPart.color.g, bodyPart.color.b, opacity);
        }
    }

    IEnumerator TurnOffInvisibility(float time)
    {
        yield return new WaitForSeconds(time);
        invisibilityStack--;
        isCoroutineRunning = false;
    }

    public bool GetIsUsingInvisibility()
    {
        return isUsingInvisibility;
    }

    public void AddStack()
    {
        invisibilityStack++;
    }
}
