using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerupUI : MonoBehaviour {

    public GameObject panel;

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
    void OnBarInit()
    {
        panel.SetActive(true);
        isOn = true;
        percent = 1;
    }
    void Update()
    {
        if (!isOn) return;
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
