using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EN UN FUTURO SE USARA PARA AGREGAR ATAQUES A LOS ENEMIGOS

public class FlyEnemy : MonoBehaviour
{
	private EnemyBody enemyBody;

	void Start ()
	{
		enemyBody = GetComponent<EnemyBody>();
	}
	
	void Update () {
		enemyBody.Move();
	}
}