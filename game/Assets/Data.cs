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
    
    static Data mInstance = null;
    [HideInInspector]
    public ClothesSettings clothesSettings;
    [HideInInspector]
    public EnemiesManager enemiesManager;
    [HideInInspector]
    public GameSettings gameSettings;

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
    public void LoadLevel(string aLevelName)
    {
        Time.timeScale = 1;
        Application.LoadLevel(aLevelName);
    }
    void Awake()
    {
		QualitySettings.vSyncCount = 1;
        if (!mInstance)
            mInstance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        clothesSettings = GetComponent<ClothesSettings>();
        enemiesManager = GetComponent<EnemiesManager>();
        gameData = GetComponent<GameData>();
        gameSettings = GetComponent<GameSettings>();

        GetComponent<MusicManager>().Init();

        Events.OnMusicVolumeChanged += OnMusicVolumeChanged;
        Events.OnSoundsVolumeChanged += OnSoundsVolumeChanged;

//#if UNITY_ANDROID || UNITY_IPHONE
       // Handheld.PlayFullScreenMovie(movPath, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFill);
//#endif

    }
    void Start()
    {
       // Data.Instance.LoadLevel("01_Splash");
    }
    void OnMusicVolumeChanged(float value)
    {
        musicVolume = value;
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
