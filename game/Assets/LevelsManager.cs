using UnityEngine;
using System.Collections;
using System;

public class LevelsManager : MonoBehaviour {

    [Serializable]
    public class Group
    {
        public int distance;
        public Level[] levels;
    }
    public int activeGroupId = 0;
    private float startingGroupDistance;
    public Lanes lanes;
    public Level StartingLevel;

    public Group[] groups;

    public Level activeLevel;
    private int nextLevelDistance;
    private int offset = 60;
   

	public void Init () {
        CheckForNewLevel(0);
	}
    public void CheckForNewLevel(float distance)
    {
        distance += offset;        

        if (distance < nextLevelDistance) return;

        if (distance <= 70)
             activeLevel = StartingLevel;
        else
        {
            int rand = UnityEngine.Random.Range(0, groups[activeGroupId].levels.Length);
            activeLevel = groups[activeGroupId].levels[rand];
        }

        if (distance - startingGroupDistance > groups[activeGroupId].distance)
        {
            startingGroupDistance += distance;
            activeGroupId++;
        }

        print("CheckForNewLevel" + distance + " activeGroupId: " + activeGroupId);
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
