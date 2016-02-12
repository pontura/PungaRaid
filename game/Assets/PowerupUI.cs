using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerupUI : MonoBehaviour {

    public GameObject panel;
    public Text title;

    public GameObject bar;
    private float percent;
    public bool isOn;

    void Start()
    {
        panel.SetActive(false);
        Events.OnBarInit += OnBarInit;
    }
    void OnDestroy()
    {
        Events.OnBarInit -= OnBarInit;
    }
    void OnBarInit(PowerupManager.types type)
    {
        switch (type)
        {
            case PowerupManager.types.CHUMBO: title.text = "MEGA-CHUMBO"; break;
            case PowerupManager.types.GIL: title.text = "GIL-POWA"; break;
            case PowerupManager.types.MOTO: title.text = "RATI-CICLO"; break;
        }
        panel.SetActive(true);
        isOn = true;
        percent = 1;
    }
    void Update()
    {
        if (!isOn) return;
        if (Game.Instance.gameManager.state == GameManager.states.ENDING) return;

        OnSetBar();
        percent -= Time.deltaTime/10;
        if (percent < 0)
            SetOff();
    }
    void SetOff()
    {
        isOn = false;
        panel.SetActive(false);
        Events.OnHeroPowerUpOff();
    }
    void OnSetBar()
    {
        bar.GetComponent<Image>().fillAmount = percent;
    }
}
