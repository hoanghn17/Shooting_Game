using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    [SerializeField]
    private Texture2D cursorTexture;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI gameOverText;

    public TextMeshProUGUI timeText;

    public TextMeshProUGUI liveText;

    public Button restartButton;

    public Button menuButton;

    public GameObject titleScreen;

    private float spawnRate = 1f;

    private int score;

    public bool isGameActive;

    public float time = 100;

    public float lives;

    public int ammo;

    // Update is called once per frame
    void Update()
    {
        CountDownTime();
    }

    //Create a coroutine to spawn objects
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0,targets.Count);
            Instantiate(targets[index]);
        }
    }

    // Start Game
    public void StartGame(int difficulty)
    {
        var cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
        
        isGameActive = true;
        scoreText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        liveText.gameObject.SetActive(true);

        score = 0;
        lives = 100;

        liveText.text = " " + lives;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        
        
        // When game start title screen off
        titleScreen.gameObject.SetActive(false);

        // In 1 level have diffult time to spawn enemy
        spawnRate /= difficulty;
    }

    //Create a new UpdateScore method
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = " " + score;
    }

    // Game over
    public void GameOver()
    {
        var cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(null, cursorHotspot, CursorMode.Auto);

        gameOverText.gameObject.SetActive(true); // If Gameover show text
        isGameActive = false;
        restartButton.gameObject.SetActive(true); // If Gameover show button
    }

    // Restart gamne
    public void RestartGame()
    {
        // Load scene when game end
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // CountDownTime
    public void CountDownTime()
    {
        if (isGameActive)
        {
            time -= Time.deltaTime;
            timeText.text = " " + Mathf.Round(time);

            if (time < 0)
            {
                GameOver();
            }
        }
    }
    // Lives
    public void Lives(int liveToChange)
    {
        lives += liveToChange;
        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }
        liveText.text = " " + lives;
    }
}
