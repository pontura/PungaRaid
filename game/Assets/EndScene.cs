﻿using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour {

    public RatiEnding ratiEndingAsset;
    private RatiEnding ratiEnding;
    private int MAXCOPS = 12;
    private int totalCops = 0;

	void Start () {
        Events.OnHeroDie += OnHeroDie;        
	}
    void OnDestroy()
    {
        Events.OnHeroDie -= OnHeroDie;

    }
    void OnHeroDie()
    {
        Invoke("LoopAddCop", 2);
        Events.OnMusicChange("Menu");
    }
    void LoopAddCop()
    {
        totalCops++;
        if (totalCops > 10) return;
        AddRandomRati();
        float num = Random.Range(100, 500);
        Invoke("LoopAddCop", (num / 100)+(Time.deltaTime*100));
    }
	
	void AddRandomRati () {
        ratiEnding = Instantiate(ratiEndingAsset);
        Character character = Game.Instance.GetComponent<CharacterManager>().character;
        Lane lane = Game.Instance.GetComponent<LevelsManager>().lanes.GetActivetLane();
        ratiEnding.transform.SetParent(lane.gameObject.transform);

        Vector3 scale = character.transform.localScale;

        int numn = Random.Range(0, 100);
        int posRandom = Random.Range(0, 5);

        if (totalCops == 1) numn = 1;

        if (numn > 50)
        {
            posRandom *= -1;
            scale.x *= -1;
        }

        Vector3 pos = character.transform.localPosition;
        pos.x += posRandom;
        ratiEnding.transform.localPosition = pos;

        ratiEnding.transform.localScale = scale;

        SetLane(lane.id);
        ratiEnding.GetComponent<Animation>().Play();
	}
    void SetLane(int laneID)
    {
        foreach (SpriteRenderer sprite in ratiEnding.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.sortingLayerName = "lane" + laneID;
        }
    }
}