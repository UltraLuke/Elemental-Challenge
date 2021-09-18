using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrigger : MonoBehaviour {

    public GameObject fireBall;
    public float impulse;
    private float timer;
    private float currTime;

    void Start()
    {
        timer = 3f;
        currTime = 0f;
    }

    void Update ()
    {
		if(currTime <= 0)
        {
            currTime = timer;

            GameObject fireBallInstance = Instantiate(fireBall);
            fireBallInstance.transform.position = gameObject.transform.position;
            fireBallInstance.GetComponent<Rigidbody2D>().AddForce(Vector2.up * impulse, ForceMode2D.Impulse);
            fireBallInstance.GetComponent<FireBall>().triggerPosY = transform.position.y;
        }
        else
        {
            currTime -= Time.deltaTime;
        }
	}
}
