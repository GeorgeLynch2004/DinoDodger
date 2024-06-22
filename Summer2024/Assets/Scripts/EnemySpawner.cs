using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnableObjects;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private bool canSpawn;
    private GameManager gameManager;

    private void Start()
    {
        canSpawn = true;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (!gameManager.GameRunning()) return;
        if (canSpawn)
        {StartCoroutine(spawnObject());}
    }

    private IEnumerator spawnObject()
    {
        if (gameManager.GameRunning())
        {
            canSpawn = false;

            yield return new WaitForSeconds(timeBetweenSpawns);

            int randomIndex = Random.Range(0, spawnableObjects.Count-1);

            Instantiate(spawnableObjects[randomIndex], transform.position, transform.rotation);

            canSpawn = true;
        }
    }

    public void SetTimeBetweenSpawns(float time)
    {
        timeBetweenSpawns = time;
    }
}
