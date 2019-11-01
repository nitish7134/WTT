using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public TextMeshProUGUI highscore;

    private SceneTransition sceneTransition;

    private void Start()
    {
        highscore.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");
        sceneTransition = FindObjectOfType<SceneTransition>();
    }

    public void ExitToMenu()
    {
        sceneTransition.InitiateTransition();
        StartCoroutine(LoadScene(0));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Helth",100);
        PlayerPrefs.SetInt("CurrentScore",0);

        sceneTransition.InitiateTransition();
        StartCoroutine(LoadScene(2));
        
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneIndex);
    }

}
