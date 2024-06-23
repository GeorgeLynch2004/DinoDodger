using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{   
    [SerializeField] private bool isRunning;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] points;
    [SerializeField] private int currentLevelIndex;
    [SerializeField] private float[] maxLevelScores;
    [SerializeField] private bool gameStarted;
    [SerializeField] private GameObject UIManager;

    private void Start() 
    {
        points = GameObject.FindGameObjectsWithTag("Point");
        DontDestroyOnLoad(gameObject);
        Instantiate(player, spawnPoint.position, quaternion.identity);
        isRunning = false;
        gameStarted = false;
    }

    private void Update() 
    {
        checkPoints();
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        player = GameObject.FindGameObjectWithTag("Player");
        float points = GetPlayerObject().GetComponent<PointCollector>().getPoints();
        UIManager = GameObject.Find("UIManager");

        CheckForPause();

    }

    private void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameStarted = !gameStarted;
            isRunning = true;
        }

        if (isRunning)
        {
            if (!gameStarted)
            {
                isRunning = false;
            }
        }
    }

    private void checkPoints()
    {
        if (GameObject.FindGameObjectsWithTag("Point").Length == 0)
        {
            StartCoroutine(resetlevel());
        }

        // if max points in the level is reached then the next level is loaded
        if (player.GetComponent<PointCollector>().getPoints() >= maxLevelScores[SceneManager.GetActiveScene().buildIndex])
        {
            // try progressing to next level
            try
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            catch (Exception ex)
            {
                // if there is no next level catch and restart the game
                SceneManager.LoadScene("SampleScene");
            }
            Destroy(gameObject);
            Destroy(UIManager.gameObject);
        }
    }

    private IEnumerator resetlevel()
    {
        Debug.Log("Maximum Points Collected");
        isRunning = false;

        // Reset the player position
        player.transform.position = spawnPoint.position;

        yield return new WaitForSeconds(2);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }   

        GameObject[] items = GameObject.FindGameObjectsWithTag("item");
        foreach (GameObject item in items)
        {
            Destroy(item);
        }

        yield return new WaitForSeconds(1);

        // Spawn all the points back
        foreach (GameObject point in points)
        {
            point.SetActive(true);
        }

        yield return new WaitForSeconds(1);  

        isRunning = true;
    }

    public bool GameRunning(){return isRunning;}

    public bool GameStarted(){return gameStarted;}

    public GameObject GetPlayerObject()
    {
        return player;
    }

    public IEnumerator PlayerDied()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameStarted = false;
        Instantiate(player, spawnPoint.position, quaternion.identity);
        Destroy(gameObject);
        Destroy(UIManager.gameObject);
    }

    public float GetLevelMaxScore()
    {
        return maxLevelScores[SceneManager.GetActiveScene().buildIndex];
    }
}
