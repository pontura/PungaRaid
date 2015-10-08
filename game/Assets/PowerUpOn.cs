using UnityEngine;
using System.Collections;

public class PowerUpOn : MonoBehaviour {

    public string clip_init;
    public string clip_loop;
    public int lives;

    void Start()
    {
        Events.OnHeroCrash += OnHeroCrash;
    }
    void OnDestroy()
    {
        Events.OnHeroCrash -= OnHeroCrash;
    }
    void OnHeroCrash()
    {
        Events.OnExplotion();
        lives--;
        print("CRASH");
        if (lives <= 0)
            Events.OnHeroPowerUpOff();
    }
    public void Init(int lives)
    {
        this.lives = lives;
        OnInit();
    }
    public virtual void OnInit() { }

    public void Loop()
    {
      //  GetComponent<Animator>().Play("clip_loop");
    }
}
