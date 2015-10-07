using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class Character : MonoBehaviour {

    [SerializeField]
    Hero heroAsset;

    public int TOTAL_LIVES = 3;
    public int lives;
    public Hero hero;

    float timeToCrossLane;

    public states state;
    private int posX;
    public GameObject container;
    public GameObject heroContainer;
    public GameObject powerUpsContainer;

    public PowerUpOn powerUp1;
    private PowerUpOn powerUp;

    public enum states
    {
        IDLE,
        CHANGE,
        CRASH,
        INDESTRUCTIBLE
    }
    public void Init()
    {
        timeToCrossLane = Data.Instance.gameData.timeToCrossLane;
    }
    void Start()
    {
        lives = TOTAL_LIVES;
        transform.localScale = new Vector3(0.52f, 0.52f, 0.52f);

        hero = Instantiate(heroAsset) as Hero;
        hero.transform.SetParent(heroContainer.transform);

        hero.transform.localPosition = new Vector3(0, 0, 0);

        posX = Data.Instance.gameData.CharacterXPosition;
        Vector3 pos = transform.localPosition;
        pos.x = posX;
        transform.localPosition = pos;

        Events.OnPowerUp += OnPowerUp;
    }
    void OnDestroy()
    {
        Events.OnPowerUp -= OnPowerUp;
    }
    void OnPowerUp(int id)
    {
        if (state == states.INDESTRUCTIBLE) return;
        heroContainer.transform.localPosition = new Vector3(-1000, 0, 0);
        print("OnPowerUp " + id);
        state = states.INDESTRUCTIBLE;
        powerUp = Instantiate(powerUp1) as PowerUpOn;
        powerUp.transform.SetParent(powerUpsContainer.transform);
        powerUp.transform.localScale = Vector3.one;
        powerUp.transform.localPosition = Vector3.zero;

        Vector3 pos = hero.transform.localPosition;
        pos.x = -100;
        hero.transform.localPosition = pos;
        Invoke("PowerupOff", 5);
    }
    void PowerupOff()
    {
        heroContainer.transform.localPosition = Vector3.zero;
        Destroy(powerUp.gameObject);
    }
	public void MoveUP()
    {
        Move(2.5f, true);
    }
    public void MoveDown()
    {
        Move(-2.5f, true);
    }
    void Idle()
    {
        state = states.IDLE;
    }
    public void GotoCenterOfLane()
    {
        Vector3 pos = transform.localPosition;
        pos.y = 0;
        transform.localPosition = pos;
       // if (hero)
            container.transform.localPosition = Vector3.zero;
        state = states.IDLE;
    }
    private void Move(float _y, bool firstStep)
    {
        Events.OnSoundFX("changeLane");
        state = states.CHANGE;
        TweenParms parms = new TweenParms();
        parms.Prop("localPosition", new Vector3(0,_y,0));
        parms.Ease(EaseType.Linear);

        parms.OnComplete(OnChangeLaneComplete);
        HOTween.To(container.transform, timeToCrossLane, parms);
    }
    void OnChangeLaneComplete()
    {
        Events.OnChangeLaneComplete();        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
       Enemy enemy = other.GetComponent<Enemy>();       
       if (enemy && !enemy.isPooled)
       {
           print("other " + other.name);
           print("enemy " + enemy + " _ " + enemy.laneId + " " + Game.Instance.gameManager.characterManager.lanes.laneActiveID);
           if (enemy.GetComponent<PowerUp>())
           {
               enemy.GetComponent<PowerUp>().Activate();
           } else 
           if (enemy.laneId == Game.Instance.gameManager.characterManager.lanes.laneActiveID)
           {
               OnHeroCrash();
               enemy.Crashed();
           }
           else
           {
               if (enemy.GetComponent<Victim>())
               {
                   enemy.GetComponent<Victim>().Steal();
                   Events.OnSoundFX("Pung");
               }
           }
       }
    }
    void OnHeroCrash()
    {
        Events.OnSoundFX("Crash");

        lives--;
        if(lives <1)
            Events.OnHeroDie();
        else
            Events.OnHeroCrash();

    }
}
