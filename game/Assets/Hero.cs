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
        SORETE,
        CELEBRATE,
        DEAD,        
        WIN,
        CHUMBO_RUN,
        CHUMBO_FIRE

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
        Run();
    }
    void OnHeroCrash()
    {
        Crash();
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
        Invoke("EndAnimation", 0.6f);
    }
    public void OnSorete()
    {
        if (Game.Instance.state != Game.states.PLAYING) return;
        if (state == states.CRASH) return;
        state = states.SORETE;
      //  animator.SetBool(state.ToString(), true);
        animator.Play("pungaShit", 0, 0);
        Invoke("EndAnimation", 0.6f);
    }
    void EndAnimation()
    {
        switch (state)
        {   
            case states.SORETE:         Run();          break;
            case states.DASH:           Run();          break;
            case states.CHUMBO_FIRE:    ChumboRun();    break;
        }
    }
    void Crash()
    {
        if (state == states.CRASH) return;
        state = states.CRASH;
       // animator.SetBool(state.ToString(), true);
        animator.Play("pungaCrash",0,0);
    }
    public void Run()
    {
        state = states.RUN;
        animator.Play("pungaRun", 0, 0);
    }
    public void ChumboRun()
    {
        state = states.CHUMBO_RUN;
        animator.Play("pungaRunMegachumbo", 0, 0);
    }
    public void ChumboFire()
    {
        state = states.CHUMBO_FIRE;
        animator.Play("pungaFireMegachumbo", 0, 0);
        Invoke("EndAnimation", 0.5f);
    }
    void Celebrate()
    {

    }
    void OnLevelComplete()
    {

    }
    public void Dash()
    {

    }
    public void Die()
    {

    }
    public void ResetAnimation()
    {

    }
}
