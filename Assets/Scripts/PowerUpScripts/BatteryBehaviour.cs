using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBehaviour : PowerUpBehaviour
{
    protected override void CollectPowerUp(Collider2D collider)
    {
        PlayerManager.batteryPower += 0.5f;
    }
}
