using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesManager : MonoBehaviour {

    public int qty;
    public List<Enemy> enemies;
    public List<Enemy> pool;
    public Enemy enemy;

	void Start () {
        for (int a = 0; a < qty; a++)
        {
            Enemy newEnemy = Instantiate(enemy) as Enemy;
            pool.Add(newEnemy);
        }
	}
    public Enemy GetEnemy()
    {
        Enemy enemy = pool[0];
        pool.Remove(enemy);
        enemies.Add(enemy);
        return enemy;
    }
    public void Pool(Enemy enemy)
    {
        enemies.Remove(enemy);
        pool.Add(enemy);
    }
}
