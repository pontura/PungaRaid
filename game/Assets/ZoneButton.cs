using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZoneButton : MonoBehaviour {

    public int id;
    public bool unlocked;
    public GameObject iconLock;
    public GameObject iconUnlock;

    public void Init(bool unlocked)
    {
        this.unlocked = unlocked;
        if (unlocked)
        {
            iconLock.SetActive(false);
            iconUnlock.SetActive(true);            
        }
        else
        {
            GetComponent<Button>().interactable = false;
            iconLock.SetActive(true);
            iconUnlock.SetActive(false);
        }
    }
}
