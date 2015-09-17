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

    void Start()
    {
        clothes = GetComponent<Clothes>();
    }
    override public void Enemy_Activate()
    {
        Walk();
        anim.GetComponentInChildren<Animator>();
        anim.SetBool("WALK", true);
    }
    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
        clothes.Restart();
        
        this.laneId = laneId;
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
        if (Random.Range(0, 100) < 50)
            currentAnim = "victimAWalk_phone";
        else
            currentAnim = "victimAWalk_bag";

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
        clothes.Undress();
    }
    public void Creashed()
    {
        if (state == states.CRASHED) return;
        state = states.CRASHED;
        //  anim.Play("victimAPung_phone");
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
