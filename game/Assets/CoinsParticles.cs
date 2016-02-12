using UnityEngine;
using System.Collections;

public class CoinsParticles : Enemy {

    public override void Enemy_Init(EnemySettings settings, int laneId) 
    {
       Vector3 pos =  transform.localPosition;
       transform.localPosition = pos;
    }
}
