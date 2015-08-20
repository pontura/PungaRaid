using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    private Animator animator;
    public states state;
    private Collider2D collider2d;

    public enum states
    {
        IDLE,
        RUN,
        JUMP,
        CRASH,
        SLIDE,
        LAVA,
        CELEBRATE,
        UNHAPPY,
        WIN
    }
    void Start()
    {
        Events.StartGame += StartGame;
        Events.OnHeroJump += OnHeroJump;
        Events.OnHeroCrash += OnHeroCrash;
        Events.OnHeroSlide += OnHeroSlide;
        Events.OnHeroCelebrate += OnHeroCelebrate;
        Events.OnHeroUnhappy += OnHeroUnhappy;
        Events.OnLevelComplete += OnLevelComplete;
        Events.OnGamePaused += OnGamePaused;

        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();
    }
    void OnDestroy()
    {
        Events.StartGame -= StartGame;
        Events.OnHeroJump -= OnHeroJump;
        Events.OnHeroCrash -= OnHeroCrash;
        Events.OnHeroSlide -= OnHeroSlide;
        Events.OnHeroCelebrate -= OnHeroCelebrate;
        Events.OnHeroUnhappy -= OnHeroUnhappy;
        Events.OnLevelComplete -= OnLevelComplete;
        Events.OnGamePaused -= OnGamePaused;
    }
    void OnGamePaused(bool isPaused)
    {
        if(!isPaused)
            collider2d.enabled = true;
    }
    void StartGame()
    {
        ResetAnimation();
    }
    void OnHeroJump()
    {
        Jump();
        collider2d.enabled = false;
    }
    void OnHeroSlide(int id)
    {
        if (id == 1)
            Slide();
        else
            Lava();
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
    void OnHeroUnhappy()
    {
        Unhappy();
    }
    void Slide()
    {
        if (Game.Instance.state != Game.states.PLAYING) return;
        if (state == states.SLIDE) return;
        state = states.SLIDE;
        animator.SetBool(state.ToString(), true);
    }
    void Lava()
    {
        if (Game.Instance.state != Game.states.PLAYING) return;
        if (state == states.LAVA) return;
        state = states.LAVA;
        animator.SetBool(state.ToString(), true);
        Events.OnSoundFX("threadLava");
    }
    void Crash()
    {
        if (state == states.CRASH) return;
        state = states.CRASH;
        animator.SetBool(state.ToString(), true);
    }
    void Jump()
    {
        if (Game.Instance.state != Game.states.PLAYING) return;
        if (state == states.JUMP) return;
        Events.OnSoundFX("jump");
        state = states.JUMP;
        animator.SetBool(state.ToString(), true);
    }
    void Celebrate()
    {
        if (state == states.JUMP) return;
        state = states.CELEBRATE;
        animator.SetBool(state.ToString(), true);
    }
    void Unhappy()
    {
        if (state == states.JUMP) return;
        state = states.UNHAPPY;
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
        if (Game.Instance.GetComponent<GameManager>().state == GameManager.states.GAMEOVER) return;
        collider2d.enabled = true;
        state = states.RUN;
        animator.SetBool("JUMP", false);
        animator.SetBool("CRASH", false);
        animator.SetBool("SLIDE", false);
        animator.SetBool("LAVA", false);
        animator.SetBool("CELEBRATE", false);
        animator.SetBool("UNHAPPY", false);
        animator.SetBool("IDLE", false);
    }
    private bool step1;
    public void OnDinoStep()
    {
		return;
        step1 = !step1;
        if(step1)
            Events.OnSoundFX("dinoStep001");
        else
            Events.OnSoundFX("dinoStep002");
    }
}
