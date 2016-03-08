using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {

    public GameObject simpleButton;
    public Text simpleButtonTitle;
    public GameObject button;
    private SwipeDetector.directions direction;
    private Character character;

    public void Init()
    {
        character = Game.Instance.characterManager.character;
        simpleButton.SetActive(false);
        Events.OnSwipe += OnSwipe;
    }
    void OnDestroy()
    {
        Events.OnSwipe -= OnSwipe;
    }
	public void InitSimpleButton (string title, SwipeDetector.directions direction) {

        if (direction == SwipeDetector.directions.NONE)
            button.gameObject.SetActive(true);

        else button.gameObject.SetActive(false);
        this.direction = direction;
        simpleButton.SetActive(true);
        simpleButtonTitle.text = title;
        Time.timeScale = 0;
	}
    void OnSwipe(SwipeDetector.directions dir)
    {
        Lanes lanes = Game.Instance.GetComponent<LevelsManager>().lanes;
        if (dir.ToString() == direction.ToString())
        {
            if (dir == SwipeDetector.directions.UP)
            {
                lanes.TryToChangeLane(true);
                character.MoveUP();
            }
            else if (dir == SwipeDetector.directions.DOWN)
            {
                lanes.TryToChangeLane(false);
                character.MoveDown();
            }
            else if (dir == SwipeDetector.directions.RIGHT)
                character.Dash();

            SimpleButtonOk();
        }

        
    }
    public void SimpleButtonOk()
    {
        Time.timeScale = 1;
        simpleButton.SetActive(false);
        gameObject.SetActive(false);
	}
}
