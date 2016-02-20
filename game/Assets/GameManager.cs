﻿using UnityEngine;
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

    public float speed;
    public float realSpeed = 0;

    public CharacterManager characterManager;
    private LevelsManager levelsManager;
    public MainCamera camera;
    public BackgroundScrolleable[] backgroundsScrolleable;
    public ParticleSystem explotion;
    private CombosManager combosManager;

    private float DEFAULT_SPEED = 0.09f;

    
    public void Init()
    {
        

        Data.Instance.errors = 0;
       combosManager = Data.Instance.combosManager;

        Events.OnScoreAdd += OnScoreAdd;
        Events.OnHeroDie += OnHeroDie;
        Events.OnHeroCrash += OnHeroCrash;
        Events.StartGame += StartGame;
        Events.OnExplotion += OnExplotion;
        Events.OnChangeSpeed += OnChangeSpeed;
        Events.OnResetSpeed += OnResetSpeed;

        characterManager = GetComponent<CharacterManager>();
        characterManager.Init();

        levelsManager = GetComponent<LevelsManager>();
        levelsManager.Init();

        Events.OnStartCountDown();

        Events.OnMusicChange("Gameplay");

        score = 0;

    }
    void OnDestroy()
    {
        Events.OnScoreAdd -= OnScoreAdd;
        Events.OnHeroDie -= OnHeroDie;
        Events.OnHeroCrash -= OnHeroCrash;
        Events.StartGame -= StartGame;
        Events.OnExplotion -= OnExplotion;
        Events.OnChangeSpeed -= OnChangeSpeed;
        Events.OnResetSpeed -= OnResetSpeed;

        combosManager = null;
    }
    public void OnScoreAdd(int _score)
    {
        score += _score * combosManager.comboID;
        Events.OnRefreshScore(score);
    }
    void StartGame()
    {
        speed = DEFAULT_SPEED;
        state = states.ACTIVE;
        
    }
    void OnHeroDie()
    {
        state = states.ENDING;
        Game.Instance.state = Game.states.ENDED;
        Events.OnSoundFX("warningPopUp");
        Events.OnMusicChange("gameOverTemp");
        Invoke("gameOverReady", 2);        
    }
    void gameOverReady()
    {
        Events.OnPoolAllItemsInScene();  
    }
    public void Restart()
    {
        Application.LoadLevel("04_Game");
    }
    void OnChangeSpeed(float _speed, bool accelerating)
    {
        speed *= 1.4f;
        if (!accelerating)
           realSpeed = speed;
    }
    void OnResetSpeed()
    {
        speed = DEFAULT_SPEED;
    }
    void OnHeroCrash()
    {
      //  Events.OnSoundFX("trip");
       // state = states.INACTIVE;
       // Invoke("goOn", 0.2f);
    }
    void goOn()
    {
        state = states.ACTIVE;
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
    void OnExplotion()
    {
        Character character = characterManager.character;
        ParticleSystem ps = Instantiate(explotion) as ParticleSystem;
        ps.transform.SetParent(character.transform.parent.transform);
        ps.transform.localScale = Vector3.one;
        Vector3 newPos = character.transform.localPosition;
        newPos.y += 3;
        ps.transform.localPosition = newPos;
        ps.Play();
    }
}
