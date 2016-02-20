﻿using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    private AudioSource loopAudioSource;
    public float volume;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("SFXVol", 1);
        Events.OnSoundFX += OnSoundFX;
        Events.OnSoundFXLoop += OnSoundFXLoop;
        Events.OnSoundsVolumeChanged += OnSoundsVolumeChanged;        
        Events.OnHeroDie += OnHeroDie;
    }
    void OnHeroDie()
    {
        OnSoundFXLoop("");
    }
    void OnDestroy()
    {
        Events.OnSoundFX -= OnSoundFX;
        Events.OnSoundFXLoop -= OnSoundFXLoop;
        Events.OnSoundsVolumeChanged -= OnSoundsVolumeChanged;
        Events.OnHeroDie -= OnHeroDie;
        if (loopAudioSource)
        {
            loopAudioSource = null;
            loopAudioSource.Stop();
        }
    }
    void OnSoundsVolumeChanged(float vol)
    {
        PlayerPrefs.SetFloat("SFXVol", vol);
        this.volume = vol;
    }
    void OnSoundFXLoop(string soundName)
    {
        if (volume == 0) return;

        if (!loopAudioSource)
            loopAudioSource = gameObject.AddComponent<AudioSource>() as AudioSource;

        if (soundName != "")
        {
            loopAudioSource.clip = Resources.Load("sound/" + soundName) as AudioClip;
            loopAudioSource.Play();
            loopAudioSource.loop = true;
        }
        else
        {
            loopAudioSource.Stop();
        }
    }
    void OnSoundFX(string soundName)
    {
        if (soundName == "")
        {
            audioSource.Stop();
            return;
        }

        if (volume == 0) return;
       // print("_________________soundName: " + soundName);
        audioSource.PlayOneShot(Resources.Load("sound/" + soundName) as AudioClip);

    }
}
