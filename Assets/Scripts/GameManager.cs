using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject player;
    public GameObject firstPortal;
    public GameObject secondPortal;
    public int score=0;
    public float spawnWaitTime;
    public float enemyRadius;
    public int enemyKillCount;
    public int enemyKillThreshold;
    public int killThreshold = 10;
    public bool isPlayerDead = false;
    public TextMeshProUGUI scoreText;
    public PlayerHandler playerHandler;
    public TextMeshProUGUI healthText;
   
    public GameObject gameOverScreen;
    public GameObject inGameScreen;

    
    public Transform[] spawnPoint;
    public Transform[] portalOneSpawnPoint;
    public Transform[] portalTwoSpawnPoint;

    private bool isPortalOpen = false, isBoosted = false;

    void Start()
    {
        score = PlayerPrefs.GetInt("CurrentScore", 0);
        InvokeRepeating("SpawnEnemy", 1f, spawnWaitTime);
    }
    
    public void EnableMovement() //Function to start Movement only when round starts.
    {
        player.GetComponent<CameraController>().canMove = true;
    }


    public void SpawnEnemy()
    {
        if (isPlayerDead)
            return;

        int enemyIndex = Random.Range(0, enemies.Length - 1);
        int spawnPointIndex = Random.Range(0, spawnPoint.Length - 1);
        if (Physics.OverlapSphere(spawnPoint[spawnPointIndex].position, enemyRadius, (int)(~(1 << 2))).Length == 0)
        {
            Instantiate(enemies[enemyIndex], spawnPoint[spawnPointIndex]);
        }
        else
        {
            SpawnEnemy();
        }
    }

    public void EnemyDown()
    {
        enemyKillCount++;
        if(isBoosted){
            isBoosted = false;
        }
        if(isPortalOpen){
            isPortalOpen = false;
        }
        score++;
        scoreText.text = "Score: " + score;
    }

    private void Update()
    {
        if(enemyKillCount % killThreshold == 0 && enemyKillCount != 0 && isBoosted == false)
        {
            isBoosted = true;
            playerHandler.health += 10;
            playerHandler.health = Mathf.Min(playerHandler.health, 100);
        }
        if(enemyKillCount % enemyKillThreshold == 0 && enemyKillCount != 0 && isPortalOpen == false)
        {
            //enemyKillCount = 0;
            isPortalOpen = true;
            for (int i = 0; i < portalOneSpawnPoint.Length; i++)
                Instantiate(firstPortal, portalOneSpawnPoint[i]);
            for (int i = 0; i < portalTwoSpawnPoint.Length; i++)
                Instantiate(secondPortal, portalTwoSpawnPoint[i]);
        }

        healthText.text = playerHandler.health.ToString();

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
