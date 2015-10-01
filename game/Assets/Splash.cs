using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {


	void Update () {
        if (Input.anyKeyDown == true)
        {
            Data.Instance.LoadLevel("04_Game");
        }
	}
}
