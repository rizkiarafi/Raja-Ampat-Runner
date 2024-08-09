using System.Collections;
using UnityEngine;

public class StalactiteSpawner : MonoBehaviour
{
    public GameObject[] stalactitePrefab;
    public Transform[] spawnPoints;
    public float minSpawnDelay = 2f;
    public float maxSpawnDelay = 8f;

    private CameraShake cameraShake;
    private GameManager gameManager;

    private void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnStalactites());
    }

    IEnumerator SpawnStalactites()
    {
        // Loop tak terbatas untuk terus-menerus melakukan spawn stalaktit
        while (true)
        {
            if (gameManager.currentState != GameManager.GameState.GameStart)
            {
                yield return null;
                continue;
            }

            cameraShake.Shake();

            // Menunggu waktu random antara minSpawnDelay dan maxSpawnDelay
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(spawnDelay);

            // Memilih titik spawn secara acak dari array spawnPoints
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Memilih prefab stalaktit secara acak dari array stalactitePrefab
            GameObject selectedPrefab = stalactitePrefab[Random.Range(0, stalactitePrefab.Length)];

            // Melakukan spawn stalaktit di titik spawn yang dipilih
            Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}