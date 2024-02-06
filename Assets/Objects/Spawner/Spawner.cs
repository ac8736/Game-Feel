using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;
    public float initialSpawnInterval = 3f;
    public float minSpawnInterval = 0.5f;
    public float intervalDecreaseRate = 0.1f;
    public float timeUntilMinInterval = 30f; // Time until the spawn interval reaches the minimum
    public float spawnCooldown = 1.5f; // Cooldown between spawns to prevent overlapping

    private float spawnInterval;
    private bool canSpawn = true;

    void Start()
    {
        spawnInterval = initialSpawnInterval;
        InvokeRepeating(nameof(SpawnObject), Random.Range(0f, spawnInterval), spawnInterval);
    }

    void SpawnObject()
    {
        if (!canSpawn)
            return;

        // Check if there's already an object at the spawn point
        Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, 0.1f);
        if (colliders.Length > 0)
            return;

        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);

        // Decrease spawn interval
        spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - intervalDecreaseRate);

        // If time until minimum interval is reached, stop decreasing interval
        if (Time.timeSinceLevelLoad >= timeUntilMinInterval)
        {
            intervalDecreaseRate = 0f;
        }

        // Start cooldown
        StartCoroutine(SpawnCooldown());
    }

    // Cooldown coroutine
    IEnumerator SpawnCooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }
}
