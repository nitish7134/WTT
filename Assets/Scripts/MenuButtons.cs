using TMPro;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public TextMeshProUGUI highscore;

    private SceneTransition sceneTransition;

    private void Start()
    {
        highscore.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        sceneTransition = FindObjectOfType<SceneTransition>();
    }

    public void RestartGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Health", 100);
        PlayerPrefs.SetInt("CurrentScore", 0);
        sceneTransition.InitiateTransition(1);
    }

    public void MainMenu()
    {
        sceneTransition.InitiateTransition(-1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
