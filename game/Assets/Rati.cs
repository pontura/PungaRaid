using UnityEngine;
using System.Collections;

public class Rati : Enemy {

    public states state;

    public enum states
    {
        IDLE,
        WALKING,
        STOLEN,
        CRASHED
    }

    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
        this.laneId = laneId;
    }

    public override void OnCrashed()
    {
        anim.Play("copShieldCollide");
    }
    override public void OnExplote()
    {
        if (state == states.CRASHED) return;
        state = states.CRASHED;
        anim.Play("crashed");
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
