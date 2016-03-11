using UnityEngine;
using System.Collections;

public class SpecialItems : MonoBehaviour {

    private Hero hero;

	void Start () {
        hero = GetComponent<Character>().hero;
        if (Data.Instance.specialItems.type == SpecialItemsManager.types.CASCO)
            OnSetSpecialItem(1, true);
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
        } else
        {
            hero.casco.gameObject.SetActive(false);
        }
    }
}
