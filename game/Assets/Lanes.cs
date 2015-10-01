﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lanes : MonoBehaviour {

    //public GameObject background;
   // public List<GameObject> backgrounds;
    public Lane[] all;
    public int laneActiveID = 3;
    public GameObject enemy;

	void Start () {
	    
	}
    public Lane GetActivetLane()
    {
        return all[laneActiveID];
    }
    public void AddBackground(string name, int _x)
    {
        //print("AddBackground : " + name + " in " + _x);
        //GameObject go = Instantiate(Resources.Load<GameObject>("backgrounds/" + name)) as GameObject;
        //backgrounds.Add(go);
        //go.transform.SetParent(background.transform);
        //go.transform.localPosition = new Vector3(_x, 0, 0);
        //if (backgrounds.Count > 2)
        //{
        //    GameObject b = backgrounds[0];
        //    Destroy(b);
        //    backgrounds.RemoveAt(0);
        //}
    }
    public void AddObjectToLane(string name, int laneId, int _x, EnemySettings settings )
    {
      //  print("new : " + name);
        Enemy enemy = null;

        switch (name)
        {
            case "Victim":
                enemy = Data.Instance.enemiesManager.GetEnemy("Victim");
                break;
            case "RatiEscudo":
                enemy = Data.Instance.enemiesManager.GetEnemy("RatiEscudo");
                break;
        }
        if (enemy == null)
            return;

        enemy.Init(settings, laneId);
        GameObject go = enemy.gameObject;
        sortInLayersByLane(go, laneId);

      

        go.transform.SetParent(all[laneId].transform);
        go.transform.localPosition = new Vector3(_x, 0, 0);

    }
    public void sortInLayersByLane(GameObject go, int laneId)
    {
         SpriteRenderer[] renderers = go.GetComponentsInChildren<SpriteRenderer>(true);
         foreach (SpriteRenderer sr in renderers)
             sr.sortingLayerName = "lane" + laneId;
    }
    public bool TryToChangeLane(bool up)
    {
        if (up && laneActiveID < all.Length - 1)
        {
            laneActiveID++;
            return true;
        } else if (!up && laneActiveID > 0)
        {
            laneActiveID--;
            return true;
        }
        return false;
    }
}
