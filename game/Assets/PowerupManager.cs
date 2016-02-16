using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PowerupManager : MonoBehaviour {

    public types type;

    public GameObject powerUpsContainer;
    public GameObject powerUpGil;

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
        powerUpGil.SetActive(false);
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
        if (type != types.NONE) return;

        Events.OnBarInit(newType);
        Events.OnSoundFX("PowerUpItem");

        Moto(); return;
        
        switch (newType)
        {
            case types.MOTO: Moto(); break;
            case types.CHUMBO: Chumbo(); break;
            case types.GIL: Gil(); break;
        }
        
    }
    void Chumbo()
    {
        type = types.CHUMBO;
        character.PowerupActivated(type);
    }
    void Gil()
    {
        powerUpGil.SetActive(true);
        type = types.GIL;
        character.PowerupActivated(type);
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
        character.Idle();

        if (type == types.MOTO)
        {
            Events.OnSoundFX("PowerUpOff");
            Events.OnSoundFXLoop("");
            character.OnSetHeroState(true);
            if (powerUp != null)
                Destroy(powerUp.gameObject);
            Events.OnResetSpeed();
            character.Jump();
        }
        else if (type == types.GIL)
        {
            powerUpGil.SetActive(false);           
        }
        type = types.NONE;
    }
}
