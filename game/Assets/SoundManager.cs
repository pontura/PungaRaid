using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    private AudioSource loopAudioSource;

    void Start()
    {
        Events.OnSoundFX += OnSoundFX;
        Events.OnSoundFXLoop += OnSoundFXLoop;
    }

    void OnDestroy()
    {
        Events.OnSoundFX -= OnSoundFX;
        Events.OnSoundFXLoop -= OnSoundFXLoop;
        if (loopAudioSource)
        {
            loopAudioSource = null;
            loopAudioSource.Stop();
        }
    }
    void OnSoundFXLoop(string soundName)
    {
        if (Data.Instance.soundsVolume == 0) return;

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

        if (Data.Instance.soundsVolume == 0) return;
       // print("_________________soundName: " + soundName);
        audioSource.PlayOneShot(Resources.Load("sound/" + soundName) as AudioClip);

    }
}
