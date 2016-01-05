using UnityEngine;
using System.Collections;

public class CharacterBar : MonoBehaviour {

    public GameObject container;

	void Start () {
        Events.OnBarInit += OnBarInit;
        Events.OnBarReady += OnBarReady;
        Events.OnSetBar += OnSetBar;
        
        gameObject.SetActive(false);
	}
    void OnDestroy()
    {
        Events.OnBarInit -= OnBarInit;
        Events.OnBarReady -= OnBarReady;
        Events.OnSetBar -= OnSetBar;
    }
    void OnBarInit()
    {
        OnSetBar(1);
        gameObject.SetActive(true);
	}
    void OnBarReady()
    {
        gameObject.SetActive(false);
    }
    void OnSetBar(float qty)
    {
        print("___SET BAR: " + qty);
        container.transform.localScale = new Vector3(qty, 1, 1);
    }
}
