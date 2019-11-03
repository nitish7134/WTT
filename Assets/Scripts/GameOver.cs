using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI gameOverScore;
    public TextMeshProUGUI highscoreText;

    private void Start()
    {
        gameOverScore.text = "Score: " + gameManager.score;

        if (PlayerPrefs.GetInt("HighScore") < gameManager.score)
            PlayerPrefs.SetInt("HighScore", gameManager.score);

        highscoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }
}
