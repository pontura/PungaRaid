using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Animator anim;
    private float speed;
    public states state;

    public enum states
    {
        WALKING, 
        STOLEN
    }

	public void Init (EnemySettings settings) {
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
        if (state == states.STOLEN) return;
        Vector3 pos = transform.localPosition;
        pos.x -= speed;
        transform.localPosition = pos;
    }
    void Steal()
    {
        if (state == states.STOLEN) return;
        state = states.STOLEN;
        anim.SetBool("STEAL", true);

    }
}
