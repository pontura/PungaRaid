﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SocialManager : MonoBehaviour
{
    static SocialManager mInstance = null;

    [HideInInspector]
    public UserData userData;
    [HideInInspector]
    public LoginManager loginManager;
    [HideInInspector]
    public FacebookFriends facebookFriends;
    [HideInInspector]
    public UserHiscore userHiscore;
    [HideInInspector]
    public Ranking ranking;  

    public static SocialManager Instance
    {
        get
        {
            return mInstance;
        }
    }
    public void LoadLevel(string aLevelName)
    {
        Time.timeScale = 1;
        Application.LoadLevel(aLevelName);
    }
    void Awake()
    {
        if (!mInstance)
            mInstance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        loginManager = GetComponent<LoginManager>();
        facebookFriends = GetComponent<FacebookFriends>();
        userData = GetComponent<UserData>();
        userHiscore = GetComponent<UserHiscore>();
        ranking = GetComponent<Ranking>();

        userData.Init();

    }

    public void Reset()
    {

    }
}