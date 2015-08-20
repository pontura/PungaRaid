using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {

    public int speed;
    
    public float timeToCrossLane;

    [Tooltip("X Position Of Hero. 0 is de Center")]
    public int CharacterXPosition;
    public float distanceBetweenWords;

    [Tooltip("Obstacles on or off")]
    public bool Obstacles;
    public float distanceBetweenObstacles;
    public int percentProbabilityObstacleFrom;
    public int percentProbabilityObstacleTo;

    [Tooltip("Offset to separate Obstacles from Words")]
    public float offsetForObstacles;
}
