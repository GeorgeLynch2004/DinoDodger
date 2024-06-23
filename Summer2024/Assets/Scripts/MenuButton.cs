using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlayButton : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private Button pb;
    private TextMeshProUGUI text;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color changedColor;
    private SoundManager soundManager;

    void Start()
    {
        pb = GetComponent<Button>();
        text = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        defaultColor = text.color;
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = changedColor;
        soundManager.PlaySound("Button Sound");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = defaultColor;
    }
}