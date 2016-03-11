using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RuletaPopup : MonoBehaviour {

    public Text desc;
    public Image icon;
    public Animator anim;

    void Start()
    {
        anim.gameObject.SetActive(false);
    }
	
	public void Open () {
        Invoke("OpenDelay", 0.5f);
        desc.text = "+1 VIDA: Te ganaste un casco para la próxima partida";
        Events.OnSetSpecialItem(1, true);
	}
    public void OpenDelay()
    {
        anim.gameObject.SetActive(true);
        anim.Play("PopupOn", 0, 0);
    }
    public void Map()
    {
        Data.Instance.LoadLevel("02_Map");
    }
}
