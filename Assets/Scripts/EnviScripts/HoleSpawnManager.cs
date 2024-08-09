using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSpawnManager : MonoBehaviour
{
    public GameObject holePrefab;
    public Transform[] holeSpawnPoint;

    public float minHoleSpawnDelay = 6f;
    public float maxHoleSpawnDelay = 8f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnHoles());
    }

    IEnumerator SpawnHoles()
    {
        // Loop tak terbatas untuk terus-menerus melakukan spawn hole
        while (true)
        {
            if (gameManager.currentState != GameManager.GameState.GameStart)
            {
                yield return null;
                continue;
            }

            float spawnDelay = Random.Range(minHoleSpawnDelay, maxHoleSpawnDelay);
            yield return new WaitForSeconds(spawnDelay);

            Transform spawnPoint = holeSpawnPoint[Random.Range(0, holeSpawnPoint.Length)];

            Instantiate(holePrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
