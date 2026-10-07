using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Make it easier for other scripts to call this
    public static GameManager instance;

    public GameObject asteroidPrefab;
    public GameObject player;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text livesText;
    public GameObject gameOverText;

    public int lives = 3;
    int score = 0;
    int highScore = 0;
    bool dead = false;
    float respawnTimer = 0;
    bool gameOver = false;

    float asteroidSpawnTimer = 0;
    

    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateText();
        for (int i = 0; i < 4; i++)
        {
            SpawnAsteroid();
        }
        asteroidSpawnTimer = 3;
    }

    // Update is called once per frame
    void Update()
    {
        // Respawn player after interval
        if (dead && !gameOver)
        {
            respawnTimer = respawnTimer - Time.deltaTime;
            if (respawnTimer <= 0)
            {
                Respawn();
            }
        }

        // Spawn asteroids regularly
        if (gameOver == false)
        {
            asteroidSpawnTimer = asteroidSpawnTimer - Time.deltaTime;
            if (asteroidSpawnTimer <= 0)
            {
                GameObject[] rocks = GameObject.FindGameObjectsWithTag("Asteroid");
                // Make sure there's not too many asteroids on screen
                if (rocks.Length < 12)
                {
                    SpawnAsteroid();
                }
                asteroidSpawnTimer = 3;
            }
        }

        // Let user restart when game is over by pressing R
        if (gameOver && Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void UpdateText()
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
        livesText.text = "x " + lives;
    }

    public void AddScore(int addition)
    {
        score += addition;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        UpdateText();
    }

    void SpawnAsteroid()
    {
        // Choose random side of screen
        int side = Random.Range(0, 4);
        float x = 0;
        float y = 0;

        // Left side
        if (side == 0) {
            x = -9f;
            y = Random.Range(-5f, 5f);
        }
        // Right side
        else if (side == 1) {
            x = 9f;
            y = Random.Range(-5f, 5f);
        }
        // Top side
        else if (side == 2) {
            x = Random.Range(-9f, 9f);
            y = 5f;
        }
        // Bottom side
        else {
            x = Random.Range(-9f, 9f);
            y = -5f;
        }

        Instantiate(asteroidPrefab, new Vector2(x, y), Quaternion.identity);
    }

    public void AddLife()
    {
        lives += 1;
        UpdateText();
    }

    public void Death()
    {
        lives -= 1;
        UpdateText();
        player.SetActive(false);
        dead = true;
        respawnTimer = 2;
        if (lives <= 0)
        {
            gameOver = true;
            gameOverText.SetActive(true);
        }
    }

    public void Respawn()
    {
        player.transform.position = Vector2.zero;
        player.transform.rotation = Quaternion.identity;
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        player.GetComponent<PlayerControl>().Invincify();
        player.SetActive(true);
        dead = false;
    }
}
