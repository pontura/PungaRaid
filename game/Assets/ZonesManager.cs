using UnityEngine;
using System.Collections;
using System;

public class ZonesManager : MonoBehaviour {

    public Data[] all;

    [Serializable]
    public class Data
    {
        public string name;
        public int id;
        public bool unlocked;
    }
	void Start () {
        int id = 1;
        foreach (Data data in all)
        {
            if (PlayerPrefs.GetString("zone" + id) == "true")
                data.unlocked = true;
            id++;
        }
        Events.OnUnlockZone += OnUnlockZone;
	}
    public Data GetData(int id)
    {
        return all[id - 1];
    }
    void OnUnlockZone(int id)
    {
        PlayerPrefs.SetString("zone" + id, "true");
        all[id - 1].unlocked = true;
	}
}
