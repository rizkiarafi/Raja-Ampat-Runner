using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : PowerUpBehaviour
{
    protected override void CollectPowerUp(Collider2D collider)
    {
        collider.GetComponent<PlayerShieldBehaviour>().AddStack();
    }
}
