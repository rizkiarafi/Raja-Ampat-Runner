using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [Header("Power-ups Spawn Manager")]
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] List<PowerUps> powerUps;

    [SerializeField] float spawnDelay;

    float xSpawnPos;

    private void Awake()
    {
        Vector2 rightEdgePos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));
        xSpawnPos = rightEdgePos.x + 2;
    }

    private void Start()
    {
        StartCoroutine(TimedSpawnPowerUps(spawnDelay));
    }

    IEnumerator TimedSpawnPowerUps(float time)
    {
        while (true)
        {
            if (gameManager.currentState != GameManager.GameState.GameStart)
            {
                yield return null;
                continue;
            }

            int randomValue = Random.Range(0, 100);
            int cummulativeWeight = 0;

            foreach (var powerUp in powerUps)
            {
                cummulativeWeight += powerUp.spawnRatePercentage;
                if (randomValue <= cummulativeWeight)
                {
                    SpawnPowerUp(powerUp.powerUpObject);
                    break;
                }
            }
            yield return new WaitForSeconds(time);
        }
    }

    private void SpawnPowerUp(GameObject obj)
    {
        float ySpawnPos = spawnPositions[Random.Range(0, spawnPositions.Length)].position.y;
        Vector2 newPos = new Vector2(xSpawnPos, ySpawnPos);
        Instantiate(obj, newPos, Quaternion.identity);
    }
}
