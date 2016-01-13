﻿using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

    public Character character;
    public Lanes lanes;

    public void Init()
    {
        Events.OnSwipe += OnSwipe;
       // Events.OnChangeLane += OnChangeLane;
        Events.OnChangeLaneComplete += OnChangeLaneComplete;

        OnChangeLaneComplete();
        character.Init();
    }
    public void OnDestroy()
    {
        Events.OnSwipe -= OnSwipe;
       // Events.OnChangeLane -= OnChangeLane;
        Events.OnChangeLaneComplete -= OnChangeLaneComplete;
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

        switch (direction)
        {
            case SwipeDetector.directions.UP:
                if (!character.CantMoveUp && lanes.TryToChangeLane(true))
                    character.MoveUP(); 
                break;
            case SwipeDetector.directions.DOWN:
                if (!character.CantMoveDown && lanes.TryToChangeLane(false))
                    character.MoveDown(); 
               break;
            case SwipeDetector.directions.RIGHT:
               character.Dash();
               break;
        }
    }
    //void OnChangeingLane()
    //{
    //    if (lanes.GetActivetLane())
    //    {
    //        //character.transform.parent = lanes.GetActivetLane().gameObject.transform;
    //        //character.GotoCenterOfLane();
    //    }
    //}
    void OnChangeLaneComplete()
    {
        character.transform.parent = lanes.GetActivetLane().gameObject.transform;
        character.GotoCenterOfLane();
        lanes.sortInLayersByLane(character.gameObject, lanes.GetActivetLane().id);
    }
    
}
