using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginAdvisor : MonoBehaviour {

    public GameObject panel;
    private GraphicRaycaster graphicRaycaster;

	void Start () {
     //   panel.transform.localScale = Data.Instance.screenManager.scale;
        panel.SetActive(false);
        Events.OnLoginAdvisor += OnLoginAdvisor;
        SocialEvents.OnFacebookLoginCanceled += OnFacebookLoginCanceled;
	}
    void OnLoginAdvisor()
    {
        graphicRaycaster = GetComponentInChildren<GraphicRaycaster>();
        graphicRaycaster.enabled = true;

        panel.SetActive(true);
        panel.GetComponent<Animation>().Play("PopupOn");
    }
    public void LoginToFacebook()
    {        
        SocialManager.Instance.loginManager.FBLogin();
        Data.Instance.LoadLevel("03_Connecting");
        Close();
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
    void OnFacebookLoginCanceled()
    {
        Close();
        Data.Instance.LoadLevel("02_MainMenu");
    }
}
