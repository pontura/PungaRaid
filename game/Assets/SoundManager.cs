using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        Events.OnSoundFX += OnSoundFX;
    }

    void OnDestroy()
    {
        Events.OnSoundFX -= OnSoundFX;
    }

    void OnSoundFX(string soundName)
    {
        if (soundName == "")
        {
            audioSource.Stop();
            return;
        }

        if (Data.Instance.soundsVolume == 0) return;
        print("_________________soundName: " + soundName);
        audioSource.PlayOneShot(Resources.Load("sound/" + soundName) as AudioClip);

    }
}
