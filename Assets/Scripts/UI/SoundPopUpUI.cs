using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundPopUpUI : PopUpUI
{
    Slider soundSlider;
    float currentSoundVolume; 
    protected override void Awake()
    {
        base.Awake();
        soundSlider = sliders["Buttons_Slider"]; 
        soundSlider.maxValue = 20;
        soundSlider.minValue = -80;
        soundSlider.value = GameManager.AudioManager.CurrentMasterVolume;
        soundSlider.onValueChanged.AddListener(ChangeMasterVolume);
        buttons["Buttons_Return"].onClick.AddListener(CloseThisUI); 
    }
    private void ChangeMasterVolume(float value)
    {
        GameManager.AudioManager.SetMasterVolume(value); 
    }

    private void ChangeSFXVolume(float value)
    {

    }

    private void ChangeBGMVolume(float value)
    {

    }

    private void CloseThisUI()
    {
        GameManager.UIManager.ClosePopUpUI(); 
    }
}
