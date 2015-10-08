using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class Character : MonoBehaviour {

    [SerializeField]
    Hero heroAsset;

    public int TOTAL_LIVES = 1;
  //  public int lives;
    public Hero hero;

    float timeToCrossLane;

  //  public states state;
    public GameObject container;
    public GameObject heroContainer;
    public GameObject powerUpsContainer;
    private PowerupManager powerupManager;

    //public enum states
    //{
    //    IDLE,
    //    CRASH,
    //    INDESTRUCTIBLE
    //}
    public void Init()
    {
        timeToCrossLane = Data.Instance.gameData.timeToCrossLane;
    }
    void Start()
    {
        powerupManager = GetComponent<PowerupManager>();
        transform.localScale = new Vector3(0.52f, 0.52f, 0.52f);

        hero = Instantiate(heroAsset) as Hero;
        hero.transform.SetParent(heroContainer.transform);

        hero.transform.localPosition = Vector3.zero;
    }
    public void OnSetHeroState( bool show)
    {
        if (!show)
            heroContainer.transform.localPosition = new Vector3(-1000, 0, 0);
        else
            heroContainer.transform.localPosition = Vector3.zero;
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
      //  state = states.IDLE;
    }
    public void GotoCenterOfLane()
    {
        Vector3 pos = transform.localPosition;
        pos.y = 0;
        transform.localPosition = pos;
       // if (hero)
            container.transform.localPosition = Vector3.zero;
       // state = states.IDLE;
    }
    private void Move(float _y, bool firstStep)
    {
        Events.OnSoundFX("changeLane");
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
          // print("other " + other.name);
         //  print("enemy " + enemy + " _ " + enemy.laneId + " " + Game.Instance.gameManager.characterManager.lanes.laneActiveID);
           if (enemy.GetComponent<PowerUp>())
           {
               enemy.GetComponent<PowerUp>().Activate();
           } else 
           if (enemy.laneId == Game.Instance.gameManager.characterManager.lanes.laneActiveID)
           {


               if (powerupManager.type != PowerupManager.types.NONE)
               {
                   Events.OnHeroCrash();
                   enemy.Explote();
               }
               else
               {
                   enemy.Crashed();
                   Events.OnHeroDie();
               }

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
}
