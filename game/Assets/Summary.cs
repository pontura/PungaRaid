using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Summary : MonoBehaviour {

    public GameObject panel;
    public Text hiscoreStaticField;
    public Text hiscoreField;
    public Text totalScore;
    public RankingUI ranking;
    public GameObject challengeButton;

    public Image bar;
    public int totalToWin = 100000;
    private float score;
    private float total_from;
    private int total_to;
    private bool ready;

    private states state;
    private enum states
    {
        OFF,
        COUNT_DOWN,
        READY
    }

    void Start()
    {
        panel.SetActive(false);
    }
    public void Init()
    {
        if (SocialManager.Instance.userData.logged)
        {
            ranking.gameObject.SetActive(true);
            challengeButton.SetActive(false);
        }
        else
        {
            ranking.gameObject.SetActive(false);
            challengeButton.SetActive(true);
        }
        panel.SetActive(true);
       // panel.GetComponent<Animator>().Play("PopupOn");
        score = Game.Instance.gameManager.score;
        if (score > SocialManager.Instance.userHiscore.hiscore)
        {
            SendHiscore((int)score);
            hiscoreStaticField.text = "NUEVO RECORD! $" + score;
        }
        else
        {
            hiscoreStaticField.text = "Hicites $" + score;
        }
        total_from = SocialManager.Instance.userHiscore.totalScore;
        total_to = (int)total_from + (int)score;
        SocialEvents.OnAddToTotalScore((int)score);

        state = states.COUNT_DOWN;

    }
    void Update()
    {
        if (state == states.COUNT_DOWN)
        {
            float resta = (score / 40) * (Time.deltaTime*100);
            total_from += resta;
            score -= resta;
            if (total_from >= total_to)
            {
                state = states.READY;
                total_from = total_to;
                score = 0;
            }
            if (total_from > totalToWin)
            {
                total_from = totalToWin;
                state = states.READY;
                score = 0;
            }
            totalScore.text = "$" + (int)total_from;
            hiscoreField.text = "$" + (int)score;

            bar.fillAmount = total_from / totalToWin;
        }
    }
    public void SendHiscore(int distance)
    {
        SocialEvents.OnNewHiscore(distance);
    }
    public void Restart()
    {
        Game.Instance.gameManager.Restart("04_Game");
    }
    public void Map()
    {
        Game.Instance.gameManager.Restart("02_Map");
    }
    public void LoginAdvisor()
    {
        Events.OnLoginAdvisor();
    }
    public void Challenge()
    {
        Data.Instance.LoadLevel("08_ChallengesCreator");
    }
}
