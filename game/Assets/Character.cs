using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class Character : MonoBehaviour {

    [SerializeField]
    Hero heroAsset;

    public Hero hero;

    float timeToCrossLane;

    public states state;
    private int posX;
    public GameObject container;

    public enum states
    {
        IDLE,
        CHANGE,
        JUMP,
        CRASH
    }
    public void Init()
    {
        timeToCrossLane = Data.Instance.gameData.timeToCrossLane;
    }
    void Start()
    {
        transform.localScale = new Vector3(0.52f, 0.52f, 0.52f);

        hero = Instantiate(heroAsset) as Hero;
        hero.transform.SetParent(container.transform);

        hero.transform.localPosition = new Vector3(0, 0, 0);

        posX = Data.Instance.gameData.CharacterXPosition;
        Vector3 pos = transform.localPosition;
        pos.x = posX;
        transform.localPosition = pos;
    }
	public void MoveUP()
    {
        Move(2.5f, true);
    }
    public void MoveDown()
    {
        Move(-2.5f, true);
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
       if (enemy)
       {
           if (enemy.laneId == Game.Instance.gameManager.characterManager.lanes.laneActiveID)
               Events.OnHeroCrash();
           enemy.Steal();
       }
    }
}
