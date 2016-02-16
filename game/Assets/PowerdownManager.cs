using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PowerdownManager : MonoBehaviour {

    public types type;

    public GameObject sorete;

    private Character character;

    public enum types
    {
        SORETE
    }
    void Start()
    {
        sorete.SetActive(false);
        character = GetComponent<Character>();
       // Events.OnPowerUp += OnPowerUp;
       // Events.OnHeroPowerUpOff += OnHeroPowerUpOff;
    }
    void OnDestroy()
    {
       // Events.OnPowerUp -= OnPowerUp;
       // Events.OnHeroPowerUpOff -= OnHeroPowerUpOff;
    }
    void OnPowerUp(types newType)
    {
        if (type != types.SORETE) return;

      //  Events.OnBarInit(newType);
        Events.OnSoundFX("PowerUpItem");
        
        switch (newType)
        {
            case types.SORETE: 
                //Moto(); 
                break;
        }
        
    }
    //void Chumbo()
    //{
    //    type = types.CHUMBO;
    //    character.PowerupActivated(type);
    //}
    //void Gil()
    //{
    //    powerUpGil.SetActive(true);
    //    type = types.GIL;
    //    character.PowerupActivated(type);
    //}
    //void Moto()
    //{
    //    type = types.MOTO;
    //    powerUp = Instantiate(powerUp1) as PowerUpOn;
    //    powerUp.transform.SetParent(powerUpsContainer.transform);
    //    powerUp.transform.localScale = Vector3.one;
    //    powerUp.transform.localPosition = Vector3.zero;
    //    powerUp.Init(10);

    //    character.OnSetHeroState(false);
    //    Events.OnChangeSpeed(2, true);
    //}
}
