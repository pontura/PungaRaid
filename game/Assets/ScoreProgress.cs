using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreProgress : MonoBehaviour {

    [SerializeField]
    Text label;
    private int totalLevelScore;
    public int score;

    void Start()
    {
        Events.OnScoreAdd += OnScoreAdd;        
    }
    void OnDestroy()
    {
        Events.OnScoreAdd -= OnScoreAdd;
    }
    public void OnScoreAdd(int _score)
    {
        score += _score;
        label.text = "$" + score.ToString();
    }
}
