using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Parse;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public class UserHiscore : MonoBehaviour {

    public int hiscore;

	void Start () {
        SocialEvents.OnNewHiscore += OnNewHiscore;
        hiscore = PlayerPrefs.GetInt("UserHiscore", 0);
        hiscore = 0;
	}

    void OnNewHiscore(int score)
    {
        this.hiscore = score;
        PlayerPrefs.SetInt("UserHiscore", score);

        if (!SocialManager.Instance.userData.logged) return;

       print("OnNewHiscore:::::::::: " + score);

        var query = new ParseQuery<ParseObject>("User")
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
}
