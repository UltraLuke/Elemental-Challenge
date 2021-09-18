using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingPrefab : MonoBehaviour {

    public float destroyTime;
    private float currTime;
	
	void Start ()
    {
        currTime = destroyTime;
    }
	
	void Update ()
    {
        if (currTime > 0)
            currTime -= Time.deltaTime;
        else
            Destroy(gameObject);
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 23 || collision.gameObject.layer == 24)
        {
            Destroy(gameObject);
        }
    }
}
