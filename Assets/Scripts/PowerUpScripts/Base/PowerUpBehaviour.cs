using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectPowerUp(other);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Limit"))
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void CollectPowerUp(Collider2D collider)
    {
        //do something in inherited scripts
    }
}
