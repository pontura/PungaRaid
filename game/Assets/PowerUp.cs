using UnityEngine;
using System.Collections;

public class PowerUp : Enemy {

    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
        this.laneId = laneId;
    }

    public void Activate()
    {
        Events.OnPowerUp(1);
        Pool();
    }
}
