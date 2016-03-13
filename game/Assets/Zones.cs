using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Zones : MonoBehaviour {

    public Text score;
    public Text username;
    public GameObject container;
    private ZonesManager zonesManager;

	void Start () {
        Events.OnMusicChange("Raticity");
        score.text = "$" + SocialManager.Instance.userHiscore.totalScore;

        if (SocialManager.Instance.userData.logged)
            username.text = SocialManager.Instance.userData.username;

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
