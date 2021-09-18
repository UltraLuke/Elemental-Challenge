using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator anim;
    public GameObject explosion;
    private AudioSource src;

    public int direction = -1;

    public float speed;
    private bool facingRight = true;

    public int points;

    //Layers
    //private int[] playerParts = new int[] { 10, 11, 13, 20 };

    void Start ()
    {   
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
        src = GetComponent<AudioSource>();
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

    public void SavageMove(bool pointingDirection)
    {
        Vector2 moveSpeed;
        if(!pointingDirection)
            moveSpeed.x = speed;
        else
            moveSpeed.x = speed * direction;

        moveSpeed.y = rb.velocity.y;

        rb.velocity = moveSpeed;
    }

    public void GravityShoot(List<Transform> triggers, GameObject bullet, List<Vector2> direction)
    {
        for(int i = 0; i < triggers.Count; i++)
        {
            GameObject enemyBullet = GameObject.Instantiate(bullet);
            enemyBullet.transform.position = triggers[i].position;
            //Vector3 derecha = new Vector3(gameObject.transform.right) *= direction[i];
            enemyBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(gameObject.transform.right.x * direction[i].x, direction[i].y), ForceMode2D.Impulse);
        }
    }

    public void LinearShoot(List<Transform> triggers, GameObject bullet, List<Vector2> direction)
    {
        for (int i = 0; i < triggers.Count; i++)
        {
            GameObject enemyBullet = GameObject.Instantiate(bullet);
            enemyBullet.transform.position = triggers[i].position;

            enemyBullet.transform.right = direction[i];
        }
    }

    public void AnimationExe(string animName)
    {
        anim.Play(animName);
    }

    public void PlaySound(int index)
    {
        src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(index));
    }

    public void DestroySelf(int layer)
	{
        if(layer == 8 || layer == 10 || layer == 13)
        {
            Game.score[0] += points;
        }
        else if (layer == 9 || layer == 11 || layer == 22)
        {
            Game.score[1] += points;
        }

		GameObject explode = GameObject.Instantiate(explosion);
		explode.transform.position = transform.position;
		Destroy(gameObject);
	}

    public void FlipCharacterX()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        facingRight = !facingRight;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        int otherLayer = other.gameObject.layer;

        switch (otherLayer)
        {
            case 8:
            case 9:
            case 21:
                {
                    direction *= -1;
                    break;
                }

            case 10:
            case 11:
            case 13:
            case 22:
                {
                    DestroySelf(otherLayer);
                    break;
                }
        }

        /*for (int i = 0; i < playerHit.Length; i++)
        {
            if (playerHit[i] == otherLayer)
            {
                DestroySelf();
            }
        }*/

        /*
        if (other.gameObject.layer == 9)
            direction *= -1;

        if (other.gameObject.layer == 19 || other.gameObject.layer == 23)
        {
            Stomp();
        }*/
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        /*
        if (other.gameObject.layer == 10 && other.transform.position.y > transform.position.y)
        {
            Stomp();
        }*/
    }
}
