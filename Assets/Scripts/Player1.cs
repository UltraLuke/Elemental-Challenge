using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {

	public PlayerBody playerBody;
    private string axis = "Horizontal1";
    private string axisJoy = "HorizontalStick1";
    private string jump = "Jump1";
	private string attack = "Attack1";
    private string fire = "Fire1";

    public bool specialActive;

    public bool isPaused;

    void Start ()
	{
		playerBody = GetComponent<PlayerBody>();
        isPaused = false;
    }

	void Update ()
	{
        if (!isPaused)
            CheckKeys();
    }

	public void CheckKeys()
	{
		float hAxis;

    	hAxis = Input.GetAxis(axis);
        if(Input.GetAxis(axisJoy) != 0)
        {
            hAxis = Input.GetAxis(axisJoy);
        }

        if (Input.GetButtonDown(jump))
        {
            playerBody.Jump1();
        }

        if(Input.GetButtonDown(attack))
		{
			playerBody.Attack();
		}

        if (Input.GetButtonDown(fire))
        {
            playerBody.Shoot();
        }

        playerBody.Move(hAxis);
	}
}
