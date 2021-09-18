using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : MonoBehaviour {

    private Rigidbody2D rbParent;

    void Start()
    {
        rbParent = transform.parent.GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12 || collision.gameObject.layer == 25)
        {
            rbParent.velocity = Vector2.zero;
            rbParent.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<EnemyBody>().DestroySelf(10);
        }
    }
}
