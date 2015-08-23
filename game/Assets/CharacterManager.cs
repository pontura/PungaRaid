using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

    public Character character;
    public Lanes lanes;

    public void Init()
    {
        Events.OnSwipe += OnSwipe;
        Events.OnChangeingLane += OnChangeingLane;
        Events.OnChangeLaneComplete += OnChangeLaneComplete;

        OnChangeLaneComplete();
        character.Init();
    }
    public void OnDestroy()
    {
        Events.OnSwipe -= OnSwipe;
        Events.OnChangeingLane -= OnChangeingLane;
        Events.OnChangeLaneComplete += OnChangeLaneComplete;
    }
    public void UpdatePosition(float _x)
    {
       Vector3 pos = character.transform.position;
       pos.x = _x;
       character.transform.position = pos;
    }
    void OnSwipe(SwipeDetector.directions direction)
    {
        if ( Game.Instance.state != Game.states.PLAYING ) return;
        if (character.state == Character.states.CHANGE) return;

        switch (direction)
        {
            case SwipeDetector.directions.UP:
                if (lanes.TryToChangeLane(true))
                    character.MoveUP(); 
                break;
            case SwipeDetector.directions.DOWN:
                if (lanes.TryToChangeLane(false))
                    character.MoveDown(); 
               break;
        }
    }
    void OnChangeingLane()
    {
        if (lanes.GetActivetLane())
        {
            //character.transform.parent = lanes.GetActivetLane().gameObject.transform;
            //character.GotoCenterOfLane();
        }
    }
    void OnChangeLaneComplete()
    {
        character.transform.parent = lanes.GetActivetLane().gameObject.transform;
        character.GotoCenterOfLane();
        lanes.sortInLayersByLane(character.gameObject, lanes.GetActivetLane().id);
    }
    
}
