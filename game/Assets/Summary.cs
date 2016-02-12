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
        int distance = (int)Game.Instance.gameManager.distance;
        if (distance > SocialManager.Instance.userHiscore.hiscore)
        {
            SendHiscore(distance);
            hiscoreField.text = "NUEVO HISCORE: " + distance + " METROS";
        }
        else
        {
            hiscoreField.text = "HICISTE " + distance + " METROS";
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
