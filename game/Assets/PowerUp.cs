using UnityEngine;
using System.Collections;

public class PowerUp : Enemy {

    public int lives;

    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
    }

    public void Activate()
    {
        Events.OnPowerUp(1);
        Pool();
    }
}
