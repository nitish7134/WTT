using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
  //  public GameObject spawnPoints;
   // public GameObject portalOneSpawnPoints;
   // public GameObject portalTwoSpawnPoints;
    public GameObject[] enemies;
    public GameObject player;
    public GameObject firstPortal;
    public GameObject secondPortal;
    public int score=0;
    public float spawnWaitTime;
    public float startWaitTime;
    public float enemyRadius;
    public int enemyKillCount;
    public int enemyKillThreshold;
    public int killThreshold = 10;
    public bool isPlayerDead = false;
    public TextMeshProUGUI scoreText;
    public PlayerHandler Handler;
    public TextMeshProUGUI healthText;
   
    public GameObject gameOverScreen;
    public GameObject inGameScreen;

    private WaitForSeconds spawnWait;
    public Transform[] spawnPoint;
    public Transform[] portalOneSpawnPoint;
    public Transform[] portalTwoSpawnPoint;

    void Start()
    {
        //spawnPoint = spawnPoints.GetComponentsInChildren<Transform>();
       // portalOneSpawnPoint = portalOneSpawnPoints.GetComponentsInChildren<Transform>();
       // portalTwoSpawnPoint = portalTwoSpawnPoints.GetComponentsInChildren<Transform>();
        inititate();
        score = PlayerPrefs.GetInt("CurrentScore",0);
        InvokeRepeating("SpawnEnemy", 1f, spawnWaitTime);
    }

    IEnumerator inititate()
    {
        //TODO
        //Start for scene add Animations or visuals before starting the game
        yield return new WaitForSecondsRealtime(startWaitTime);
        enableMovement();
    }
    

    public void enableMovement() //Function to start Movement only when round starts.
    {
        player.GetComponent<CameraController>().canMove = true;
    }


    public void SpawnEnemy()
    {
        if (!isPlayerDead)
        {
            int enemyIndex = Random.Range(0, enemies.Length - 1);
            int spawnPointIndex = Random.Range(0, spawnPoint.Length - 1);
            if (Physics.OverlapSphere(spawnPoint[spawnPointIndex].position, enemyRadius, (int)(~(1 << 2))).Length == 0) //LayerMask 2 is ignored for environment and rest is to be checked
            {
                Instantiate(enemies[enemyIndex], spawnPoint[spawnPointIndex]);
            }
            else
            {
                SpawnEnemy();
            }
        }
    }

    public void enemyDown()
    {
        enemyKillCount += 1;
        score++;
        scoreText.text = "Score: " + score;
    }

    private void Update()
    {
        if(enemyKillCount % killThreshold == 0 && enemyKillCount != 0){
            if(Handler.health <= 90)
                Handler.health += 10;
            else{
                Handler.health = 100;
            }
        }
        if(enemyKillCount % enemyKillThreshold == 0 && enemyKillCount != 0)
        {
            enemyKillCount = 0;
            for (int i = 0; i < portalOneSpawnPoint.Length; i++)
                Instantiate(firstPortal, portalOneSpawnPoint[i]);
            for (int i = 0; i < portalTwoSpawnPoint.Length; i++)
                Instantiate(secondPortal, portalTwoSpawnPoint[i]);
        }

        healthText.text = Handler.health.ToString();

        if(Handler.health <= 0){
            isPlayerDead = true;
            gameOverScreen.SetActive(true);
            inGameScreen.SetActive(false);
        }

    }


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    private void PauseGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

}
