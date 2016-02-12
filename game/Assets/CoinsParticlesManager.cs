﻿using UnityEngine;
using System.Collections;

public class CoinsParticlesManager : MonoBehaviour {

    private LevelsManager levelsManager;

	void Start () {
       levelsManager = GetComponent<LevelsManager>();
       Events.OnAddCoins += OnAddCoins;
	}
    void OnAddCoins(int laneID, float distance)
    {
        print("OnAddCoins  lane: " + laneID + " distance: " + distance);
        EnemySettings settings = new EnemySettings();
        levelsManager.lanes.AddObjectToLane("CoinParticles", laneID, (int)distance, settings);
    }
}
