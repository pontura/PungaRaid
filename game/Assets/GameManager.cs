using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float distance;
    public int score;

    public states state;
    public enum states
    {
        IDLE,
        ACTIVE,
        INACTIVE,
        GAMEOVER
    }

    private float speed;
    public float realSpeed = 0;

    private float lastVolume;
    private CharacterManager characterManager;
    private LevelsManager levelsManager;
    public MainCamera camera;

    public void Init()
    {
        lastVolume = Data.Instance.musicVolume;
        Data.Instance.errors = 0;

        Events.OnHeroCrash += OnHeroCrash;
        Events.OnHeroSlide += OnHeroSlide;
        Events.OnLevelComplete += OnLevelComplete;
        Events.StartGame += StartGame;
        Events.OnGameOver += OnGameOver;

        characterManager = GetComponent<CharacterManager>();
        characterManager.Init();

        levelsManager = GetComponent<LevelsManager>();
        levelsManager.Init();

        Events.OnStartCountDown();

    }
    void OnDestroy()
    {
        Events.OnHeroCrash -= OnHeroCrash;
        Events.OnHeroSlide -= OnHeroSlide;
        Events.OnLevelComplete -= OnLevelComplete;
        Events.StartGame -= StartGame;
        Events.OnGameOver -= OnGameOver;

        Events.OnMusicVolumeChanged(lastVolume);
    }
    void StartGame()
    {
        speed = 0.08f;
        state = states.ACTIVE;        
    }
    void OnGameOver()
    {
        state = states.GAMEOVER;
        Events.OnSoundFX("warningPopUp");
        Events.OnMusicChange("gameOverTemp");
        Invoke("voice", 2);
    }
    void voice()
    {
        Events.OnSoundFX("23_Try Again");
    }
    void OnLevelComplete()
    {
        state = states.INACTIVE;
        lastVolume = Data.Instance.musicVolume;
        Events.OnMusicVolumeChanged(0.2f);
        Events.OnSoundFX("victoryMusic");
    }
    void OnHeroCrash()
    {
        Data.Instance.errors++;
        realSpeed = 0;
        Events.OnSoundFX("trip");
        if (state != states.GAMEOVER)
        {
            state = states.INACTIVE;
            Invoke("goOn", 1.7f);
        }
    }
    void goOn()
    {
        if (state == states.GAMEOVER) return;
        state = states.ACTIVE;
    }
    void OnHeroSlide(int id)
    {
        realSpeed = speed*2;
        Events.OnSoundFX("stepPond");
    }
    void Update()
    {
        if (state == states.INACTIVE || state == states.GAMEOVER)
        {
            return;
        }
        if (realSpeed < speed)
            realSpeed += 0.0001f;
        else if (realSpeed > speed)
            realSpeed -= 0.0001f;

        if (state == states.ACTIVE)
        {
			float _speed = (realSpeed*100)*Time.smoothDeltaTime;
            distance += _speed;
		}
        camera.UpdatePosition(distance);
        characterManager.UpdatePosition(distance);
        levelsManager.CheckForNewLevel(distance);
	}
}
