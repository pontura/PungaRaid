using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public void UnblockAllLevels()
    {
       // Data.Instance.GetComponent<UserData>().UnblockAllLevels();
    }
    void Start()
    {
        Events.OnMusicChange("gameMenu");
    }
	public void Play () {
        Events.OnSoundFX("buttonPress");
        Data.Instance.LoadLevel("03_LevelSelector");        
	}
    public void Gallery()
    {
        Events.OnSoundFX("buttonPress");
        Data.Instance.LoadLevel("06_Gallery");
    }
    public void Settings()
    {
        Events.OnSoundFX("buttonPress");
        GetComponent<Menu>().Init();
    }
    public void SoundsToogle()
    {
        if (Data.Instance.soundsVolume == 1)
            Data.Instance.soundsVolume = 0;
        else
            Data.Instance.soundsVolume = 1;

        Events.OnSoundsVolumeChanged( Data.Instance.soundsVolume );
    }
    public void MusicToogle()
    {
        if (Data.Instance.musicVolume == 1)
            Data.Instance.musicVolume = 0;
        else
            Data.Instance.musicVolume = 1;

        Events.OnMusicVolumeChanged(Data.Instance.musicVolume);
    }
}
