using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Ranking : MonoBehaviour {

    public List<RankingData> data;
    private string TABLE = "Ranking";

    [Serializable]
    public class RankingData
    {
        public int score;
        public string facebookID;
        public string playerName;
        public bool isYou;
    }
    void Start()
    {
        SocialEvents.OnFacebookFriends += OnFacebookFriends;
        SocialEvents.OnRefreshRanking += OnRefreshRanking;
    }
    void OnDestroy()
    {
        SocialEvents.OnFacebookFriends -= OnFacebookFriends;
        SocialEvents.OnRefreshRanking -= OnRefreshRanking;
    }
    public void OnFacebookFriends()
    {
        LoadRanking();
    }
    public void LoadRanking()
    {
        data.Clear();
        string url = SocialManager.Instance.FIREBASE + "/scores.json?orderBy=\"score\"&limitToLast=30";
        Debug.Log("LoadRanking: " + url);
        HTTP.Request someRequest = new HTTP.Request("get", url);
        someRequest.Send((request) =>
        {
            Hashtable decoded = (Hashtable)JSON.JsonDecode(request.response.Text);
            if (decoded == null)
            {
                Debug.LogError("server returned null or     malformed response ):");
                return;
            }

            foreach (DictionaryEntry json in decoded)
            {
                Hashtable jsonObj = (Hashtable)json.Value;
                RankingData newData = new RankingData(); 
               // s.id = (string)json.Key;
                newData.playerName = (string)jsonObj["playerName"];
                newData.score = (int)jsonObj["score"];
                newData.facebookID = (string)jsonObj["facebookID"];
                data.Add(newData);
            }
        });
    }
    void OnRefreshRanking()
    {
        int hiscore = SocialManager.Instance.userHiscore.hiscore;

        bool userExistsInRanking = false;
        foreach(RankingData rankingData in data)
        {
            if (rankingData.facebookID == SocialManager.Instance.userData.facebookID)
            {
                rankingData.score = hiscore;
                userExistsInRanking = true;
                rankingData.isYou = true;
            }
        }
        if (!userExistsInRanking)
        {
            RankingData rankingData = new RankingData();
            rankingData.facebookID =  SocialManager.Instance.userData.facebookID;
            rankingData.score =  hiscore;
            rankingData.playerName =  SocialManager.Instance.userData.username;
            rankingData.isYou = true;
            data.Add(rankingData);
        }
        OrderByScore();
    }
    void OrderByScore()
    {
        print("OrderByScore");
        data = data.OrderBy(go => go.score).Reverse().ToList();
    }
}
