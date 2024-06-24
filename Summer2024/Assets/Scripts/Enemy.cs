using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Vector2 directionToMove;
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool movementOverrided;

    private void establishDirectionToMove()
    {
        Vector3 scale = transform.localScale;

        if (transform.position.x < 0)
        {
            directionToMove = Vector2.right;
        }
        else
        {
            directionToMove = -Vector2.right;
        }

        transform.localScale = scale;
    }

    private void Start()
    {
        establishDirectionToMove();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (movementOverrided)
        {
            SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            soundManager.PlaySound("Roar");    
        }
        
    }

    private void Update()
    {
        if (!gameManager.GameRunning() || movementOverrided) return;

        Vector2 position = transform.position;

        position += directionToMove * movementSpeed * Time.deltaTime;

        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
            if (GetComponent<HealthSystem>() != null)
            {
                GetComponent<HealthSystem>().instaKill();
            }
        }
    }

    public Vector2 GetDirectionToMove()
    {
        return directionToMove;
    }

    public void SetDirectionToMove(Vector2 direction)
    {
        directionToMove = direction;
    }
}
