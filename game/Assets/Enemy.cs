using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int laneId;
    public Animator anim;
    private float speed;
    public states state;
    public float distance;
    private Clothes clothes;
    private string currentAnim;

    void Start()
    {
        clothes = GetComponent<Clothes>();
    }
    public enum states
    {
        WALKING, 
        STOLEN,
        CRASHED,
        POOLED
    }
    public void Init(EnemySettings settings, int laneId)
    {
        clothes.Restart();
        Walk();
        this.laneId = laneId;
        distance = Game.Instance.gameManager.distance;
        speed = settings.speed;
        Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);

        if(speed <0)
            scale.x *= -1;

        transform.localScale = scale;

        anim.GetComponentInChildren<Animator>();
        anim.SetBool("WALK", true);
    }
    void Update()
    {
        if (state == states.POOLED) return;
        if (transform.localPosition.x + 5 < Game.Instance.gameManager.distance)
        {
            Data.Instance.enemiesManager.Pool(this);
            state = states.POOLED;
            return;
        }

        if (state == states.CRASHED) return;
        if (state == states.STOLEN) return;

        Vector3 pos = transform.localPosition;
        pos.x -= speed;
        transform.localPosition = pos;
    }
    public void Walk()
    {
        state = states.WALKING;
        if(Random.Range(0,100)<50)
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
