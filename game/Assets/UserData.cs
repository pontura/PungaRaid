using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserData : MonoBehaviour {

    public bool DEBUG_UnlockAllLevels;
    public List<int> starsZone1;

    private Data data;

	public void Init () {
        Events.OnLevelComplete += OnLevelComplete;
        data = GetComponent<Data>();
        LoadData();
        //diplomaId = PlayerPrefs.GetInt("diplomaId");
	}
    public void Reset()
    {

        for (int a=0; a<31; a++)
            PlayerPrefs.SetInt("level_1_" + a, 0);

        starsZone1.Clear();

        LoadData();

        PlayerPrefs.SetInt("hats", 0);
        PlayerPrefs.SetInt("chairs", 0);
        PlayerPrefs.SetInt("legs", 0);
    }
    public void UnblockAllLevels()
    {
        DEBUG_UnlockAllLevels = true;
    }
    void OnLevelComplete()
    {

    }
    public int ErorsToStars(int errors)
    {
        int stars;

        if (errors < 2) stars = 3;
        else if (errors < 4) stars = 2;
        else stars = 1;

        return stars;
    }
    void SaveStars(int levelID, int newStars)
    {
        int stars = PlayerPrefs.GetInt("level_1_" + levelID); 
        if(stars<newStars)
        {
            PlayerPrefs.SetInt("level_1_" + levelID, newStars);
            if (starsZone1.Count < levelID)
                starsZone1.Add(newStars);
            else
                starsZone1[levelID - 1] = newStars;
        }
    }
    void LoadData()
    {
       // int levelID = 0;
    }
    public int GetStarsIn(int LevelID)
    {
        if (DEBUG_UnlockAllLevels)
            return 3;

       // if (starsZone1.Count < LevelID) 
         //   return 0;
        int stars;

        if (starsZone1.Count < LevelID) 
            return 0;

        stars = starsZone1[LevelID - 1];

        return stars;
    }
}
