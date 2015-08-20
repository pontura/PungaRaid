using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Data : MonoBehaviour
{
    private string movPath = "bumper04.mp4";

    public string URL_php_email = "http://www.pontura.com/tipitap/";
        
    public int totalScore;
    public int errors = 0;

    public float musicVolume = 1;
    public float soundsVolume = 1;
    public bool caps = false;

    public GameData gameData;

    //malisimo
    public bool MainMenuPopupOn;
    public bool TutorialReady;

    const string PREFAB_PATH = "Data";

    private Fade fade;

    static Data mInstance = null;

    public static Data Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<Data>();

                if (mInstance == null)
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>(PREFAB_PATH)) as GameObject;
                    mInstance = go.GetComponent<Data>();
                }
            }
            return mInstance;
        }
    }
    public void LoadLevel(string aLevelName, float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        Time.timeScale = 1;
        fade.LoadLevel(aLevelName, aFadeOutTime, aFadeInTime, aColor);
    }
    void Awake()
    {
		//Application.targetFrameRate = 30;
		QualitySettings.vSyncCount = 1;
        fade = GetComponentInChildren<Fade>();
        fade.gameObject.SetActive(true);
       //PlayerPrefs.SetInt("level_1_1", 0);
        //PlayerPrefs.SetInt("level_1_2", 0);
        //PlayerPrefs.SetInt("level_1_3", 0);
        //PlayerPrefs.SetInt("level_1_4", 0);   
        //PlayerPrefs.SetInt("hats", 0);
        //PlayerPrefs.SetInt("legs", 0);

        //if we don't have an [_instance] set yet
        if (!mInstance)
            mInstance = this;
        //otherwise, if we do, kill this thing
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        gameData = GetComponent<GameData>();
        GetComponent<UserData>().Init();
        GetComponent<MusicManager>().Init();

        Events.OnMusicVolumeChanged += OnMusicVolumeChanged;
        Events.OnSoundsVolumeChanged += OnSoundsVolumeChanged;
        Events.OnCapsChanged += OnCapsChanged;

//#if UNITY_ANDROID || UNITY_IPHONE
        Handheld.PlayFullScreenMovie(movPath, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFill);
//#endif

    }
    void Start()
    {
       // Data.Instance.LoadLevel("02_MainMenu", 1, 1, Color.black);
    }
    void OnMusicVolumeChanged(float value)
    {
        musicVolume = value;
    }
    void OnCapsChanged(bool _caps)
    {
        caps = _caps;
    }
    void OnSoundsVolumeChanged(float value)
    {
        soundsVolume = value;
    }
    void OnBadgeSelected(int _totalScore)
    {
        this.totalScore = _totalScore;
    }
    public void Reset()
    {

    }
    void OnSaveVolumes(float _musicVolume, float _soundsVolume)
    {
        this.musicVolume = _musicVolume;
        this.soundsVolume = _soundsVolume;
    }
}
