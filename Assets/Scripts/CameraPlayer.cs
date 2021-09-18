using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour {

	private Vector2 velocity;
	private Camera cameraSelf;

    [Header("Player To Follow")]
    public GameObject player;

    [Header("Camera Following Settings")]
    public Vector2 setLockPosition;
    public bool[] lockXOrY = new bool[2];
    public float[] smoothTimeXY = new float[2];

    //public float smoothTimeY;
    //public Transform player;
	//public bool secCamera;
	//private float setY;

	void Start()
	{
		cameraSelf = GetComponent<Camera>();


		/*if(!Game.player2Game)
		{
			gameObject.SetActive(false);
		}

		setY = 5f;*/

        if(player.gameObject.layer == 8)
            cameraSelf.rect = new Rect(0.0f,0.0f,0.5f,1.0f);
        else if(player.gameObject.layer == 9)
            cameraSelf.rect = new Rect(0.5f,0.0f,0.5f,1.0f);
	}

	void Update ()
    {
        Vector3 focusPoint;

        focusPoint = player.transform.position;

        Focus(focusPoint);
	}

    public void Focus(Vector3 focusPoint)
    {
        float posY;
        float posX;

        if (lockXOrY[1] || focusPoint.y < setLockPosition.y)
        {
            posY = Mathf.SmoothDamp(transform.position.y, setLockPosition.y, ref velocity.y, smoothTimeXY[1]);
        }
        else
        {
            posY = Mathf.SmoothDamp(transform.position.y, focusPoint.y, ref velocity.y, smoothTimeXY[1]);
        }

        transform.position = new Vector3(transform.position.x, posY, transform.position.z);

        if (lockXOrY[0] || focusPoint.x < setLockPosition.x)
        {
            posX = Mathf.SmoothDamp(transform.position.x, setLockPosition.x, ref velocity.x, smoothTimeXY[0]);
        }
        else
        {
            posX = Mathf.SmoothDamp(transform.position.x, focusPoint.x, ref velocity.x, smoothTimeXY[0]);
        }

        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }
}
