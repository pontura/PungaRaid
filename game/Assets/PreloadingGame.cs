using UnityEngine;
using System.Collections;

public class PreloadingGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Events.OnMusicChange("");
        Invoke("StartGame", 2);
	}
    void StartGame()
    {
        Data.Instance.LoadLevel("04_Game");
    }
}
