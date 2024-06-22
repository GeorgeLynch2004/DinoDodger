using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_MaxSpeedX;
    [SerializeField] private float m_MaxSpeedY;
    [SerializeField] private float m_JumpForce;
    [SerializeField] private Vector2 m_LastDirection;
    [SerializeField] private bool m_IsGrounded;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private float groundCheckRadius = 0.2f; 
    private GameManager gameManager;
    private HealthSystem healthSystem;
    [SerializeField] private bool movementPermitted;
    

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        healthSystem = GetComponent<HealthSystem>();
    }


    void FixedUpdate()
    {
        if (!gameManager.GameRunning()) 
        {
            m_Rigidbody2D.gravityScale = 0; 
            return;
        }
        else 
        {
            m_Rigidbody2D.gravityScale = 1;
        }

        updateTransform();

        m_IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (healthSystem != null && healthSystem.getIsAlive() == false)
        {
            m_IsGrounded = false;
        }

        Vector2 velocity = m_Rigidbody2D.velocity;

        if (movementPermitted)
        {
            if (Input.GetKey(KeyCode.W) && m_IsGrounded)
            {
                velocity += (Vector2.up * m_JumpForce * Time.deltaTime);
                m_LastDirection = Vector2.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                velocity -= (Vector2.up * m_Speed * Time.deltaTime);
                m_LastDirection = -Vector2.up;
            }
            if (Input.GetKey(KeyCode.A))
            {
                velocity -= (Vector2.right * m_Speed * Time.deltaTime);
                m_LastDirection = -Vector2.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                velocity += (Vector2.right * m_Speed * Time.deltaTime);
                m_LastDirection = Vector2.right;
            }
        }
        else
        {
            Enemy enemyComponent = GetComponent<Enemy>();

            // shoot raycast to check if the enemy collides with a wall
            RaycastHit2D hit = Physics2D.Raycast(transform.position, enemyComponent.GetDirectionToMove(), (transform.localScale.x / 2 + .2f), groundLayer);
            Debug.DrawRay(transform.position, enemyComponent.GetDirectionToMove(), Color.red);
            if (hit.collider != null)
            {
                enemyComponent.SetDirectionToMove(-enemyComponent.GetDirectionToMove());
            }
            velocity += (enemyComponent.GetDirectionToMove() * m_Speed * Time.deltaTime);
        }

        velocity.x = Mathf.Clamp(velocity.x, -m_MaxSpeedX, m_MaxSpeedX);
        velocity.y = Mathf.Clamp(velocity.y, -m_MaxSpeedY, m_MaxSpeedY * 2);

        if (velocity.y < 0)
        {
            velocity.y *= 1.3f;
        }

        m_Rigidbody2D.velocity = velocity;
    }

    public Vector2 LastDirection()
    {
        return m_LastDirection;
    }

    public void updateTransform()
    {
        Vector3 rot = transform.localEulerAngles;

        if (m_LastDirection == Vector2.right)
        {
            rot.y = 0;
        }
        if (m_LastDirection == -Vector2.right)
        {
            rot.y = -180;
        }

        transform.localEulerAngles = rot;
    }

    public bool GetIsGrounded()
    {
        return m_IsGrounded;
    }

}
