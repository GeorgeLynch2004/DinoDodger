using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private bool isAlive;
    [SerializeField] private bool isPlayer;
    private GameManager gameManager;

    private void Start()
    {
        currentHealth = maxHealth;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() 
    {
        isAlive = currentHealth > 0;

        if (!isAlive)
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
            setColour(Color.gray);
            if (isPlayer){StartCoroutine(gameManager.PlayerDied());}
            
        }
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public void setMaxHealth(float health)
    {
        maxHealth = health;
    }

    public void setCurrentHealth(float health)
    {
        currentHealth = health;
    }

    private void setColour(Color color)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }

    public bool getIsAlive() 
    {
        return isAlive;
    }

    public void instaKill()
    {
        currentHealth = 0;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        setColour(Color.blue);
    }
}
