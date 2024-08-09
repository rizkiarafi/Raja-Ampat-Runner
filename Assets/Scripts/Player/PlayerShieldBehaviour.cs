using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShieldBehaviour : MonoBehaviour
{
    [SerializeField] Image shieldImage;

    bool isUsingShield;

    int shieldAmount;
    int shieldStack;

    private void Awake()
    {
        shieldAmount = PowerUpsDuration.Instance.ShieldAmount;
    }

    private void Update()
    {
        if (shieldStack > 0)
        {
            isUsingShield = true;
        }
        else
        {
            isUsingShield = false;
        }

        if (isUsingShield)
        {
            shieldImage.enabled = true;
        }
        else
        {
            shieldImage.enabled = false;
        }
    }

    public bool GetIsUsingShield()
    {
        return isUsingShield;
    }

    public void AddStack()
    {
        shieldStack += shieldAmount;
    }

    public void SubstractStack()
    {
        shieldStack--;
    }
}
