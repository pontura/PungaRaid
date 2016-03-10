using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public void Connect()
    {
        Events.OnLoginAdvisor();
    }
	public void GotoGame () {
        Data.Instance.LoadLevel("02_Map");
	}
    public void OnSettings()
    {
        Events.OnSettings();
    }
}
