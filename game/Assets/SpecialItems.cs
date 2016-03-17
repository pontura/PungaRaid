using UnityEngine;
using System.Collections;

public class SpecialItems : MonoBehaviour {

    private Hero hero;

	void Start () {
        hero = GetComponent<Character>().hero;
        if (Data.Instance.specialItems.type == SpecialItemsManager.types.CASCO)
            OnSetSpecialItem(Data.Instance.specialItems.id, true);
        else
            hero.casco.gameObject.SetActive(false);
        Events.OnSetSpecialItem += OnSetSpecialItem;
	}
    void OnDestroy()
    {
        Events.OnSetSpecialItem -= OnSetSpecialItem;
    }
    void OnSetSpecialItem(int id, bool active)
    {
        if (active)
        {
            hero.casco.gameObject.SetActive(true);
            hero.casco.sprite = Resources.Load("helmets/" + id, typeof(Sprite)) as Sprite;
        } else
        {
            hero.casco.gameObject.SetActive(false);
        }
    }
}
