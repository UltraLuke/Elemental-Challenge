using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public float triggerPosY;
    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        AnimSet();
        FireKiller();
    }

    public void FireKiller()
    {
        if (transform.position.y < triggerPosY)
        {
            Destroy(gameObject);
        }
    }

    public void AnimSet()
    {
        anim.SetFloat("Velocity Y", rb.velocity.y);
    }
}
