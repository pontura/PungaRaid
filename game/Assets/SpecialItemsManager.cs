using UnityEngine;
using System.Collections;

public class SpecialItemsManager : MonoBehaviour {

    public types type;
    public enum types
    {
        NONE,
        CASCO
    }
    public int id;
	void Start () {
        Events.OnSetSpecialItem += OnSetSpecialItem;
	}
    void OnDestroy()
    {
        Events.OnSetSpecialItem -= OnSetSpecialItem;
    }
    void OnSetSpecialItem(int _id, bool active)
    {
        if (active)
        {
            type = types.CASCO;
            this.id = _id;
        } else
        {
            type = types.NONE;
            this.id = 0;
        }
    }
}
