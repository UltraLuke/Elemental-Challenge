using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed;
    private float timer;
    public GameObject smoke;
    public Vector2 smokeCorrection;

    void Start ()
    {
        timer = 2f;
    }

	void Update ()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            Destroy(this.gameObject);

		transform.position += transform.right * speed * Time.deltaTime;
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 14)
        {
            Destroy(other.gameObject);
        }

        if((gameObject.layer == 13 && other.gameObject.layer == 9) || (gameObject.layer == 22 && other.gameObject.layer == 8))
        {
            other.GetComponent<IPlayerBodyCallback>().Damage(false);
        }

        GameObject smokeInstance = GameObject.Instantiate(smoke);
        smokeInstance.transform.position = new Vector2(transform.position.x + (transform.right.x * smokeCorrection.x), transform.position.y + smokeCorrection.y);
        Destroy(gameObject);
    }
}
