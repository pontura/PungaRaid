using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Parse;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public class UserHiscore : MonoBehaviour {

    private string TABLE = "Ranking";
    public int hiscore;

	void Start () {
        SocialEvents.OnNewHiscore += OnNewHiscore;
        SocialEvents.OnFacebookLogin += OnFacebookLogin;
        //hiscore = PlayerPrefs.GetInt("UserHiscore", 0);
        hiscore = 0;
	}
    void OnFacebookLogin()
    {
        TryToGetHiscore();
    }
    void TryToGetHiscore()
    {      
        if (hiscore == 0)
        {
            print(" TryToGetHiscore" + SocialManager.Instance.userData.facebookID);
            LoadFromParse(
                   ParseObject.GetQuery(TABLE)
                  .WhereEqualTo("facebookID", SocialManager.Instance.userData.facebookID)
                  .Limit(1)
              );
            Invoke("TryToGetHiscore", 1);
        }
    }
    void LoadFromParse(ParseQuery<ParseObject> query)
    {
        print("LoadFromParse" + query);
        query.FindAsync().ContinueWith(t =>
        {
            IEnumerable<ParseObject> results = t.Result;
            foreach (var result in results)
                hiscore = result.Get<int>("score");
        }
       );
    }
    void OnNewHiscore(int score)
    {
        
        if (!SocialManager.Instance.userData.logged) return;
        if (hiscore == 0)
        {
            ParseObject parseObj = new ParseObject(TABLE);

            parseObj["facebookID"] = SocialManager.Instance.userData.facebookID;
            parseObj["score"] = score;
            parseObj["playerName"] = SocialManager.Instance.userData.username;
            parseObj.SaveAsync();
            
            Debug.Log("OnNewHiscore!");
        }
        else
        {
            var query = new ParseQuery<ParseObject>(TABLE)
                .WhereEqualTo("facebookID", SocialManager.Instance.userData.facebookID);
            query.FindAsync().ContinueWith(t =>
            {
                IEnumerator<ParseObject> enumerator = t.Result.GetEnumerator();
                enumerator.MoveNext();
                var data = enumerator.Current;
                data["score"] = score;
                return data.SaveAsync();
            }).Unwrap().ContinueWith(t =>
            {
                Debug.Log("OnNewHiscore updated!");
            });
        }
        PlayerPrefs.SetInt("UserHiscore", score);
        this.hiscore = score;
         
    }
}
