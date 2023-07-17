using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundPopUpUI : PopUpUI
{
    Slider masterSlider;
    Slider bgmSlider;
    Slider sfxSlider; 
    float currentSoundVolume; 
    protected override void Awake()
    {
        base.Awake();
        masterSlider = sliders["Buttons_Master_Slider"]; 
        masterSlider.maxValue = 20;
        masterSlider.minValue = -80;
        masterSlider.value = GameManager.AudioManager.CurrentMasterVolume;
        masterSlider.onValueChanged.AddListener(ChangeMasterVolume);
        bgmSlider = sliders["Buttons_BGM_Slider"];
        bgmSlider.maxValue = 20;
        bgmSlider.minValue = -80;
        bgmSlider.value = GameManager.AudioManager.CurrentMasterVolume;
        bgmSlider.onValueChanged.AddListener(ChangeBGMVolume);
        sfxSlider = sliders["Buttons_SFX_Slider"];
        sfxSlider.maxValue = 20;
        sfxSlider.minValue = -80;
        sfxSlider.value = GameManager.AudioManager.CurrentMasterVolume;
        sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);
        buttons["Buttons_Return"].onClick.AddListener(CloseThisUI); 
    }
    private void ChangeMasterVolume(float value)
    {
        GameManager.AudioManager.SetMasterVolume(value); 
    }

    private void ChangeSFXVolume(float value)
    {
        GameManager.AudioManager.SetSFXVolume(value);
    }

    private void ChangeBGMVolume(float value)
    {
        GameManager.AudioManager.SetBGMVolume(value);
    }

    private void CloseThisUI()
    {
        GameManager.UIManager.ClosePopUpUI(); 
    }
}
