using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelector : MonoBehaviour {

    [SerializeField]
    Image title1;
    [SerializeField]
    Image title2;
    [SerializeField]
    Image title1_off;
    [SerializeField]
    Image title2_off;

    [SerializeField]
    Image bg1;
    [SerializeField]
    Image bg2;

    [SerializeField]
    GameObject items1;
    [SerializeField]
    GameObject items2;

    public GameObject[] hats;
    public GameObject[] legs;
    public GameObject[] chairs;

    public GameObject buttonsContainer;

    private UserData userData;
    public LevelLockedPopup levelLockedPopup;
    public int zoneID = 1;
    
    private int id = 1;
    private bool nextActiveButton = false;
    private int _xZone2 = -2351;

	void Start () {
        Events.OnMusicChange("gameMenu");
        activateZone(1);
        levelLockedPopup.gameObject.SetActive(false);
        userData = Data.Instance.GetComponent<UserData>();

        LevelSelectorButton[] buttons = buttonsContainer.GetComponentsInChildren<LevelSelectorButton>();
        
        foreach (LevelSelectorButton button in buttons)
        {
            
            int starsQty = userData.GetStarsIn( id);

            button.Init(zoneID, id, starsQty);

            //el proximo activo:
            if (starsQty == 0 && !nextActiveButton)
            {
                nextActiveButton = true;
                button.NextButton();
            }

            id++;
        }
        buttons[0].GetComponent<LevelSelectorButton>().isActive = true;

        

    }
    public void Clicked(LevelSelectorButton button)
    {
        if (!button.isActive)
        {
            levelLockedPopup.gameObject.SetActive(true);
            return;
        }
        Events.OnSoundFX("buttonPress");
       // Data.Instance.GetComponent<WordsData>().LevelID = button.id;
        Data.Instance.LoadLevel("04_Game", 1, 1, Color.black);   
    }
    public void MainMenu()
    {
        Events.OnSoundFX("backPress");
        Data.Instance.LoadLevel("02_MainMenu", 1, 1, Color.black);     
    }
    public void nextClicked()
    {
        activateZone(2);
        Events.OnSoundFX("buttonPress");
    }
    public void prevClicked()
    {
        activateZone(1);
        Events.OnSoundFX("buttonPress");
    }
    private void activateZone(int zoneID)
    {
        
        this.zoneID = zoneID;
        Vector3 pos = buttonsContainer.transform.localPosition;

        if (zoneID == 2)
        {
            pos.x = _xZone2;
            items1.SetActive(false);
            items2.SetActive(true);

            title1.enabled = false;
            title2.enabled = true;
            title1_off.enabled = true;
            title2_off.enabled = false;

            bg2.enabled = true;
            bg1.enabled = false;
        }
        else
        {
            pos.x = 0;

            items2.SetActive(false);
            items1.SetActive(true);

            title1.enabled = true;
            title2.enabled = false;
            title2_off.enabled = true;
            title1_off.enabled = false;

            bg1.enabled = true;
            bg2.enabled = false;
        }
        buttonsContainer.transform.localPosition = pos;


        foreach (Image image in items1.GetComponentsInChildren<Image>())
            image.color = new Color(1, 1, 1, 0.35f);

        foreach (Image image in items2.GetComponentsInChildren<Image>())
            image.color = new Color(1, 1, 1, 0.35f);

        if (PlayerPrefs.GetInt("hats") > 0) hats[PlayerPrefs.GetInt("hats") - 1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        if (PlayerPrefs.GetInt("chairs") > 0) chairs[PlayerPrefs.GetInt("chairs") - 1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        if (PlayerPrefs.GetInt("legs") > 0) legs[PlayerPrefs.GetInt("legs") - 1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        
    }
    
}
