using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsPopup : MonoBehaviour {

    public Text LoginField;
    public GameObject panel;
    private GraphicRaycaster graphicRaycaster;

	void Start () {
     //   panel.transform.localScale = Data.Instance.screenManager.scale;
        panel.SetActive(false);
        Events.OnSettings += OnSettings;
        SocialEvents.OnFacebookNotConnected += OnFacebookNotConnected;
        
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
        graphicRaycaster.enabled = false;
        panel.SetActive(false);
    }
}
