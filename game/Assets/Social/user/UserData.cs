using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//using Soomla.Store;


public class UserData : MonoBehaviour {

    public bool logged;
    public string facebookID;
    public string username;
    public string email;

	public void Init () {

#if UNITY_EDITOR
        SetUser("", "", "");
        return;
#endif
        if (PlayerPrefs.GetString("username") != "" && PlayerPrefs.GetString("facebookID") != "")
            SetUser(PlayerPrefs.GetString("username"), PlayerPrefs.GetString("facebookID"), PlayerPrefs.GetString("email"));

	}
    void SetUser(string username, string facebookID, string email)
    {
        this.facebookID = facebookID;
        this.username = username;
        this.email = email;
        if (username.Length > 1)
        {
            print("::::::::::::::" + username + " logged: " + logged);
            logged = true;
        }
        SocialEvents.OnFacebookLogin();
    }
    public void RegisterUser(string username, string facebookID, string email)
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("facebookID", facebookID);
        PlayerPrefs.SetString("email", email);
        SetUser(username, facebookID, email);
    }
    public void Reset()
    {
        logged = false;
        facebookID = "";
        username = "";
        email = "";
    }  
}
