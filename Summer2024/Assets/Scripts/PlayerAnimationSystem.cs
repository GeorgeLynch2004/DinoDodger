using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationSystem : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private HealthSystem healthSystem;
    private Animator playerAnimator;
    private Rigidbody2D rb;
    private float prevFrameHealth;
    [SerializeField] private bool changeStates;

    private void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();
        prevFrameHealth = healthSystem.getCurrentHealth();
        changeStates = true;
    }

    private void Update()
    {
        // check for every possible player state
        if (changeStates)
        {
            States();
        }
        else
        {
            playerAnimator.SetTrigger("Hurt");
        }
        prevFrameHealth = healthSystem.getCurrentHealth();
    }

    private void States()
    {
        if (healthSystem.getCurrentHealth() != prevFrameHealth)
        {
            StartCoroutine(StateChangeTimeout());
        }
        // Idle
        else if (rb.velocity.magnitude < .5f)
        {
            playerAnimator.SetTrigger("Idle");
        }
        // Running
        else if (rb.velocity.magnitude > .5f && playerMovement.GetIsGrounded())
        {
            playerAnimator.SetTrigger("Running");
        }
        // Jumping
        else if (rb.velocity.magnitude > .5f && rb.velocity.y > 0 && !playerMovement.GetIsGrounded())
        {
            playerAnimator.SetTrigger("Jumping");
        }
        // Falling
        else if (rb.velocity.magnitude > .5f && rb.velocity.y < 0 && !playerMovement.GetIsGrounded())
        {
            playerAnimator.SetTrigger("Falling");
        }
    }

    public IEnumerator StateChangeTimeout()
    {
        changeStates = false;
        yield return new WaitForSeconds(.5f);
        changeStates = true;
    }
}
