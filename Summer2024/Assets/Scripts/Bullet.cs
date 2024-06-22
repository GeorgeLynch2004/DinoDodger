using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    void Start()
    {
        StartCoroutine(destroyCountDown());
    }

    void Update()
    {
        Vector3 pos = transform.position;

        pos += transform.right * speed * Time.deltaTime;

        transform.position = pos;
    }


    private IEnumerator destroyCountDown()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}


