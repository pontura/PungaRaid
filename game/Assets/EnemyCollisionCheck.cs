using UnityEngine;
using System.Collections;

public class EnemyCollisionCheck : MonoBehaviour {

    public Enemy enemy;

	void Start () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        enemy.OnSecondaryCollision(other);
    }
}
