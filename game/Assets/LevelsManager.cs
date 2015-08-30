using UnityEngine;
using System.Collections;

public class LevelsManager : MonoBehaviour {

    public Lanes lanes;
    public Level StartingLevel;

    public Level[] randomLevels;

    public Level activeLevel;
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

        if(distance < 30)
             activeLevel = StartingLevel;
        else
        {
            int rand = Random.Range(0,randomLevels.Length);
            activeLevel = randomLevels[rand];
        }

        LoadLevelAssets(nextLevelDistance);
        nextLevelDistance += activeLevel.length;
        
    }
    private void LoadLevelAssets(int nextLevelDistance)
    {
        Lanes laneData = activeLevel.GetComponent<Lanes>();
      //  lanes.AddBackground(laneData.background.name, nextLevelDistance);

        foreach (Lane lane in laneData.all)
        {
            Transform[] allObjectsInLane = lane.transform.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in allObjectsInLane)
            {
                 EnemySettings settings = new EnemySettings();
                
                settings.speed = 0.03f;
                if(t.transform.localScale.x < 0)
                    settings.speed = -0.02f;

                lanes.AddObjectToLane(t.gameObject.name, lane.id, (int)(nextLevelDistance + t.transform.localPosition.x), settings);
            }
        }
    }
    
	public void LoadNext () {
	    
	}
}
