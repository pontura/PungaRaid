using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	public void GotoGame () {
        Data.Instance.LoadLevel("04_Game");
	}
    public void OnSettings()
    {
        Events.OnSettings();
    }
}
