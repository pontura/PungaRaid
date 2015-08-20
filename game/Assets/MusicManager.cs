﻿using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public float volume;
       
	public void Init () {
        GetComponent<AudioSource>().loop = true;
        OnMusicVolumeChanged(Data.Instance.musicVolume);

        Events.OnGamePaused += OnGamePaused;
        Events.OnMusicVolumeChanged += OnMusicVolumeChanged;
        Events.OnMusicChange += OnMusicChange;
	}
    void OnDestroy()
    {
        Events.OnGamePaused -= OnGamePaused;
        Events.OnMusicVolumeChanged -= OnMusicVolumeChanged;
        Events.OnMusicChange -= OnMusicChange;
    }

    void OnMusicChange(string soundName)
    {
        if (soundName == "") GetComponent<AudioSource>().Stop();
        if (GetComponent<AudioSource>().clip && GetComponent<AudioSource>().clip.name == soundName) return;
        GetComponent<AudioSource>().clip = Resources.Load("music/" + soundName) as AudioClip;
        GetComponent<AudioSource>().Play();

        if (soundName == "victoryMusic" || soundName == "gameOverTemp") 
            GetComponent<AudioSource>().loop = false;
        else
            GetComponent<AudioSource>().loop = true;
    }
    void OnSoundsFadeTo(float to)
    {
        if (to > 0) to = volume;
       // TweenVolume tv = TweenVolume.Begin(gameObject, 1, to);
        //tv.PlayForward();
        //tv.onFinished.Clear();
    }
    void OnMusicVolumeChanged(float value)
    {
        GetComponent<AudioSource>().volume = value;
        volume = value;
    }
    void OnGamePaused(bool paused)
    {
        if(paused)
            GetComponent<AudioSource>().Stop();
        else
            GetComponent<AudioSource>().Play();
    }
    void stopAllSounds()
    {
        GetComponent<AudioSource>().Stop();
    }
}



