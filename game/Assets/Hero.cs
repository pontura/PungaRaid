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
        DASH,
        CELEBRATE,
        DEAD,        
        WIN
    }
    void Start()
    {
        Events.StartGame += StartGame;
        Events.OnHeroCrash += OnHeroCrash;
        Events.OnHeroDash += OnHeroDash;
        Events.OnHeroCelebrate += OnHeroCelebrate;
        Events.OnLevelComplete += OnLevelComplete;
        Events.OnGamePaused += OnGamePaused;
        Events.OnHeroDie += OnHeroDie;

        animator = GetComponent<Animator>();
    }
    void OnDestroy()
    {
        Events.StartGame -= StartGame;
        Events.OnHeroCrash -= OnHeroCrash;
        Events.OnHeroDie -= OnHeroDie;
        Events.OnHeroDash -= OnHeroDash;
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
    void OnHeroCrash()
    {
        Crash();
       // collider.enabled = false;
    }
    void OnHeroCelebrate()
    {
        Celebrate();
    }
    void OnHeroDie()
    {
        if (state == states.DEAD) return;
        state = states.DEAD;
        animator.SetBool(state.ToString(), true);
        print("OnHeroDie");
        animator.Play("pungaDeath", 0, 0);
    }
    void OnHeroDash()
    {
        if (Game.Instance.state != Game.states.PLAYING) return;
        if (state == states.DASH) return;
        if (state == states.CRASH) return;
        state = states.DASH;
        animator.SetBool(state.ToString(), true);
        animator.Play("pungaDash", 0, 0);
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
    public void Dash()
    {

    }
    public void Die()
    {

    }
    public void ResetAnimation()
    {
        state = states.RUN;
        animator.Play("pungaRun", 0, 0);

        animator.SetBool("RUN", true);
        animator.SetBool("CRASH", false);
        animator.SetBool("DASH", false);
        animator.SetBool("SLIDE", false);
        animator.SetBool("LAVA", false);
        animator.SetBool("CELEBRATE", false);
        animator.SetBool("IDLE", false);
    }
}
