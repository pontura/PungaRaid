using UnityEngine;
using System.Collections;

public class LevelsManager : MonoBehaviour {

    public Lanes lanes;
    public Level level1;
    private Level activeLevel;
    private int nextLevelDistance;
    private int offset = 20;
   

	public void Init () {
        CheckForNewLevel(0);
        CheckForNewLevel(offset);
	}
    public void CheckForNewLevel(float distance)
    {
        distance += offset;
        if (distance < nextLevelDistance) return;
      //  print("add level in: " + distance);
        activeLevel = level1;
        LoadLevelAssets(nextLevelDistance);
        nextLevelDistance += activeLevel.length;
        
    }
    private void LoadLevelAssets(int nextLevelDistance)
    {
        Lanes laneData = activeLevel.GetComponent<Lanes>();
        lanes.AddBackground(laneData.background.name, nextLevelDistance);
    }
	public void LoadNext () {
	    
	}
}
