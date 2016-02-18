﻿using UnityEngine;
using System.Collections;

public static class SocialEvents {

    public static System.Action OnFacebookFriends = delegate { };
    public static System.Action OnFacebookNotConnected = delegate { };
    public static System.Action OnFacebookInviteFriends = delegate { };
    public static System.Action OnFacebookLogin = delegate { };
    public static System.Action<string, string> AddFacebookFriend = delegate { }; 

    public static System.Action OnParseLogin = delegate { };
    public static System.Action<string, float, int> OnParseLoadedScore = delegate { };
    public static System.Action OnLoadLocalData = delegate { };

    //Hiscores:
    public static System.Action<int> OnNewHiscore = delegate { };

    //challenges:

    //facebookID, op_facebookID, score
    public static System.Action<string, string, int> OnChallengeCreate = delegate { };
}