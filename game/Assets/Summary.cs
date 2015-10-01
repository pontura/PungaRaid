using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Summary : MonoBehaviour {

    public GameObject canvas;
    [SerializeField] Stars stars;
    private string NextAction;

    public GameObject RewardsCanvas;

    public GameObject[] rewardHats;
    public GameObject[] rewardChairs;
    public GameObject[] rewardHShoes;

    public AudioClip[] clipHats;
    public AudioClip[] clipChairs;
    public AudioClip[] clipShoes;

    void Start()
    {
        Events.OnLevelComplete += OnLevelComplete;
        canvas.SetActive(false);
        RewardsCanvas.SetActive(false);
    }
    void OnDestroy()
    {
        Events.OnLevelComplete -= OnLevelComplete;
    }
    void OnLevelComplete()
    {
        canvas.SetActive(true);
        int _stars;
        int errors = Data.Instance.errors;

        if (errors==0)
            _stars = 3;
        else if (errors==1)
            _stars = 2;
        else
            _stars = 1;
        
        stars.Init(_stars);

        Invoke("sayCongrats", 1);
    }
    void sayCongrats()
    {
        int rand = Random.Range(0, 100);

        if (rand < 33)
            Events.OnSoundFX("2_Well Done");
        else if (rand < 66)
            Events.OnSoundFX("3_Great Job");
        else
            Events.OnSoundFX("4_Awesome Job");
    }
    void particlesOn()
    {
      //  Events.OnHeroWinClothes(reward.rewardType, reward.num);
        Invoke("sayIt", 1);
    }
    void sayIt()
    {
       // Events.OnSoundFX(clip.name);
    }
    public void ResetLevel()
    {
        Data.Instance.LoadLevel("04_Game");
        Data.Instance.errors = 0;
    }
    public void Next()
    {
        Events.OnSoundFX("buttonPress");
        ResetLevel();
    }
    public void RePlay()
    {
        Events.OnSoundFX("buttonPress");
        ResetLevel();
    }
    public void MainMenu()
    {
        Events.OnSoundFX("buttonPress");
        ResetLevel();
    }
}
