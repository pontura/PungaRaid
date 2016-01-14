﻿using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

    void Start()
    {
        Invoke("GotoGame", 4);
    }
	public void GotoGame () {
        Data.Instance.LoadLevel("02_MainMenu");
	}
    public void OnSettings()
    {
        Events.OnSettings();
    }
}
