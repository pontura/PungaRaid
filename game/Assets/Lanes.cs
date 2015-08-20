using UnityEngine;
using System.Collections;

public class Lanes : MonoBehaviour {

    public GameObject background;
    public Lane[] all;
    public int laneActiveID = 3;

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
	
	void Update () {
	
	}
}
