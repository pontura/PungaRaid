using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void GotoGame () {
        print("ASD");
        Data.Instance.LoadLevel("04_Game");
	}
    public void OnSettings()
    {
        Events.OnSettings();
    }
}
