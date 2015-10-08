using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PowerupManager : MonoBehaviour {

    public types type;

    public GameObject powerUpsContainer;

    public PowerUpOn powerUp1;

    private PowerUpOn powerUp;
    private Character character;

    private BoxCollider2D collider2d;

    public enum types
    {
        NONE,
        MOTO
    }
    void Start()
    {
        character = GetComponent<Character>();
        Events.OnPowerUp += OnPowerUp;
        Events.OnHeroPowerUpOff += OnHeroPowerUpOff;
        collider2d = GetComponent<BoxCollider2D>();
    }
    void OnDestroy()
    {
        Events.OnPowerUp -= OnPowerUp;
        Events.OnHeroPowerUpOff -= OnHeroPowerUpOff;
    }
    void OnPowerUp(int id)
    {
        if (type == types.MOTO) return;

        type = types.MOTO;
        powerUp = Instantiate(powerUp1) as PowerUpOn;
        powerUp.transform.SetParent(powerUpsContainer.transform);
        powerUp.transform.localScale = Vector3.one;
        powerUp.transform.localPosition = Vector3.zero;
        powerUp.Init(3);
        collider2d.size = new Vector2(8, collider2d.size.y);

        character.OnSetHeroState(false);

        Events.OnChangeSpeed(2);

       // Invoke("PowerupOff", 5);
    }

    void OnHeroPowerUpOff()
    {
        collider2d.size = new Vector2(1, collider2d.size.y);
        type = types.NONE;
        character.OnSetHeroState(true);
        Destroy(powerUp.gameObject);
        Events.OnResetSpeed();
    }
}
