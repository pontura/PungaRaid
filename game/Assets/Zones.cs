using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Zones : MonoBehaviour {

    public Text score;
    public Text username;
    public GameObject container;

	void Start () {
        Events.OnMusicChange("Raticity");
        score.text = "$" + SocialManager.Instance.userHiscore.totalScore;

        if (SocialManager.Instance.userData.logged)
            username.text = SocialManager.Instance.userData.username;

        int id = 1;
        foreach (ZoneButton button in container.GetComponentsInChildren<ZoneButton>())
        {
            bool unlocked = false;
            if(Data.Instance.moodsManager.IsMoodUnlocked( button.id))
                unlocked = true;
            
            button.Init(unlocked);
            id++;
        }
    }
    public void Clicked(int id)
    {
        print("clicked" + id);
        Data.Instance.moodsManager.SetCurrentMood(id);
        GetComponent<MoodPopup>().Open();
    }
}
