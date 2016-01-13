﻿using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PowerupManager : MonoBehaviour {

    public types type;

    public GameObject powerUpsContainer;

    public PowerUpOn powerUp1;

    private PowerUpOn powerUp;
    private Character character;

    public enum types
    {
        NONE,
        MOTO,
        CHUMBO,
        GIL
    }
    void Start()
    {
        character = GetComponent<Character>();
        Events.OnPowerUp += OnPowerUp;
        Events.OnHeroPowerUpOff += OnHeroPowerUpOff;
    }
    void OnDestroy()
    {
        Events.OnPowerUp -= OnPowerUp;
        Events.OnHeroPowerUpOff -= OnHeroPowerUpOff;
    }
    void OnPowerUp(types newType)
    {
        if (type == newType) return;

        Events.OnBarInit();
        Events.OnSoundFX("PowerUpItem");

        Moto();
    }
    void Moto()
    {
        type = types.MOTO;
        powerUp = Instantiate(powerUp1) as PowerUpOn;
        powerUp.transform.SetParent(powerUpsContainer.transform);
        powerUp.transform.localScale = Vector3.one;
        powerUp.transform.localPosition = Vector3.zero;
        powerUp.Init(10);

        character.OnSetHeroState(false);
        Events.OnChangeSpeed(2, true);
    }

    void OnHeroPowerUpOff()
    {
        Events.OnSoundFX("PowerUpOff");
        Events.OnSoundFXLoop("");
        Events.OnBarReady();

        
        type = types.NONE;
        character.OnSetHeroState(true);
        Destroy(powerUp.gameObject);
        Events.OnResetSpeed();
    }
}
