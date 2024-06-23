using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private float health;
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) && transform.parent != null)
        {
            HealthSystem healthSystem = transform.parent.parent.GetComponent<HealthSystem>();
            SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            soundManager.PlaySound("Heal");
            healthSystem.setCurrentHealth(healthSystem.getCurrentHealth() + health);
            Destroy(gameObject);
        }
    }
}
