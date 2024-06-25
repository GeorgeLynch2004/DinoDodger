using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI startButton;
    [SerializeField] private TextMeshProUGUI controlsButton;
    [SerializeField] private Animator animator;
    [SerializeField] private bool onMainPage;
    [SerializeField] private GameObject[] controlsGraphics;
    private float timer;

    private void Start()
    {
        onMainPage = true;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timer < .5f)
        {
            title.text = "DINO DODGER 3000";
        }
        else if (timer > .5f && timer < 1)
        {
            title.text = "";
        }
        else
        {
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ToggleBetweenControlsScreen()
    {
        if (onMainPage)
        {
            foreach (GameObject graphic in controlsGraphics)
            {
                graphic.SetActive(!graphic.activeInHierarchy);
            }
        }
        else
        {
            foreach (GameObject graphic in controlsGraphics)
            {
                graphic.SetActive(!graphic.activeInHierarchy);
            }
        }
        onMainPage = !onMainPage;
    }
}
