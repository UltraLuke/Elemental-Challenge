using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public float dyingTime;
	private float currTime;
	//public bool isBig;

	void Start () {
		currTime = dyingTime;
        /*if(!isBig)
			GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(1));
		else
			GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(15));*/

        GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(1));
    }
	
	// Update is called once per frame
	void Update () {
		currTime -= Time.deltaTime;

		if(currTime <= 0f)
		{
			Destroy(gameObject);
		}
	}
}
