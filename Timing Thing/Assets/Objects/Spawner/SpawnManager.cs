using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    Spawner[] spawners;
    [SerializeField] private float maxCD = 1f;
    private float cd = 0f;
    // Start is called before the first frame update
    void Start()
    {
        spawners = GameObject.FindObjectsOfType<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        cd -= Time.deltaTime;
        if (cd <= 0)
        {
            cd = maxCD;
            spawners[Random.Range(0, 4)].SpawnObject();
        }
    }
}
