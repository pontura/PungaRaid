using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemiesManager : MonoBehaviour {

    public EnemyPool[] enemiesToPool;

    [Serializable]
    public class EnemyPool
    {
        public Enemy enemy;
        public int qty;
    }
    
    public List<Enemy> enemies;
    public List<Enemy> pool;

    public GameObject poolObjects;

	void Start () {
        poolObjects = new GameObject();
        poolObjects.name = "POOL";

        DontDestroyOnLoad(poolObjects);

        Instantiate(poolObjects);
        poolObjects.transform.localPosition = new Vector3(-1000, 0, 0);
        foreach (EnemyPool enemyPool in enemiesToPool)
        {
            for (int a = 0; a < enemyPool.qty; a++)
            {
                Enemy newEnemy = Instantiate(enemyPool.enemy) as Enemy;
                pool.Add(newEnemy);
                newEnemy.isPooled = true;
                newEnemy.transform.SetParent(poolObjects.transform);
            }
        }
	}
    public Enemy GetEnemy(string type)
    {
        print("t: " + type);
        Enemy enemy = GetPooled(type);
        print("enemy" + enemy);
        pool.Remove(enemy);
        enemies.Add(enemy);
        return enemy;
    }
    private Enemy GetPooled(string type)
    {
        foreach (Enemy enemy in pool)
        {
            if (enemy.isPooled)
            {
                switch (type)
                {
                    case "Victim":
                        if (enemy.GetComponent<Victim>())
                            return enemy;
                        break;
                    case "RatiEscudo":
                        if (enemy.GetComponent<Rati>())
                            return enemy;
                        break;
                }                
            }
        }

        print("te quedaste sin enemys. Hay en pool: " + pool.Count + " type: " + type);
        return pool[0];
    }
    public void Pool(Enemy enemy)
    {
        enemy.isPooled = true;
        enemies.Remove(enemy);
        pool.Add(enemy);
        enemy.transform.SetParent(poolObjects.transform);
    }
}
