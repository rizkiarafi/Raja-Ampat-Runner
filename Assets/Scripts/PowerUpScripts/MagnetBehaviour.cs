using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBehaviour : PowerUpBehaviour
{
    protected override void CollectPowerUp(Collider2D collider)
    {
        collider.GetComponent<PlayerMagnetBehaviour>().AddStack();
        collider.GetComponent<PlayerMagnetBehaviour>().SetIsUsingMagnet(true);
    }
}
