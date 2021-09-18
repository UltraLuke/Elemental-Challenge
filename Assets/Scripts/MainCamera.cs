using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	private Vector2 velocity;
	private Camera cameraSelf;
    //public float smoothTimeY;
    [Header("Players")]
	public GameObject mainPlayer;
	public GameObject secPlayer;
	//public bool split;
    /*
	private float setY;
    private float setX;
    */
    
    [Header("Camera Following Settings")]
    public Vector2 setLockPosition;
    public bool[] lockXOrY = new bool [2];
    public float[] smoothTimeXY = new float [2];

    private float differenceY;
	//private int lastPlayerAlive = 0;

	void Start()
	{
		//split = false;
		cameraSelf = GetComponent<Camera>();

        /*if(!Game.player2Game)
		{
			cameraSelf.depth = 0;
		}
		else
		{
			cameraSelf.depth = 1;
		}*/

        //setY = 1f;

	}

	void Update ()
	{
		Vector3 focusPoint;
        //differenceY = Mathf.Abs(mainPlayer.transform.position.y - secPlayer.transform.position.y);

        /*if(Game.player2Game)
		{
			if(secPlayer.gameObject.activeSelf && mainPlayer.gameObject.activeSelf)
			{
				focusPoint = new Vector3(0f, (mainPlayer.transform.position.y + secPlayer.transform.position.y) / 2, gameObject.transform.position.z);
			}
			else if(secPlayer.gameObject.activeSelf || lastPlayerAlive == 2)
			{
				focusPoint = secPlayer.transform.position;
				lastPlayerAlive = 2;
			}
			else if(mainPlayer.gameObject.activeSelf || lastPlayerAlive == 1)
			{
				focusPoint = mainPlayer.transform.position;
				lastPlayerAlive = 1;
			}
			else
			{
				focusPoint = new Vector3(0f, (mainPlayer.transform.position.y + secPlayer.transform.position.y) / 2, gameObject.transform.position.z);
			}
		}
		else
		{
			focusPoint = mainPlayer.transform.position;
		}*/

        focusPoint = mainPlayer.transform.position;

       /* if (Game.player2Game)
		{
			SortCamera();
		}*/

		Focus(focusPoint);
	}

	public void Focus(Vector3 focusPoint)
	{
        float posY;// = Mathf.SmoothDamp(transform.position.y, focusPoint.y, ref velocity.y, smoothTimeXY[1]);
        float posX;// = Mathf.SmoothDamp(transform.position.x, focusPoint.x, ref velocity.x, smoothTimeXY[0]);

        //float posLockX = Mathf.SmoothDamp(transform.position.x, setLockPosition.x, ref velocity.x, smoothTimeXY[0]);
        //float posLockY = Mathf.SmoothDamp(transform.position.y, setLockPosition.y, ref velocity.y, smoothTimeXY[1]);

        /*if(focusPoint.y >= setY)
			transform.position = new Vector3(transform.position.x, posY, transform.position.z);
		else
			transform.position = new Vector3 (transform.position.x, setY, transform.position.z);*/
        //================================================================================================================
        /*if (!lockXOrY[1])
        {
            if (focusPoint.y >= setLockPosition.y)
                transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x, setLockPosition.y, transform.position.z);
        }*/

        if (lockXOrY[1] || focusPoint.y < setLockPosition.y)
        {
            posY = Mathf.SmoothDamp(transform.position.y, setLockPosition.y, ref velocity.y, smoothTimeXY[1]);
        }
        else
        {
            posY = Mathf.SmoothDamp(transform.position.y, focusPoint.y, ref velocity.y, smoothTimeXY[1]);
        }

        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        /*if (!lockXOrY[0])
        {
            if (focusPoint.x >= setLockPosition.x)
                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(setLockPosition.x, transform.position.y, transform.position.z);
        }*/

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

	/*public void SortCamera()
	{
		if((differenceY < 3 && secPlayer.gameObject.activeSelf && mainPlayer.gameObject.activeSelf) || !(secPlayer.gameObject.activeSelf && mainPlayer.gameObject.activeSelf))
		{
			cameraSelf.depth = 1;
			split = false;
		}
		else
		{
			cameraSelf.depth = -1;
			split = true;
		}
	}*/
}
