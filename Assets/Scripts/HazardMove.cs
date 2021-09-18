using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardMove : MonoBehaviour {

	public float speed;
	private int direction = 1;
	public bool isVertical;
    public Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		Move();
	}

	public void Move()
	{
		Vector2 moveSpeed;

		if(!isVertical)
		{
        	moveSpeed.x = direction * speed;
			moveSpeed.y = rb.velocity.y;
		}
		else
		{
			moveSpeed.y = direction * speed;
			moveSpeed.x = rb.velocity.x;
		}

		rb.velocity = moveSpeed;
	}

	public void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.layer == 27)
			direction *= -1;
	}
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 27)
            direction *= -1;
    }
}
