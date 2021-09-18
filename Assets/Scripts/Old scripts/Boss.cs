using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	private int HP = 10;
	public Color damaged;
	private Color normal;

	public GameObject explosion;
	public GameObject flag;
	private AudioSource src;
	private Rigidbody2D rb;

	private float coolDown = 0.5f;
	private float lastCoolDown;

	public float speed;
	private int direction = -1;
	private bool facingRight = false;

	void Start () {
		normal = GetComponent<SpriteRenderer>().color;
		rb = GetComponent<Rigidbody2D>();
		src = GetComponent<AudioSource>();
		lastCoolDown = 0;
	}

	void Update ()
	{

		Move();

		if(lastCoolDown <= 0)
		{
			GetComponent<SpriteRenderer>().color = normal;
		}
		else
		{
			lastCoolDown -= Time.deltaTime;
		}
	}

	public void Move()
	{
		Vector2 moveSpeed;
        moveSpeed.x = speed * direction;
		moveSpeed.y = rb.velocity.y;

		rb.velocity = moveSpeed;

		if(moveSpeed.x > 0 && !facingRight)
			FlipCharacterX();
		else if(moveSpeed.x < 0 && facingRight)
			FlipCharacterX();
	}

	public void FlipCharacterX()
	{
		transform.Rotate(new Vector3(0, 180, 0));
		facingRight = !facingRight;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.layer == 9)
			direction *= -1;

		if(lastCoolDown <= 0 && (other.gameObject.layer == 19 || other.gameObject.layer == 23))
		{
			Hit();
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(lastCoolDown <= 0 && (other.gameObject.layer == 10 && other.transform.position.y > transform.position.y))
		{
			Hit();
		}
	}

	public void Hit()
	{
		HP--;
		if(HP > 0)
		{
			GetComponent<SpriteRenderer>().color = damaged;
			lastCoolDown = coolDown;
			src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(14));
		}
		else
		{
			GameObject bigExplosion = GameObject.Instantiate(explosion);
			bigExplosion.transform.position = transform.position;
			src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(15));

			flag.gameObject.SetActive(true);

			gameObject.SetActive(false);
		}
	}
}
