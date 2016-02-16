using UnityEngine;
using System.Collections;

public class CharacterCollider : MonoBehaviour {

    public types type;
    public Character character;
    private BoxCollider2D collider2d;
    private Blocker lastBlocker;

    public enum types
    {
        CENTER,
        TOP,
        BOTTOM,
        GUN
    }
    void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
        OnChangeLaneComplete();

        Events.OnChangeLaneComplete += OnChangeLaneComplete;
        Events.OnChangeLane += OnChangeLane;
        Events.OnPowerUpShoot += OnPowerUpShoot;
        Events.OnHeroPowerUpOff += OnHeroPowerUpOff;
        Events.OnVulnerability += OnVulnerability;
        
    }
    void OnDestroy()
    {
        Events.OnChangeLaneComplete -= OnChangeLaneComplete;
        Events.OnChangeLane -= OnChangeLane;
        Events.OnPowerUpShoot -= OnPowerUpShoot;
        Events.OnHeroPowerUpOff -= OnHeroPowerUpOff;
        Events.OnVulnerability -= OnVulnerability;
    }
    void OnVulnerability(bool isOn)
    {
        if (type == types.CENTER)
            collider2d.enabled = !isOn;
    }
    void OnChangeLaneComplete()
    {
        if (type != types.GUN)
            collider2d.enabled = true;
    }
    void OnChangeLane()
    {
        if (type != types.GUN)
            collider2d.enabled = false;
    }
    void OnPowerUpShoot(PowerupManager.types newType)
    {
        if (type == types.GUN && newType == PowerupManager.types.CHUMBO)
        {
            collider2d.enabled = true;
            Invoke("PowerUpReady", 0.2f);
        }
    }
    void PowerUpReady()
    {
        collider2d.enabled = false;
    }
    void OnHeroPowerUpOff()
    {
       // collider2d.size = new Vector2(4, collider2d.size.y);
        if (type == types.GUN)
            collider2d.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(!enemy) return;        

        Blocker blocker = enemy.GetComponent<Blocker>();

        if (type == types.GUN)
        {
          //  if(blocker == null)
                enemy.Explote();

            return;
        } else
        if (type == types.CENTER)
        {
            if (blocker)
            {
                if (character.hero.state != Hero.states.DEAD)
                    Events.OnHeroDie();
                return;
            }
            character.OnCollisionCenter(enemy);
        }
        else
        {
            
            if (!blocker) return;
            lastBlocker = blocker;
            character.OnCollisionWithBlocker(blocker, type);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (type == types.CENTER) return;
        if (type == types.GUN) return;

        if (lastBlocker == other.GetComponent<Blocker>())
        {
            if (type == types.TOP)
                character.CantMoveUp = false;
            else
                character.CantMoveDown = false;
        }
    }
}
