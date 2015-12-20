using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int laneId;
    public Animator anim;
    
    public float distance;
    public bool isPooled;
    private bool isActivated;

    public void Init(EnemySettings settings, int laneId)
    {
        isPooled = false;
        isActivated = false;
        this.laneId = laneId;
        Enemy_Init(settings, laneId);
    }
    void Update()
    {
        if (isPooled) return;

        Vector3 pos = transform.localPosition;

        if (pos.x + 5 < Game.Instance.gameManager.distance)
        {
            Pool();
        }
        else if (pos.x < Game.Instance.gameManager.distance + 20)
        {
            //isActivated = solo cuando entra dentro del area activa
            if (!isActivated)
            {
                isActivated = true;
                Enemy_Activate();
            }
            Enemy_Update(pos);
        }
    }
    public void Pool()
    {
        Data.Instance.enemiesManager.Pool(this);
        isPooled = true;
        Enemy_Pooled();
    }
    public void Crashed()
    {
        OnCrashed();
    }
    public void Explote()
    {
        Events.OnSoundFX("Explosion");
        OnExplote();
    }
    public virtual void OnSecondaryCollision(Collider2D other) { }
    public virtual void Enemy_Pooled() { }
    public virtual void Enemy_Init(EnemySettings settings, int laneId) { }
    public virtual void Enemy_Activate() { }
    public virtual void Enemy_Update(Vector3 pos)  {  }
    public virtual void OnCrashed() { }
    public virtual void OnExplote() { }
}
