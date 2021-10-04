using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour {

    private Rigidbody2D rbParent;
    public GameObject dust;

    private bool onGround;
    private bool onPlatform;

	void Start ()
    {
        rbParent = transform.parent.GetComponent<Rigidbody2D>();
        onGround = false;
        onPlatform = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12 || collision.gameObject.layer == 25)
        {
            rbParent.velocity = Vector2.zero;
            rbParent.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<IEnemyDestroyable>().DestroySelf(transform.parent.gameObject.layer);
            transform.parent.GetComponent<IPlayerBodyCallback>().FlyJump(false);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!onPlatform && rbParent.velocity.y == 0 && collision.gameObject.layer != 15)
        {
            if (!onGround && collision.gameObject.layer == 23)
            {
                GameObject dustInstance = GameObject.Instantiate(dust);
                dustInstance.transform.position = transform.position;
                onGround = true;
            }
            GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(18));
            onPlatform = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (onPlatform)
        {
            if (onGround && collision.gameObject.layer == 23)
            {
                onGround = false;
            }
            onPlatform = false;
        }
    }
}
