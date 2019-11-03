using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHandler : MonoBehaviour
{   
    public GameManager gameManager;
    public TextMeshProUGUI healthText;
    public GameObject gameOverScreen;
    public GameObject inGameScreen;
    [HideInInspector] public int health;

    private void Start()
    {
        health =  PlayerPrefs.GetInt("Health", 100);
    }

    public void GiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameManager.isPlayerDead = true;
            gameOverScreen.SetActive(true);
            inGameScreen.SetActive(false);
        }
        healthText.text = health.ToString();
    }
}
