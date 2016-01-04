﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemiesManager : MonoBehaviour {

    public EnemyPool[] enemiesToPool;
    public EnemyPool[] veredasToPool;

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
                AddToPool(enemyPool);
        }
        foreach (EnemyPool enemyPool in veredasToPool)
        {
            for (int a = 0; a < enemyPool.qty; a++)
                AddToPool(enemyPool);
        }
	}
    void AddToPool(EnemyPool enemyPool)
    {
        Enemy newEnemy = Instantiate(enemyPool.enemy) as Enemy;
        pool.Add(newEnemy);
        newEnemy.isPooled = true;
        newEnemy.transform.SetParent(poolObjects.transform);
        newEnemy.gameObject.SetActive(false);
    }
    public Enemy GetEnemy(string type)
    {
        Enemy enemy = GetPooled(type);
        enemy.gameObject.SetActive(true);
        enemy.Init(new EnemySettings(), 0);
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
                    case "ObstacleGeneric":
                        if (enemy.GetComponent<Obstacle>())
                            return enemy;
                        break;
                    case "Victim":
                        if (enemy.GetComponent<Victim>())
                            return enemy;
                        break;
                    case "RatiJump":
                        if (enemy.GetComponent<RatiJump>())
                            return enemy;
                        break;
                    case "RatiEscudo":
                        if (enemy.GetComponent<Rati>())
                            return enemy;
                        break;
                    case "PowerUp":
                        if (enemy.GetComponent<PowerUp>())
                            return enemy;
                        break;
                    case "Blocker":
                        if (enemy.name == "Blocker" || enemy.name == "Blocker(Clone)")  
                            return enemy;
                        break;
                    case "vereda_5_lanes":
                        if (enemy.name == "vereda_5_lanes" || enemy.name == "vereda_5_lanes(Clone)")
                            return enemy;
                        break;
                    case "veredaIntro":
                        if (enemy.name == "veredaIntro" || enemy.name == "veredaIntro(Clone)")
                            return enemy;
                        break;
                    case "vereda_10mts_5lanes":
                        if (enemy.name == "vereda_10mts_5lanes" || enemy.name == "vereda_10mts_5lanes(Clone)")
                            return enemy;
                        break;
                    case "vereda_2_lanes":
                        if (enemy.name == "vereda_2_lanes" || enemy.name == "vereda_2_lanes(Clone)")
                            return enemy;
                        break;
                    case "vereda_3_lanes":
                        if (enemy.name == "vereda_3_lanes" || enemy.name == "vereda_3_lanes(Clone)")
                            return enemy;
                        break;
                }                
            }
        }

        print("FALTAN:  " + type + " -  pool.Count : " +  pool.Count);
        return pool[0];
    }
    public void Pool(Enemy enemy)
    {
        enemy.isPooled = true;
        enemies.Remove(enemy);
        pool.Add(enemy);
        enemy.transform.SetParent(poolObjects.transform);
        enemy.gameObject.SetActive(false);
    }
}