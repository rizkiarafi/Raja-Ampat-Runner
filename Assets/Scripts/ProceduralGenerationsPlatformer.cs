using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerationsPlatformer : MonoBehaviour
{
    public GameObject[] groundPrefabs; // Array yg menyimpan prefabs platform
    public Transform spawnPoint; // Titik awal spawn
    public Transform destroyPoint; // Titik akhir destroy

    [SerializeField] private float depth = 1;

    private PlayerController player;

    // public float platformMoveSpeed = 5f;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(GenerateGroundRoutine());
    }

    private IEnumerator GenerateGroundRoutine()
    {
        while (true)
        {
            GenerateGround();
            yield return new WaitForSeconds(1f);
        }
    }

    // Menghasilkan platform baru
    public void GenerateGround()
    {
        int randomIndex = Random.Range(0, groundPrefabs.Length); // Pilih prefab platform acak
        GameObject newGround = Instantiate(groundPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
        StartCoroutine(MoveGroundRoutine(newGround));
    }

    private IEnumerator MoveGroundRoutine(GameObject ground)
    {
        float realVelocity = player.velocity.x / depth;

        while (ground.transform.position.x > destroyPoint.position.x)
        {
            ground.transform.Translate(Vector2.left * realVelocity * Time.deltaTime);
            yield return null;
        }
        Destroy(ground);
    }
}