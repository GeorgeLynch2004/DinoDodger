using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_SpeedVariationRange;
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
    private bool footstepAvailable;
    

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        healthSystem = GetComponent<HealthSystem>();
        m_MaxSpeedX = Random.Range(m_MaxSpeedX - m_SpeedVariationRange, m_Speed + m_SpeedVariationRange);
        footstepAvailable = true;
    }


    void FixedUpdate()
    {
        if (gameManager != null)
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
        }
        

        m_IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (healthSystem != null && healthSystem.getIsAlive() == false)
        {
            m_IsGrounded = false;
        }

        Vector2 velocity = m_Rigidbody2D.velocity;

        if (movementPermitted)
        {
            if (Input.GetKey(KeyCode.UpArrow) && m_IsGrounded)
            {
                velocity += (Vector2.up * m_JumpForce * Time.deltaTime);
                m_LastDirection = Vector2.up;
                SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
                soundManager.PlaySound("Jump");
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                velocity -= (Vector2.up * m_Speed * Time.deltaTime);
                m_LastDirection = -Vector2.up;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                velocity -= (Vector2.right * m_Speed * Time.deltaTime);
                m_LastDirection = -Vector2.right;
                if (footstepAvailable && m_IsGrounded) StartCoroutine(footstep());
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                velocity += (Vector2.right * m_Speed * Time.deltaTime);
                m_LastDirection = Vector2.right;
                if (footstepAvailable && m_IsGrounded) StartCoroutine(footstep());
            }
            updateTransform(m_LastDirection);
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
            updateTransform(enemyComponent.GetDirectionToMove());
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

    public void updateTransform(Vector2 direction)
    {
        Vector3 rot = transform.localEulerAngles;

        if (direction == Vector2.right)
        {
            rot.y = 0;
        }
        if (direction == -Vector2.right)
        {
            rot.y = -180;
        }

        transform.localEulerAngles = rot;
    }

    public bool GetIsGrounded()
    {
        return m_IsGrounded;
    }

    private IEnumerator footstep()
    {
        footstepAvailable = false;
        SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.PlaySound("Footstep");
        yield return new WaitForSeconds(.18f);
        footstepAvailable = true;
    }

}
