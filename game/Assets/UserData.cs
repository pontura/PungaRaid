using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Soomla.Store;


public class UserData : MonoBehaviour
{
    public bool logged;
    public string facebookID;
    public string username;
    public int score;

    [Serializable]
    public class FacebookUserData
    {
        public string facebookID;
        public string username;
    }

    public void Init()
    {
        //RegisterUser("", "", "");

        if (PlayerPrefs.GetString("username") != "" && PlayerPrefs.GetString("facebookID") != "")
            SetUser(PlayerPrefs.GetString("username"), PlayerPrefs.GetString("facebookID"), PlayerPrefs.GetInt("score"));

#if UNITY_EDITOR
        SetUser("", "", 0);
#endif

    }
    void SetUser(string username, string facebookID, int score)
    {
        this.facebookID = facebookID;
        this.username = username;
        this.score = score;
        if (username != "")
            logged = true;
    }
    public void RegisterUser(string username, string facebookID, string score)
    {
        print("RegisterUser username: " + username + " + facebookID + " + facebookID + " + score + " + score);
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("facebookID", facebookID);
        PlayerPrefs.SetInt("email", int.Parse(score));
        SetUser(username, facebookID, int.Parse(score));
    }
    public void Reset()
    {
        logged = false;
        facebookID = "";
        username = "";
        score = 0;
    }
    public void ResetFacebookFriends()
    {
        print("ResetFacebookFriends");
       // FacebookFriends.Clear();
    }
    
}
