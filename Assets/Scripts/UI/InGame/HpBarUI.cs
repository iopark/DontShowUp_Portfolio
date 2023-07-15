using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : SceneUI
{
    private Slider slider;
    private TMP_Text text; 

    private void Awake()
    {
        base.Awake();
        slider = GetComponentInChildren<Slider>();
        text = texts["HealthBar_HealthText"]; 
    }

    private void OnEnable()
    {
        GameManager.DataManager.OnHealthChange += UpdateHpBar;
        GameManager.DataManager.OnHealthChange += UpdateHealthText; 
    }
    private void Start()
    {
        
        slider.maxValue = GameManager.DataManager.MaxHealth;
        slider.value = GameManager.DataManager.Health;
        text.text = GameManager.DataManager.Health.ToString(); 
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
