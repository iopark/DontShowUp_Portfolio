using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public enum Soundtype
    {
        BGM, 
        SFX
    }

    public AudioMixer audioMixer;
    AudioSource[] soundSource;
    
    AudioMixerGroup[] audioMixerGroup;
    HashSet<Sound> soundList = new HashSet<Sound>();

    public float CurrentMasterVolume
    {
        get
        {
            float volume; 
            audioMixer.GetFloat("Master", out volume);
            return volume;
        }
    }
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        string[] listnames = Enum.GetNames(typeof(Soundtype)); 
        audioMixer = Resources.Load<AudioMixer>("Sound/GameMasterMixer");
        audioMixerGroup = audioMixer.FindMatchingGroups("Master"); 
        SetMasterVolume(-10f); 
    }
    public void PlaySound(string soundName)
    {
        
    }
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume); 
    }
}
