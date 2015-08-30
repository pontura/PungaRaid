using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int laneId;
    public Animator anim;
    private float speed;
    public states state;
    public float distance;

    public enum states
    {
        WALKING, 
        STOLEN
    }

    public void Init(EnemySettings settings, int laneId)
    {
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
        if (transform.localPosition.x + 5 < Game.Instance.gameManager.distance)
            Destroy(gameObject);

        if (state == states.STOLEN) return;
        Vector3 pos = transform.localPosition;
        pos.x -= speed;
        transform.localPosition = pos;
    }
    public void Steal()
    {
        if (state == states.STOLEN) return;
        state = states.STOLEN;
        anim.Play("victimAPung_phone");
    }
}
