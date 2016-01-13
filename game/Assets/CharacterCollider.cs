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
        BOTTOM
    }
    void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
        Events.OnChangeLaneComplete += OnChangeLaneComplete;
        Events.OnChangeLane += OnChangeLane;
        Events.OnPowerUp += OnPowerUp;
        Events.OnHeroPowerUpOff += OnHeroPowerUpOff;
    }
    void OnDestroy()
    {
        Events.OnChangeLaneComplete -= OnChangeLaneComplete;
        Events.OnChangeLane -= OnChangeLane;
        Events.OnPowerUp -= OnPowerUp;
        Events.OnHeroPowerUpOff -= OnHeroPowerUpOff;
    }
    void OnChangeLaneComplete()
    {
        collider2d.enabled = true;
    }
    void OnChangeLane()
    {
        collider2d.enabled = false;
    }
    void OnPowerUp(PowerupManager.types newType)
    {
        if (type == types.CENTER)
            collider2d.size = new Vector2(10, collider2d.size.y);
    }
    void OnHeroPowerUpOff()
    {
        collider2d.size = new Vector2(4, collider2d.size.y);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(!enemy) return;
        Blocker blocker = enemy.GetComponent<Blocker>();
 
        if (type == types.CENTER)
        {
            if (blocker)
            {
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
        if (lastBlocker == other.GetComponent<Blocker>())
        {
            if (type == types.TOP)
                character.CantMoveUp = false;
            else
                character.CantMoveDown = false;
        }
    }
}
