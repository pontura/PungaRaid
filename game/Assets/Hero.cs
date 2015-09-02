using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    private Animator animator;
    public states state;

    public enum states
    {
        IDLE,
        RUN,
        CRASH,
        SLIDE,
        CELEBRATE,
        WIN
    }
    void Start()
    {
        Events.StartGame += StartGame;
        Events.OnHeroCrash += OnHeroCrash;
        Events.OnHeroSlide += OnHeroSlide;
        Events.OnHeroCelebrate += OnHeroCelebrate;
        Events.OnLevelComplete += OnLevelComplete;
        Events.OnGamePaused += OnGamePaused;

        animator = GetComponent<Animator>();
    }
    void OnDestroy()
    {
        Events.StartGame -= StartGame;
        Events.OnHeroCrash -= OnHeroCrash;
        Events.OnHeroSlide -= OnHeroSlide;
        Events.OnHeroCelebrate -= OnHeroCelebrate;
        Events.OnLevelComplete -= OnLevelComplete;
        Events.OnGamePaused -= OnGamePaused;
    }
    void OnGamePaused(bool isPaused)
    {
        //if(!isPaused)
        //    collider2d.enabled = true;
    }
    void StartGame()
    {
        ResetAnimation();
    }
    void OnHeroSlide(int id)
    {
        Slide();
    }
    void OnHeroCrash()
    {
        Crash();
       // collider.enabled = false;
    }
    void OnHeroCelebrate()
    {
        Celebrate();
    }
    void Slide()
    {
        if (Game.Instance.state != Game.states.PLAYING) return;
        if (state == states.SLIDE) return;
        if (state == states.CRASH) return;
        state = states.SLIDE;
        animator.SetBool(state.ToString(), true);
    }    
    void Crash()
    {
        if (state == states.CRASH) return;
        state = states.CRASH;
       // animator.SetBool(state.ToString(), true);
        animator.Play("pungaCrash",0,0);
    }   
    void Celebrate()
    {
        state = states.CELEBRATE;
        animator.SetBool(state.ToString(), true);
    }
    void OnLevelComplete()
    {
        if (state == states.WIN) return;
        state = states.WIN;
        animator.SetBool(state.ToString(), true);
    }
    public void ResetAnimation()
    {
        state = states.RUN;
        animator.SetBool("RUN", true);
        animator.SetBool("CRASH", false);
        animator.SetBool("SLIDE", false);
        animator.SetBool("LAVA", false);
        animator.SetBool("CELEBRATE", false);
        animator.SetBool("IDLE", false);
    }
}
