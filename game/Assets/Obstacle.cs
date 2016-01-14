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
    private BoxCollider2D collider2d;
    public GameObject container;

    override public void Enemy_Activate()
    {
        collider2d = GetComponent<BoxCollider2D>();
        collider2d.enabled = true;
    }
    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
        int newRandObstacleID = Random.Range(0, obstacles.Length);        
        if(randObstacleID == newRandObstacleID) return;

        randObstacleID = newRandObstacleID;
        ResetContainer();

        GameObject go = Instantiate(obstacles[randObstacleID]) as GameObject;
        go.transform.SetParent(container.transform);
        anim = go.GetComponent<Animator>();
       // distance = Game.Instance.gameManager.distance;
        float scaleNum = 1;
        Vector3 scale = new Vector3(scaleNum, scaleNum, scaleNum);
        transform.localScale = scale;
        transform.localPosition = Vector3.zero;
        go.transform.localPosition = Vector3.zero;
        anim.Play("idle",0,0);
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
        //anim.Play("crashed", 0, 0);
       // if (collider2d) collider2d.enabled = false;
        print("Pool!!!!!!" + name);
        Pool();
        //Destroy(gameObject);
    }

}
