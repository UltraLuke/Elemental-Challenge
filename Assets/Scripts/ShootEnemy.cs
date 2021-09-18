using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EN UN FUTURO SE USARA PARA AGREGAR ATAQUES A LOS ENEMIGOS

public class ShootEnemy : MonoBehaviour
{
    private EnemyBody enemyBody;
    public GameObject searchArea;
    public List<Transform> triggers = new List<Transform>();
    public GameObject bullet;
    
    public List<Vector2> directions = new List<Vector2>();

    public bool detected;

    void Start ()
	{
		enemyBody = GetComponent<EnemyBody>();
        detected = false;
	}

    void Update()
    {
        enemyBody.Move();
    }

    public void Shoot()
    {
        enemyBody.GravityShoot(triggers, bullet, directions);
    }

    public void OnDestroy()
    {
        Destroy(searchArea.gameObject);
    }
}