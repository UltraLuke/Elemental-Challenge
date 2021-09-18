using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour {

	private Rigidbody2D rb;
	private Transform tr;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		tr = GetComponent<Transform>();

        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * 26, ForceMode2D.Impulse);
    }

	// Update is called once per frame
	void Update ()
	{
		if(tr.position.y <= -20f)
		{
			Destroy(gameObject);
		}
	}
}
