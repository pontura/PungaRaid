using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PreloadingGame : MonoBehaviour {

    public string[] randomTexts;
    public Text field;

	void Start () {
        Events.OnMusicChange("");
        Invoke("StartGame", 5);
        Resources.UnloadUnusedAssets();
        field.text = randomTexts[Random.Range(0, randomTexts.Length)];
	}
    void StartGame()
    {
        Data.Instance.LoadLevel("04_Game");
    }
}
