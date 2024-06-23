using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private int ammo;
    [SerializeField] private string name;
    [SerializeField] private Transform barrel;
    [SerializeField] private bool canShoot;
    [SerializeField] private float timeBetweenShots;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return) && canShoot && ammo > 0 && transform.parent != null)
        {
            Instantiate(bullet, barrel.position, barrel.rotation);
            SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            soundManager.PlaySound("Gunshot");
            if (animator != null){animator.SetTrigger("Fire");}
            ammo--;
            StartCoroutine(reload());
        }    

        if (ammo == 0)
        {
            Destroy(gameObject);
        }
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public string GetName()
    {
        return name;
    }

    private IEnumerator reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }
}
