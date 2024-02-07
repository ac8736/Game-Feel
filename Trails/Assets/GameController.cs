using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool GameOver = false;

    public static float SpawnInterval = 0.5f;
    public static float MinSpawnInterval = 0.1f;
    public static float SpawnIntervalChangeRate = -0.01f;
    public static int SpawnLimit = 30;
    public static GameController self;
    public static float HighScore = 0;
    
    public PlayerController Player;
    public Collidable ObstaclePrefab;
    public CameraController CameraController;
    public int Score = 0;
    public Animator GameOverGUI;
    public Animator FlashGUI;

    private float spawnTimer = 0;
    private float gameOverDelay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnInterval = 0.5f;
        GameOver = false;
        self = this;
        Time.timeScale = 1f;
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
            if (Score > HighScore) HighScore = Score;
            gameOverDelay = 0.5f;
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer > SpawnInterval && GameObject.FindGameObjectsWithTag("Rock").Length < SpawnLimit)
        {
            Collidable prefabToSpawn = ObstaclePrefab;
            Vector2 dir = Random.insideUnitCircle.normalized;
            Instantiate(prefabToSpawn, (Vector2)(Player.transform.position) + dir * Random.Range(5f, 12f), Quaternion.identity);
            spawnTimer = 0;
        }

        SpawnInterval += SpawnIntervalChangeRate * Time.deltaTime;
    }
}
