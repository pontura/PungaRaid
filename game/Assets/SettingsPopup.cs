using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsPopup : MonoBehaviour {

    public Text LoginField;
    public Text musicLabel;
    public Text sfxLabel;

    public GameObject panel;
    private GraphicRaycaster graphicRaycaster;

	void Start () {
     //   panel.transform.localScale = Data.Instance.screenManager.scale;
        panel.SetActive(false);
        Events.OnSettings += OnSettings;
        SocialEvents.OnFacebookNotConnected += OnFacebookNotConnected;

        SetMusicLabel();
        SetSFXLabel();
        
	}
    void OnDestroy()
    {
        Events.OnSettings -= OnSettings;
        SocialEvents.OnFacebookNotConnected -= OnFacebookNotConnected;
    }
    void OnSettings()
    {
       // SetLoginField(FB.IsLoggedIn);

        graphicRaycaster = GetComponentInChildren<GraphicRaycaster>();
        graphicRaycaster.enabled = true;

        panel.SetActive(true);
        panel.GetComponent<Animation>().Play("PopupOn");
    }
    public void FBLoginInOut()
    {
        //if (!FB.IsLoggedIn)
        //{
            OnFacebookNotConnected();
            SetLoginField(false);
        //}
        //else
        //{
        //    Data.Instance.loginManager.ParseFBLogout();
        //    SetLoginField(true);
        //}
    }
    void SetLoginField(bool conected)
    {
        if (conected)
            LoginField.text = "Desconectarte";
        else 
            LoginField.text = "Registrate!";
    }
    public void OnFacebookNotConnected()
    {
        print("FB: OnFacebookNotConnected");
        CloseOff();
        Events.OnLoginAdvisor();
    }
    public void Close()
    {
        panel.GetComponent<Animation>().Play("PopupOff");
        Invoke("CloseOff", 0.2f);
    }
    void CloseOff()
    {
        graphicRaycaster = GetComponentInChildren<GraphicRaycaster>();
       // graphicRaycaster.enabled = false;
        panel.SetActive(false);
    }
    public void Challenges()
    {
        if (SocialManager.Instance.userData.logged)
            Data.Instance.LoadLevel("05_Challenges");
        else
            Events.OnLoginAdvisor();
        Close();
    }
    public void Ranking()
    {
        if (SocialManager.Instance.userData.logged)
            Data.Instance.LoadLevel("07_Ranking");
        else
            Events.OnLoginAdvisor();
        Close();
    }
    public void SwitchMusic()
    {
        float vol = 1;
        if (Data.Instance.musicManager.volume == 0)
            vol = 1;
        else
            vol = 0;

        Events.OnMusicVolumeChanged(vol);

        SetMusicLabel();
    }
    public void SwitchSFX()
    {
        float vol = 1;
        if (Data.Instance.soundManager.volume == 0)
            vol = 1;
        else
            vol = 0;

        Events.OnSoundsVolumeChanged(vol);

        SetSFXLabel();
    }
    void SetMusicLabel()
    {
        if(Data.Instance.musicManager.volume ==0)
            musicLabel.text = "Música ON";
        else 
            musicLabel.text = "Música OFF";

    }
    void SetSFXLabel()
    {
        if (Data.Instance.soundManager.volume == 0)
            sfxLabel.text = "SFX ON";
        else
            sfxLabel.text = "SFX OFF";

    }

}
