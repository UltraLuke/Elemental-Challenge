using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

	public Transform player1;
	public Transform player2;
	private bool lavaRelease;
	public float speed;

	public static float freezeTime;

	void Start ()
	{
		lavaRelease = false;
		freezeTime = 0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(freezeTime <= 0f)
			Move();
		else
			freezeTime -= Time.deltaTime;
	}

	public void Move()
	{
		/*if(!Game.twoPlayersGame)
		{
			if(!lavaRelease && player1.position.y > 5f)
			{
				lavaRelease = true;
			}
			else if (lavaRelease && transform.position.y < 132f)
			{
				transform.position += transform.up * speed * Time.deltaTime;
			}
		}
		else if(Game.twoPlayersGame)
		{
			if(!lavaRelease && player1.position.y > 5f && player2.position.y > 5f)
			{
				lavaRelease = true;
			}
			else if (lavaRelease && transform.position.y < 132f)
			{
				transform.position += transform.up * speed * Time.deltaTime;
			}
		}*/
	}


}
