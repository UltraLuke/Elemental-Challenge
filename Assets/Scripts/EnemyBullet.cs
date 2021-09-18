using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	public float speed;
    public float timer;
    public bool isLinear;

	void Update ()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            Destroy(this.gameObject);

		if(isLinear)
        {
            LinearMovement();
        }
	}

    public void LinearMovement()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer != 4)
        {
            Destroy(gameObject);
        }
    }
}
