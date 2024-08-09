using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerUps
{
    public GameObject powerUpObject;
    [Range(0, 100)] public int spawnRatePercentage;
}
