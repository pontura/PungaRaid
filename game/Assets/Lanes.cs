using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lanes : MonoBehaviour {

    //public GameObject background;
   // public List<GameObject> backgrounds;
    public Lane[] all;
    public int laneActiveID = 3;
    public GameObject enemy;

	void Start () {
        Events.OnPoolAllItemsInScene += OnPoolAllItemsInScene;
	}
    void OnDestroy()
    {
        Events.OnPoolAllItemsInScene -= OnPoolAllItemsInScene;
    }
    void OnPoolAllItemsInScene()
    {
        List<Enemy> enemies = new List<Enemy>();
        foreach (Lane lane in all)  
        {
            foreach (Transform child in lane.transform)
            {
                if (child.GetComponent<Enemy>())
                {
                    Enemy enemy = child.GetComponent<Enemy>();
                    if (enemy)
                        enemies.Add(enemy);
                }
            }
        }
        foreach (Enemy enemy in enemies)
        {
            Data.Instance.enemiesManager.Pool(enemy);
        }
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
            case "ObstacleGeneric":
                enemy = Data.Instance.enemiesManager.GetEnemy("ObstacleGeneric");
                break;
            case "Victim":
                enemy = Data.Instance.enemiesManager.GetEnemy("Victim");
                break;
            case "RatiJump":
                enemy = Data.Instance.enemiesManager.GetEnemy("RatiJump");
                break;
            case "RatiEscudo":
                enemy = Data.Instance.enemiesManager.GetEnemy("RatiEscudo");
                break;
            case "PowerUp":
                enemy = Data.Instance.enemiesManager.GetEnemy("PowerUp");
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
    public void changeEnemyLane(Enemy enemy, Lane lane)
    {
        enemy.transform.SetParent(lane.transform);
        sortInLayersByLane(enemy.gameObject, lane.id);

        Vector2 pos = enemy.transform.localPosition;  
        pos.y = 0;
        enemy.transform.localPosition = pos;

        enemy.laneId = lane.id;
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
