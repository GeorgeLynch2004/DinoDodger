using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModifier : MonoBehaviour
{
    [SerializeField] private float modifierAmount;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") return;

        if (other.gameObject.GetComponent<HealthSystem>() != null)
        {
            other.gameObject.GetComponent<HealthSystem>().setCurrentHealth(other.gameObject.GetComponent<HealthSystem>().getCurrentHealth() + modifierAmount);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Enemy") return;
        if (other.gameObject.GetComponent<HealthSystem>() != null)
        {
            other.gameObject.GetComponent<HealthSystem>().setCurrentHealth(other.gameObject.GetComponent<HealthSystem>().getCurrentHealth() + modifierAmount);
        }
    }
}
