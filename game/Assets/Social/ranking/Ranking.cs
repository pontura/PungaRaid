using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Parse;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public class Ranking : MonoBehaviour {

    public List<RankingData> data;
    private string TABLE = "Ranking";

    [Serializable]
    public class RankingData
    {
        public int score;
        public string facebookID;
        public string playerName;
    }
    void Start()
    {
        SocialEvents.OnFacebookFriends += OnFacebookFriends;
    }
    public void OnFacebookFriends()
    {
        LoadData(true, 
              ParseObject.GetQuery(TABLE)
              .WhereContainedIn("facebookID", SocialManager.Instance.facebookFriends.ids)
             .OrderByDescending("score")
             .Limit(99)
         );
    }
    void LoadData(bool _received, ParseQuery<ParseObject> query)
    {
        data.Clear();
        Debug.Log("RankingData     ________LoadData");
        query.FindAsync().ContinueWith(t =>
        {
            IEnumerable<ParseObject> results = t.Result;
            foreach (var result in results)
            {
                string facebookID = result.Get<string>("facebookID");
                int score = result.Get<int>("score");
                string playerName = result.Get<string>("playerName");

                RankingData playerData = new RankingData();

                playerData.facebookID = facebookID;
                playerData.playerName = playerName;
                playerData.score = score;

                data.Add(playerData);
            }
        }
        );
    }
}
