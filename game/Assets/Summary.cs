using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Summary : MonoBehaviour {

    public GameObject panel;
    public Text hiscoreField;

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
}
