using UnityEngine;
using System.Collections;

public class Obstacle : Enemy
{
    public states state;

    public enum states
    {
        IDLE,
        CRASHED
    }
    private int randObstacleID = -1;
    public GameObject[] obstacles;

    private string currentAnim;
    private BoxCollider2D collider;
    public GameObject container;

    override public void Enemy_Activate()
    {
        collider = GetComponent<BoxCollider2D>();
        collider.enabled = true;
    }
    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
        int newRandObstacleID = Random.Range(0, obstacles.Length);        
        if(randObstacleID == newRandObstacleID) return;

        randObstacleID = newRandObstacleID;
        ResetContainer();

        GameObject go = Instantiate(obstacles[randObstacleID]) as GameObject;
        anim = go.GetComponent<Animator>();
       
        go.transform.SetParent(container.transform);
       // distance = Game.Instance.gameManager.distance;
        float scaleNum = 1;
        Vector3 scale = new Vector3(scaleNum, scaleNum, scaleNum);
        transform.localScale = scale;
        transform.localPosition = Vector3.zero;
        go.transform.localPosition = Vector3.zero;
        anim.Play("idle");
    }
    void ResetContainer()
    {
        foreach (Transform child in container.transform)
            Destroy(child.gameObject);
    }
    override public void Enemy_Pooled()
    {
        state = states.IDLE;
    }
    override public void OnCrashed()
    {
        if (state == states.CRASHED) return;
        anim.Play("hit");
    }
    override public void OnExplote()
    {
        if (state == states.CRASHED) return;
        state = states.CRASHED;
        anim.Play("crashed");
        collider.enabled = false;
    }

}
