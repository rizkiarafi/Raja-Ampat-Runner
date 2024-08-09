using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnManager : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform[] spawnPoints;
    public int coinsPerSpawn = 4;
    public float minSpawnDelay = 3f;
    public float maxSpawnDelay = 5f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        // Loop tak terbatas untuk terus-menerus melakukan spawn koin
        while (true)
        {
            if (gameManager.currentState != GameManager.GameState.GameStart)
            {
                yield return null;
                continue;
            }

            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(spawnDelay);

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Membuat koin sebanyak coinsPerSpawn
            for (int i = 0; i < coinsPerSpawn; i++)
            {
                // Menghitung posisi spawn koin berdasarkan posisi spawnPoint dan jumlah koin yang sudah dibuat
                Vector3 coinSpawnPosition = spawnPoint.position + Vector3.right * i;

                Instantiate(coinPrefab, coinSpawnPosition, Quaternion.identity);
            }
        }
    }
}