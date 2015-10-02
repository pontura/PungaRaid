using UnityEngine;
using System.Collections;

public class Rati : Enemy {

    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
        this.laneId = laneId;
    }

    public override void OnCrashed()
    {
        anim.Play("copShieldCollide");
    }
}
