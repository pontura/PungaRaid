using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour {

    public GameObject live1;
    public GameObject live2;
    public GameObject live3;
    public GameObject GameOver;

    public int lives;

	void Start () {
        lives = 3;
        Events.OnHeroCrash += OnHeroCrash;
        GameOver.SetActive(false);
	}
    void OnDestroy()
    {
        Events.OnHeroCrash -= OnHeroCrash;
    }
    void OnHeroCrash()
    {
        lives--;
        switch (lives)
        {
            case 1:
                live3.SetActive(false);
                live2.SetActive(false);
                break;
            case 2:
                live3.SetActive(false);
                break;
            default:                
                GameOver.SetActive(true);
                Events.OnGameOver();
                break;
        }
    }
    
}
