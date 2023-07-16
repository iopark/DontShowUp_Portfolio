using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public enum Soundtype
    {
        BGM, 
        SFX,
        Size
    }
    Sound mainThemeSong; 
    public AudioMixer audioMixer;
    AudioSource bgmSource;
    AudioSource sfxSource; 
    AudioMixerGroup[] audioMixerGroup;
    HashSet<Sound> audioList = new HashSet<Sound>();

    public float CurrentMasterVolume
    {
        get
        {
            float volume; 
            audioMixer.GetFloat("Master", out volume);
            return volume;
        }
    }

    private void Awake()
    {
        audioMixer = Resources.Load<AudioMixer>("Sound/GameMasterMixer");
        audioMixerGroup = audioMixer.FindMatchingGroups("Master");
        bgmSource = this.AddComponent<AudioSource>();
        sfxSource = this.AddComponent<AudioSource>();
        GameManager.Instance.GameSetup += ResetAllMusic; 
        SetMasterVolume(-10f);
        AudioClip themeBGM = Resources.Load<AudioClip>("Sound/IntroductionBGM");
        mainThemeSong = new Sound("IntroductionBGM", Soundtype.BGM, themeBGM);
        PlayBGM(mainThemeSong); 
    }

    public void PlayEffect(Sound sound)
    {
        //if (!audioList.Contains(sound))
        AudioClip sfx; 
        if (!audioList.Contains(sound)) { }
        sfxSource.PlayOneShot(sound.audioClip);
    }
    private void RegisterSound(Sound sound)
    {
        string audioKey = $"Sound/{sound.soundName}"; 
        AudioClip registeringClip = Resources.Load<AudioClip>(audioKey);
        Sound registeringSound = new Sound(sound.soundName, sound.soundtype, registeringClip);
        audioList.Add(registeringSound);
    }
    
    public void PlayBGM(Sound sound)
    {
        AudioClip bgm;
    }

    public void PlaySound(Sound sound)
    {
        switch (sound.soundtype)
        {
            case Soundtype.BGM:
                PlayBGM(sound); break;
            case Soundtype.SFX:
                PlayEffect(sound); break;
        }
    }
    public void ResetAllMusic()
    {
        sfxSource.Stop();
        bgmSource.Stop();
    }
    public AudioClip GetAudio(string soundName)
    {
        AudioClip audioClip = null; 
        return audioClip;
    }
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume); 
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", volume); 
    }
}