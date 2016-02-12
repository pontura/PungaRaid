﻿using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class Character : MonoBehaviour {

    [SerializeField]
    Hero heroAsset;

    public int TOTAL_LIVES = 1;
  //  public int lives;
    public Hero hero;

    float timeToCrossLane;

  //  public states state;
    public GameObject container;
    public GameObject heroContainer;
    public GameObject powerUpsContainer;
    public PowerupManager powerupManager;
    private BoxCollider2D collider;

    public bool CantMoveUp;
    public bool CantMoveDown;

    public actions action;
    public enum actions
    {
        PLAYING,
        CHANGING_LANE
    }

    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }
    public void Init()
    {
        timeToCrossLane = Data.Instance.gameData.timeToCrossLane;
    }
    void Start()
    {        
        powerupManager = GetComponent<PowerupManager>();
        transform.localScale = new Vector3(0.52f, 0.52f, 0.52f);

        hero = Instantiate(heroAsset) as Hero;
        hero.transform.SetParent(heroContainer.transform);

        hero.transform.localPosition = Vector3.zero;
    }
    public void OnSetHeroState( bool show)
    {
        if (!show)
            heroContainer.transform.localPosition = new Vector3(-1000, 0, 0);
        else
            heroContainer.transform.localPosition = Vector3.zero;
    }
    public void PowerupActivated(PowerupManager.types type)
    {
        switch (type)
        {
            case PowerupManager.types.CHUMBO:                
                hero.ChumboRun();  
                break;
        }
    }
    public void Dash()
    {
        if (powerupManager.type == PowerupManager.types.CHUMBO)
        {
            hero.ChumboFire();
            Events.OnPowerUpShoot(PowerupManager.types.CHUMBO);
        } else
        if (hero.state != Hero.states.DASH && powerupManager.type == PowerupManager.types.NONE)
        {
            Events.OnChangeSpeed(6, false);
            Events.OnHeroDash();
            Invoke("ResetDash", 0.5f);
        }
    }
    void ResetDash()
    {
        if (powerupManager.type == PowerupManager.types.MOTO) return;
        hero.ResetAnimation();
        Events.OnResetSpeed();
    }
	public void MoveUP()
    {
        Move(2f, true);
    }
    public void MoveDown()
    {       
        Move(-2f, true);
    }
    public void Idle()
    {
        hero.Run();
    }
    public void GotoCenterOfLane()
    {
        Vector3 pos = transform.localPosition;
        pos.y = 0;
        transform.localPosition = pos;
        container.transform.localPosition = Vector3.zero;
    }
    private void Move(float _y, bool firstStep)
    {
        if (action == actions.CHANGING_LANE) return;
        CantMoveUp = false;
        CantMoveDown = false;

        Events.OnChangeLane();
        action = actions.CHANGING_LANE;
        Events.OnSoundFX("changeLane");
        TweenParms parms = new TweenParms();
        parms.Prop("localPosition", new Vector3(0,_y,0));
        parms.Ease(EaseType.Linear);

        parms.OnComplete(OnChangeLaneComplete);
        HOTween.To(container.transform, timeToCrossLane, parms);
    }
    void OnChangeLaneComplete()
    {
        Events.OnChangeLaneComplete();
        action = actions.PLAYING;
    }

    public void OnCollisionCenter(Enemy enemy) 
    {
        if (enemy.laneId == Game.Instance.gameManager.characterManager.lanes.laneActiveID)
        {
            if (enemy.GetComponent<PowerUp>())
            {
                enemy.GetComponent<PowerUp>().Activate();
            }
            else if (hero.state == Hero.states.DASH && enemy.GetComponent<Victim>())
            {
                enemy.Explote();
            }
            else if (powerupManager.type == PowerupManager.types.MOTO)
            {
                Events.OnHeroCrash();
                enemy.Explote();
            }
            else
            {
                enemy.Crashed();
                Die();
            }
        }
        else
        {
            if (enemy.GetComponent<Victim>())
            {
                enemy.GetComponent<Victim>().Steal();
                Events.OnSoundFX("Pung");
                Events.OnScoreAdd(Random.Range(5, 10) * 10);
            }
        }
    }
    public void OnCollisionWithBlocker(Blocker blocker, CharacterCollider.types type)
    {
        if (blocker.laneId == Game.Instance.gameManager.characterManager.lanes.laneActiveID)
        {            
            Die();
            Game.Instance.gameManager.realSpeed = 0;
            return;
        }
        else
        {
            if (type == CharacterCollider.types.TOP) CantMoveUp = true;
            if (type == CharacterCollider.types.BOTTOM) CantMoveDown = true;
        }
    }
    void Die()
    {
        if (action == actions.CHANGING_LANE || hero.state == Hero.states.DEAD) return;
            Events.OnHeroDie();
    }
}
