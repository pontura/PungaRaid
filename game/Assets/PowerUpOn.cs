using UnityEngine;
using System.Collections;

public class PowerUpOn : MonoBehaviour {

    public string clip_init;
    public string clip_loop;
   // public int lives;
   // private int totalLives;

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
       // lives--;
       // float percent = (float)lives / (float)totalLives;
      //  Events.OnSetBar( percent );
      //  if (lives <= 0)
       //     Events.OnHeroPowerUpOff();
    }
    public void Init(int lives)
    {
      //  this.totalLives = lives;
      //  this.lives = lives;
        OnInit();
    }
    public virtual void OnInit() { }

}
