using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : MonoBehaviour {

    public GameObject enemyShooter;
    private ShootEnemy shootEnemyScript;
    private Transform shootEnemyTrans;

    // Use this for initialization
    void Start () {
        shootEnemyScript = enemyShooter.GetComponent<ShootEnemy>();
        shootEnemyTrans = enemyShooter.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector2(shootEnemyTrans.position.x, shootEnemyTrans.position.y + 0.5f);
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        int otherLayer = collision.gameObject.layer;

        if (otherLayer == 8 || otherLayer == 9 || otherLayer == 19)
        {
            shootEnemyScript.Shoot();
        }
    }
}
