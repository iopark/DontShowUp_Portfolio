using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    private Slider slider;
    private TextMeshPro text; 

    private void Awake()
    {
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<TextMeshPro>();
    }

    private void OnEnable()
    {
        GameManager.DataManager.OnHealthChange += UpdateHpBar;
    }
    private void Start()
    {
        
        slider.maxValue = GameManager.DataManager.MaxHealth;
        slider.value = GameManager.DataManager.Health; 
        slider.minValue = 0;
    }

    private void UpdateHpBar(int value)
    {
        slider.value = value;
    }
    
    private void UpdateHealthText(int value)
    {
        text.text = value.ToString();
    }
}
