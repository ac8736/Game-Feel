using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;

    void Start()
    {

    }

    public void SpawnObject()
    {

        // Check if there's already an object at the spawn point

        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
