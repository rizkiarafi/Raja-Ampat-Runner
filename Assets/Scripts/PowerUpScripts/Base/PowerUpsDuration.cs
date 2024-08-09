using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsDuration : MonoBehaviour
{
    public static PowerUpsDuration Instance;

    [SerializeField] List<PowerUpSO> powerUpSO;
    public float MagnetDuration
    {
        get
        {
            return (powerUpSO[0].level > 1) ? powerUpSO[0].durationOrStack[powerUpSO[0].level - 2] : 7f;
        }
    }
    public int ShieldAmount
    {
        get
        {
            return (powerUpSO[1].level > 1) ? (int)powerUpSO[1].durationOrStack[powerUpSO[1].level - 2] : 1;
        }
    }
    public float DoubleCoinDuration
    {
        get
        {
            return (powerUpSO[2].level > 1) ? powerUpSO[2].durationOrStack[powerUpSO[2].level - 2] : 10f;
        }
    }
    public float InvisibilityDuration
    {
        get
        {
            return (powerUpSO[3].level > 1) ? powerUpSO[3].durationOrStack[powerUpSO[3].level - 2] : 7f;
        }
    }


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
}
