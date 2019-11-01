using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{

    public GameManager gameManager;
    public TextMeshProUGUI gameOverScore;
    public TextMeshProUGUI highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScore.text = "Score: " + gameManager.score;
        if (PlayerPrefs.GetInt("HighScore") < gameManager.score)
            PlayerPrefs.SetInt("HighScore", gameManager.score);
        highscoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
    }
}
