using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class portalManager : MonoBehaviour
{
    public int portalIndex;

    private SceneTransition sceneTransition;

    void Start()
    {
        sceneTransition = FindObjectOfType<SceneTransition>();
        StartCoroutine("Wait"); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerPrefs.SetInt("Health", GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>().health);
            PlayerPrefs.SetInt("CurrentScore", GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().score);
            sceneTransition.InitiateTransition();
            StartCoroutine(LoadScene(portalIndex + 1));
        }
    }

    IEnumerator Wait(){
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneIndex);
    }
}
