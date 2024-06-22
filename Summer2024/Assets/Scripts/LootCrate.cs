using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCrate : MonoBehaviour
{
    [SerializeField] private GameObject crateContents;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(crateContents, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
