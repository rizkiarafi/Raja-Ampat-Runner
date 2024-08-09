using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCoinBehaviour : PowerUpBehaviour
{
    protected override void CollectPowerUp(Collider2D collider)
    {
        collider.GetComponent<PlayerDoubleCoinBehaviour>().AddStack();
    }
}
