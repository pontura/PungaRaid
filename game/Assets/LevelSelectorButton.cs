using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectorButton : MonoBehaviour {

    public bool isActive;
    public Stars stars;
    public Text label;
    public int id;
    public int zoneID;
    private Button button;
    [SerializeField]
    Image imageLock;

	public void Init(int zoneID, int id, int starsQTY)
    {
        this.zoneID = zoneID;
        this.id = id;

        label.text = id.ToString();
        stars.Init(starsQTY);
        button = GetComponent<Button>();
        isActive = true;

        //// HACK: es el primer nivel
        //if (starsQTY == 4)
        //{
        //    imageLock.enabled = false;
        //    stars.Init(0);
        //} else
        if (starsQTY == 0)
        {
            isActive = false;
            imageLock.enabled = true;
        } else
            imageLock.enabled = false;
	}
    public void NextButton()
    {
         isActive = true;
         button.targetGraphic.color = button.colors.normalColor;
         imageLock.enabled = false;
    }
}
