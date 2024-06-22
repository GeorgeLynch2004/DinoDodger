using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private float timer;


    void Update()
    {
        if (timer < 1)
        {
            startText.text = "- PRESS SPACE TO START -";
        }
        else if (timer > 1 && timer < 2)
        {
            startText.text = "";
        }
        else if (timer > 2)
        {
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
