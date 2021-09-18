using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAreaTurret : MonoBehaviour {

    public GameObject enemyShooter;
    private TurretEnemy shootEnemyScript;
    private Transform shootEnemyTrans;
    public Vector2 correction;

    // Use this for initialization
    void Start () {
        shootEnemyScript = enemyShooter.GetComponent<TurretEnemy>();
        shootEnemyTrans = enemyShooter.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector2(shootEnemyTrans.position.x + correction.x, shootEnemyTrans.position.y + correction.y);
	}

    public void OnTriggerStay2D(Collider2D collision)
    {
        int otherLayer = collision.gameObject.layer;

        if (otherLayer == 8 || otherLayer == 9 || otherLayer == 19)
        {
            shootEnemyScript.Shoot();
        }
    }
}
