using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public RankingUI ranking;
    public GameObject connect;

    void Start()
    {
        Events.OnMusicChange("Gameplay");

        if (ranking == null) return;

        if (SocialManager.Instance.userData.logged)
        {
            ranking.gameObject.SetActive(true);
            connect.gameObject.SetActive(false);
        }
        else
        {
            ranking.gameObject.SetActive(false);
            connect.gameObject.SetActive(true);
        }
    }
    public void Connect()
    {
        Events.OnLoginAdvisor();
    }
	public void GotoGame () {
        Data.Instance.LoadLevel("03_PreloadingGame");
	}
    public void OnSettings()
    {
        Events.OnSettings();
    }
}
