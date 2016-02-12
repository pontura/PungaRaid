using UnityEngine;
using System.Collections;

public class CoinsParticles : Enemy {

    public override void Enemy_Init(EnemySettings settings, int laneId) 
    {
       Vector3 pos =  transform.localPosition;
       pos.z = -10;
       pos.y = 2;
       transform.localPosition = pos;
    }
}
