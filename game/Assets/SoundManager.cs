using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

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
            GetComponent<AudioSource>().Stop();
            return;
        }

        if (Data.Instance.soundsVolume == 0) return;

        GetComponent<AudioSource>().PlayOneShot(Resources.Load("sound/" + soundName) as AudioClip);

    }
}
