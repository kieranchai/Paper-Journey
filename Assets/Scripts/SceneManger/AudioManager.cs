using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    //Array to store audio files
    public Sound[] bgmSounds, sfxSounds; 
    public AudioSource bgmSource, sfxSource;

    public static AudioManager instance;

    private void Awake()
    {
        //Instantiate AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBGM("temp");
    }

    public void PlayBGM(string name)
    {
        Sound s = Array.Find(bgmSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Audio Not Found");
        }

        else
        {
            bgmSource.clip = s.clip;
            bgmSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (sfxSource.clip == null)
        {
            Debug.Log("Audio Not Found");
        }

        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }

    public void BGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}