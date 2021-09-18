using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    public GameObject splash;
    private float timeCorrection = 0.1f;
    private float currTime;

    private AudioSource src;

    // Use this for initialization
    void Start () {
        currTime = 0f;
        src = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (currTime > 0f)
            currTime -= Time.deltaTime;
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (currTime <= 0)
        {
            GameObject waterSplash = GameObject.Instantiate(splash);
            waterSplash.transform.position = new Vector2 (collision.transform.position.x, transform.position.y + 0.25f);
            src.PlayOneShot(Camera.main.GetComponent<SoundManager>().Sound(15));
            currTime = timeCorrection;
        }
    }
}