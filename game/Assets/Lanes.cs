using UnityEngine;
using System.Collections;

public class Lanes : MonoBehaviour {

    public GameObject background;
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
      //  print("AddBackground : " + name + " in " + _x);
        GameObject go = Instantiate(Resources.Load<GameObject>("backgrounds/" + name)) as GameObject;

        go.transform.SetParent(background.transform);
        go.transform.localPosition = new Vector3(_x, 0, 0);
    }
    public void AddObjectToLane(string name, int laneId, int _x, EnemySettings settings )
    {
        GameObject go = null;
        switch (name)
        {
            case "Enemy":
                go = Instantiate(enemy) as GameObject;
                go.GetComponent<Enemy>().Init(settings);
                sortInLayersByLane(go, laneId);
                break;
        }

        if(go == null)
            return;

        go.transform.SetParent(all[laneId - 1].transform);
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
