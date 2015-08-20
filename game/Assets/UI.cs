using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    [SerializeField]
    PausedMenu pausedMenu;

    [SerializeField]
    ScoreProgress scoreProgress;

    [SerializeField]
    GameObject menuButton;


   public void Init()
    {
        Events.OnLevelComplete += OnLevelComplete;
    }
    void OnDestroy()
    {
        Events.OnLevelComplete -= OnLevelComplete;
    }
    public void OnPauseButton()
    {
        GetComponent<PausedMenu>().Init();
        Events.OnGamePaused(true);
    }
    void OnLevelComplete()
    {
        scoreProgress.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
    }
}
