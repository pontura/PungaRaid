using UnityEngine;
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
        MOTO
    }
    void Start()
    {
        character = GetComponent<Character>();
        Events.OnPowerUp += OnPowerUp;
    }
    void OnDestroy()
    {
        Events.OnPowerUp -= OnPowerUp;
    }
    void OnPowerUp(int id)
    {
        if (type == types.MOTO) return;

        type = types.MOTO;
        character.state = Character.states.INDESTRUCTIBLE;
        powerUp = Instantiate(powerUp1) as PowerUpOn;
        powerUp.transform.SetParent(powerUpsContainer.transform);
        powerUp.transform.localScale = Vector3.one;
        powerUp.transform.localPosition = Vector3.zero;

        character.OnSetHeroState(false);

        Invoke("PowerupOff", 5);
    }
    void PowerupOff()
    {
        type = types.NONE;
        character.OnSetHeroState(true);
        Destroy(powerUp.gameObject);
    }
}
