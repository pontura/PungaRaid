using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Summary : MonoBehaviour {

    public GameObject panel;
    public Text hiscoreField;
    public RankingUI ranking;
    public GameObject challengeButton;

    void Start()
    {
        panel.SetActive(false);
        Events.OnHeroDie += OnHeroDie;
    }
    void OnDestroy()
    {
        Events.OnHeroDie -= OnHeroDie;
    }
    void OnHeroDie()
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
        panel.GetComponent<Animation>().Play("PopupOn");
        int score = (int)Game.Instance.gameManager.score;
        if (score > SocialManager.Instance.userHiscore.hiscore)
        {
            SendHiscore(score);
            hiscoreField.text = "NUEVO RECORD! Hiciste $" + score + " guita!";
        }
        else
        {
            hiscoreField.text = "Hiciste $" + score + " guita";
        }
    }
    public void SendHiscore(int distance)
    {
        SocialEvents.OnNewHiscore(distance);
    }
    public void Restart()
    {
        Game.Instance.gameManager.Restart();
    }
    public void Challenge()
    {
        Events.OnLoginAdvisor();
    }
}
