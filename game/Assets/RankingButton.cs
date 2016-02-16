using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RankingButton : MonoBehaviour {

    public ProfilePicture profilePicture;
    public Text scoreField;

	public void Init (string facebookID, int score) {
        scoreField.text = "$" + score.ToString();
        profilePicture.setPicture(facebookID);
	}
}
