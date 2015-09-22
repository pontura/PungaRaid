using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public AudioSource audioSource;
    public float volume;
       
	public void Init () {
        audioSource.loop = true;
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
        if (soundName == "") audioSource.Stop();
        if (audioSource.clip && audioSource.clip.name == soundName) return;
        audioSource.clip = Resources.Load("music/" + soundName) as AudioClip;
        audioSource.Play();

        if (soundName == "victoryMusic" || soundName == "gameOverTemp")
            audioSource.loop = false;
        else
            audioSource.loop = true;
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
        audioSource.volume = value;
        volume = value;
    }
    void OnGamePaused(bool paused)
    {
        if(paused)
            audioSource.Stop();
        else
            audioSource.Play();
    }
    void stopAllSounds()
    {
        audioSource.Stop();
    }
}



