using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerHandler : MonoBehaviour
{
    
   
    public GameManager gameManager;
    public TextMeshProUGUI healthText;
    public int health;
    public GameObject gameOverScreen;
    public GameObject inGameScreen;


    void Start(){
        health = PlayerPrefs.GetInt("Health",100);
    }

    void Update(){
        healthText.text = health.ToString();
        if(health <= 0){
            gameOverScreen.SetActive(true);
            inGameScreen.SetActive(false);
        }
    }

    public void GiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameManager.isPlayerDead = true;
        }
    }
}
