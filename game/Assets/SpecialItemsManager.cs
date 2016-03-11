using UnityEngine;
using System.Collections;

public class SpecialItemsManager : MonoBehaviour {

    public types type;
    public enum types
    {
        NONE,
        CASCO
    }
	void Start () {
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
            type = types.CASCO;
        } else
        {
            type = types.NONE;
        }
    }
}
