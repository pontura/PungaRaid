using UnityEngine;
using System.Collections;

public class RankingUI : MonoBehaviour {

    public RankingButton button;
    public Transform container;
    public bool loaded;

	void Update () {
        if (loaded) return;
        if (SocialManager.Instance.ranking.data.Count > 0)
        {
            loaded = true;
            foreach(Ranking.RankingData data in  SocialManager.Instance.ranking.data)
            {
                RankingButton newButton = Instantiate(button);
                newButton.transform.SetParent(container.transform);
                newButton.Init(data.facebookID, data.score);
            }
        }
	}
}
