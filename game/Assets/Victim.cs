using UnityEngine;
using System.Collections;

public class Victim : Enemy {

    public float speed;
    public states state;

    public enum states
    {
        IDLE,
        WALKING,
        STOLEN,
        CRASHED
    }
    private Clothes clothes;
    private string currentAnim;
    private BoxCollider2D collider;


    override public void Enemy_Activate()
    {
        Walk();
        anim.GetComponentInChildren<Animator>();
        anim.SetBool("WALK", true);
        collider.enabled = true;
    }
    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
        collider = GetComponent<BoxCollider2D>();
        clothes = GetComponent<Clothes>();
        clothes.Restart();

        distance = Game.Instance.gameManager.distance;
        speed = settings.speed;
        Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);

        if (speed < 0)
            scale.x *= -1;

        transform.localScale = scale;        
    }
    override public void Enemy_Update(Vector3 pos)
    {
        if (state == states.CRASHED) return;
        if (state == states.STOLEN) return;

        pos.x -= speed;
        transform.localPosition = pos;
    }
    override public void Enemy_Pooled()
    {
        anim.SetBool("WALK", false);
        state = states.IDLE;
    }
    public void Walk()
    {
        state = states.WALKING;
        if (Random.Range(0, 100) < 33)
            currentAnim = "victimAWalk_phone";
        else if (Random.Range(0, 100) < 66)
            currentAnim = "victimAWalk_bag";
        else
            currentAnim = "victimAWalk_normal";

        anim.Play(currentAnim);
    }
    public void Steal()
    {
        if (state == states.STOLEN) return;
        if (state == states.CRASHED) return;
        state = states.STOLEN;
        if (currentAnim == "victimAWalk_phone")
            anim.Play("victimAPung_phone");
        else if (currentAnim == "victimAWalk_bag")
            anim.Play("victimAPung_bag");
        else if (currentAnim == "victimAWalk_normal")
            anim.Play("victimAPung_normal");
        clothes.Undress();

        Events.OnAddCoins(laneId, transform.localPosition.x);
    }
    override public void OnCrashed()
    {
        if (state == states.CRASHED) return;
    }
    override public void OnExplote()
    {
        if (state == states.CRASHED) return;
        state = states.CRASHED;
        anim.Play("crashed");
        collider.enabled = false;
    }
    
}
