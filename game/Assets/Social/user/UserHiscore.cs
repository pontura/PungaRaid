using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class UserHiscore : MonoBehaviour {

    private string TABLE = "Ranking";
    public int totalScore;
    public int barProgress;
    public int hiscore;
    public bool isLoaded;
    public string id = "";
    

	void Start () {
        SocialEvents.OnNewHiscore += OnNewHiscore;
        SocialEvents.OnAddToTotalScore += OnAddToTotalScore;
        SocialEvents.OnSetToTotalBarScore += OnSetToTotalBarScore;
        SocialEvents.OnFacebookLogin += OnFacebookLogin;
        SocialEvents.ResetApp += ResetApp;
        hiscore = PlayerPrefs.GetInt("UserHiscore", 0);
        totalScore = PlayerPrefs.GetInt("totalScore", 0);
        barProgress = PlayerPrefs.GetInt("barProgress", 0);
	}
    void OnFacebookLogin()
    {
        if (!isLoaded)
            LoadHiscoreFromDB();
    }
    void OnAddToTotalScore(int qty)
    {
        totalScore += qty;
        PlayerPrefs.SetInt("totalScore", totalScore);
    }
    void OnSetToTotalBarScore(int total)
    {
        barProgress = total;
        PlayerPrefs.SetInt("barProgress", total);
    }
    void ResetApp()
    {
        totalScore = 0;
        hiscore = 0;
    }
    void LoadHiscoreFromDB()
    {
        string url = SocialManager.Instance.FIREBASE + "/scores.json?orderBy=\"facebookID\"&equalTo=\"" + SocialManager.Instance.userData.facebookID + "\"";

        Debug.Log(url);

        HTTP.Request someRequest = new HTTP.Request("get", url);
        someRequest.Send((request) =>
        {
            Hashtable decoded = (Hashtable)JSON.JsonDecode(request.response.Text);

            isLoaded = true;
            if (decoded == null)
            {
                Debug.Log("no existe el user or malformed response ):");
                return;
            }
            else if (decoded.Count == 0)
            {
                hiscore = 0;
            }
            else
            {
                foreach (DictionaryEntry json in decoded)
                {
                    Hashtable jsonObj = (Hashtable)json.Value;
                    //Score s = new Score();
                    id = (string)json.Key.ToString();
                    hiscore = (int)jsonObj["score"];
                    SetHiscore(hiscore);
                }
            }
        });
    }

    void OnNewHiscore(int score)
    {
        if (hiscore < score)
            SetHiscore(score);

        if (!SocialManager.Instance.userData.logged) return;

        if (id == "")
            AddNewHiscore(score);
        else
            UpdateScore(id, score);
    }
    void SetHiscore(int score)
    {
        hiscore = score;
        PlayerPrefs.SetInt("UserHiscore", score);
    }
    protected void AddNewHiscore(int score)
    {
        Hashtable data = new Hashtable();
        data.Add("facebookID", SocialManager.Instance.userData.facebookID);
        data.Add("playerName", SocialManager.Instance.userData.username);
        data.Add("score", score);

        Hashtable time = new Hashtable();
        time.Add(".sv", "timestamp");
        data.Add("time", time);

        HTTP.Request theRequest = new HTTP.Request("post", SocialManager.Instance.FIREBASE  + "/scores.json", data);

        theRequest.Send((request) =>
        {
            Hashtable jsonObj = (Hashtable)JSON.JsonDecode(request.response.Text);
            if (jsonObj == null)
            {
                Debug.LogError("server returned null or malformed response ):");
            }
            Debug.Log("GRABO NUVEO SCORE");
            hiscore = score;
            //vuelve a levantarlo para grabar el id:
            LoadHiscoreFromDB();
        });
    }
    protected void UpdateScore(string id, int score)
    {
        print("__update score id: " + id + " score: " + score);

        Hashtable data = new Hashtable();
        data.Add("score", score);

        HTTP.Request theRequest = new HTTP.Request("patch", SocialManager.Instance.FIREBASE + "/scores/" + id + "/.json", data);
        theRequest.Send((request) =>
        {
            Hashtable jsonObj = (Hashtable)JSON.JsonDecode(request.response.Text);
            if (jsonObj == null)
            {
                Debug.LogError("server returned null or malformed response ):");
            }
            Debug.Log("score updated: " + request.response.Text);
            this.hiscore = score;
        });
    }
}
