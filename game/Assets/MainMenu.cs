using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public RankingUI ranking;

    void Start()
    {
        if (ranking == null) return;

        if (SocialManager.Instance.userData.logged)
        {
            ranking.gameObject.SetActive(true);
        }
        else
        {
            ranking.gameObject.SetActive(false);
        }
    }
	public void GotoGame () {
        Data.Instance.LoadLevel("04_Game");
	}
    public void OnSettings()
    {
        Events.OnSettings();
    }
}
