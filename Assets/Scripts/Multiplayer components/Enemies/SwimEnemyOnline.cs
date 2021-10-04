using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EN UN FUTURO SE USARA PARA AGREGAR ATAQUES A LOS ENEMIGOS

public class SwimEnemyOnline : MonoBehaviour
{
	private EnemyBodyOnline enemyBody;

	void Start ()
	{
		enemyBody = GetComponent<EnemyBodyOnline>();
	}
	
	void Update () {
		enemyBody.Move();
	}
}