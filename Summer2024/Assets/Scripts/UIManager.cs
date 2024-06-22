using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highscore;
    [SerializeField] private TextMeshProUGUI playerHealth;
    [SerializeField] private TextMeshProUGUI gunName;
    [SerializeField] private TextMeshProUGUI ammo;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject menuUI;
    private GameManager gameManager;

    private void Start() {
        DontDestroyOnLoad(gameObject);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() 
    {
        float points = gameManager.GetPlayerObject().GetComponent<PointCollector>().getPoints();
        highscore.text = ConvertFloat(points, 4) + "/" + ConvertFloat(gameManager.GetLevelMaxScore(), 4);
        playerHealth.text = GetPlayerHealth();
        gunName.text = GetGunData()[0];
        ammo.text = GetGunData()[1];
        ToggleCanvas();
    }

    private string[] GetGunData()
    {
        string[] data = new string[2];
        string name = "";
        string ammo = "";
        
        if (gameManager.GetPlayerObject().GetComponent<ItemHandler>().handFull)
        {
            name = gameManager.GetPlayerObject().GetComponent<ItemHandler>().GetItemInHand().GetComponent<Gun>().GetName();
            int ammo1 = gameManager.GetPlayerObject().GetComponent<ItemHandler>().GetItemInHand().GetComponent<Gun>().GetAmmo();
            ammo = ConvertFloat((float)ammo1, 4);
        }
        else
        {
            name = "";
            ammo = "";
        }

        data[0] = name;
        data[1] = ammo;

        return data;
    }

    public string ConvertFloat(float number, int totalDigits)
    {
        // Convert float to string without scientific notation
        string floatStr = number.ToString();

        // Remove the decimal point and count the digits
        int digitCount = floatStr.Replace(".", "").Length;

        // Calculate the number of zeros to add
        int zerosToAdd = totalDigits - digitCount;

        // Create a string of zeros
        string zeros = new string('0', zerosToAdd > 0 ? zerosToAdd : 0);

        // Combine zeros and the float string
        string result = zeros + floatStr;

        return result;
    }

    private string GetPlayerHealth()
    {
        if (gameManager.GetPlayerObject().GetComponent<HealthSystem>() != null)
        {
            HealthSystem healthSystem = gameManager.GetPlayerObject().GetComponent<HealthSystem>();
            return ConvertFloat(healthSystem.getCurrentHealth(), 4);
        }
        else return "0000";
    }

    private void ToggleCanvas()
    {
        if (gameManager.GameStarted())
        {
            gameUI.SetActive(true);
            menuUI.SetActive(false);
        }
        else
        {
            gameUI.SetActive(false);
            menuUI.SetActive(true);
        }
    }
}
