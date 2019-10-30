using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0; 

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    // The very 1st time this script wakes up.. it does what's inside the method
    public void Awake()
    {
        // Integer that gets the number of game sessions
        int numGameSession = FindObjectsOfType<GameSession>().Length;

        // If there is more than 1 game session than destroy this 1 we just created
        if (numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Whatever we have as a number in our lives field should be converted to a string
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    // Method that handles when the player's lives
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    // method that reduces the nunber of player lives by 1
    private void TakeLife()
    {
        playerLives--;
        // Reload the current scene
        //:var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //:SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        // We need to destroy the instance we created
        Destroy(gameObject);
    }
}
