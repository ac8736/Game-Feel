using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static float SpawnInterval = 0.5f;
    public static bool GameOver = false;

    public static float MinSpawnInterval = 0.1f;
    public static float SpawnIntervalChangeRate = -0.01f;
    public static GameController self;

    public PlayerController Player;
    public Collidable ObstaclePrefab;
    public CameraController CameraController;

    private float spawnTimer = 0;
    private float gameOverDelay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnInterval = 0.5f;
        GameOver = false;
        self = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver) 
        {
            gameOverDelay -= Time.deltaTime;
            // To avoid accidental restart upon death
            if (Input.GetKeyUp(KeyCode.Space) && gameOverDelay <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }

        if (Player.IsDead) {
            GameOver = true;
            gameOverDelay = 0.5f;
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer > SpawnInterval)
        {
            Collidable prefabToSpawn = ObstaclePrefab;
            Instantiate(prefabToSpawn, (Vector2)(Player.transform.position) + Random.insideUnitCircle * 12, Quaternion.identity);
            spawnTimer = 0;
        }

        SpawnInterval += SpawnIntervalChangeRate * Time.deltaTime;
    }
}
