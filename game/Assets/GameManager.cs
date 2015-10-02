using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float distance;
    public int score;

    public states state;
    public enum states
    {
        ACTIVE,
        INACTIVE,
        ENDING
    }

    private float speed;
    public float realSpeed = 0;

    private float lastVolume;
    public CharacterManager characterManager;
    private LevelsManager levelsManager;
    public MainCamera camera;
    public BackgroundScrolleable[] backgroundsScrolleable;

    public void Init()
    {
        lastVolume = Data.Instance.musicVolume;
        Data.Instance.errors = 0;

        Events.OnHeroDie += OnHeroDie;
        Events.OnHeroCrash += OnHeroCrash;
        Events.OnHeroSlide += OnHeroSlide;
        Events.StartGame += StartGame;

        characterManager = GetComponent<CharacterManager>();
        characterManager.Init();

        levelsManager = GetComponent<LevelsManager>();
        levelsManager.Init();

        Events.OnStartCountDown();

    }
    void OnDestroy()
    {
        Events.OnHeroDie -= OnHeroDie;
        Events.OnHeroCrash -= OnHeroCrash;
        Events.OnHeroSlide -= OnHeroSlide;
        Events.StartGame -= StartGame;

        Events.OnMusicVolumeChanged(lastVolume);
    }
    void StartGame()
    {
        speed = 0.09f;
        state = states.ACTIVE;        
    }
    void OnHeroDie()
    {
        state = states.ENDING;
        Events.OnPoolAllItemsInScene();

        Game.Instance.state = Game.states.ENDED;
        Events.OnSoundFX("warningPopUp");
        Events.OnMusicChange("gameOverTemp");
        Invoke("gameOverReady", 2);        
    }
    void gameOverReady()
    {
        Application.LoadLevel("04_Game");
    }
    void OnHeroCrash()
    {
        Events.OnSoundFX("trip");
        state = states.INACTIVE;
        Invoke("goOn", 0.2f);
    }
    void goOn()
    {
        state = states.ACTIVE;
    }
    void OnHeroSlide(int id)
    {
        realSpeed = speed*2;
        Events.OnSoundFX("stepPond");
    }
    void Update()
    {
        if (state == states.INACTIVE)
            return;

        if (Game.Instance.state == Game.states.ENDED)
            realSpeed -= 0.001f;
        else
            realSpeed += 0.001f;

        if (realSpeed > speed)
            realSpeed = speed;
        else if (realSpeed < 0)
            realSpeed = 0;

        float _speed = (realSpeed*100)*Time.smoothDeltaTime;
        distance += _speed;

        foreach (BackgroundScrolleable bgScrolleable in backgroundsScrolleable)
            bgScrolleable.UpdatePosition(distance, _speed);

        camera.UpdatePosition(distance);
        characterManager.UpdatePosition(distance);

        if (state == states.ENDING)
            return;

        levelsManager.CheckForNewLevel(distance);

        
	}
}
