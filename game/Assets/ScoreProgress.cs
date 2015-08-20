using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreProgress : MonoBehaviour {

    [SerializeField]
    Image bar;
    [SerializeField]
    Text label;
    private int totalLevelScore;

    void Start()
    {
        Events.OnScoreRefresh += OnScoreRefresh;        
    }
    void OnDestroy()
    {
        Events.OnScoreRefresh -= OnScoreRefresh;
    }
    public void Init(int _totalLevelScore)
    {
        this.totalLevelScore = _totalLevelScore;
        OnScoreRefresh(0);
    }
    public void OnScoreRefresh(int score)
    {
        if (score > 0) GetComponent<Animator>().SetBool("on", true);
        label.text = score.ToString();
        float ammount = (float)score / (float)totalLevelScore; 
        bar.fillAmount = ammount;
       // print(ammount + "     score: " + score + "     totalLevelScore " + totalLevelScore );
    }
    public void AnimReady()
    {
        GetComponent<Animator>().SetBool("on", false);
    }
}
