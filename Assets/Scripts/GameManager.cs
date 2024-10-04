using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    public GameObject titleScreen;

    /* ---- LIVES ---- */
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public TextMeshProUGUI livesText;
    public int totalLives;
    private int numLives;

    /* ---- TEXT ---- */
    public TextMeshProUGUI scoreText;
    private int score;

    private float spawnRate = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        // everything moved to StartGame()
    }

    public void StartGame(int difficulty)
    {
        /* ---- LIVES ---- */
        numLives = totalLives;
        UpdateLives(0);

        /* ---- TEXT ---- */
        score = 0;
        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
    }


    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnTarget()
    {
        while (!isGameOver())
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    /* ---- TEXT ---- */
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public bool isGameOver()
    {
        return numLives <= 0;
    }
    public void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /* ---- LIVES ---- */
    public void UpdateLives(int livesToAdd)
    {
        numLives += livesToAdd;
        livesText.text = "Lives: " + numLives;
    }
}
