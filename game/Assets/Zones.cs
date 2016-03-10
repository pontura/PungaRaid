using UnityEngine;
using System.Collections;

public class Zones : MonoBehaviour {

    public GameObject container;
    private ZonesManager zonesManager;

	void Start () {
        Invoke("Delay", 0.1f);
	}
    void Delay()
    {
        zonesManager = Data.Instance.GetComponent<ZonesManager>();
        int id = 1;
        foreach (ZoneButton button in container.GetComponentsInChildren<ZoneButton>())
        {
            ZonesManager.Data data = zonesManager.GetData(id);
            button.Init(data.unlocked);
            id++;
        }
    }
    public void Clicked(int id)
    {
        print("clicked" + id);
        Data.Instance.LoadLevel("03_PreloadingGame");
    }
}
