using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System;
using Facebook;
using Facebook.MiniJSON;
using System.Linq;

public class FacebookLogin : MonoBehaviour
{
   

    void Start()
    {
       // SocialEvents.OnFacebookLogin += OnFacebookLogin;
    }
    void OnDestroy()
    {
      //  SocialEvents.OnFacebookLogin -= OnFacebookLogin;
    }
    void OnFacebookLogin()
    {
       // Data.Instance.Load("LevelSelector");
    }
    public void FBLogin()
    {
       // SocialManager.Instance.loginManager.FBLogin();
    }
    public void Back()
    {
      //  Data.Instance.Back();
    }
}
