using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour {

    public PlayerBody playerBody;
    private string axis = "Horizontal2";
    private string axisJoy = "HorizontalStick2";
    private string jump = "Jump2";
    private string attack = "Attack2";
    private string fire = "Fire2";

    public bool specialActive;

    public bool isPaused;

    void Start ()
	{
        playerBody = GetComponent<PlayerBody>();
        isPaused = false;
    }

	void Update ()
	{
        if(!isPaused)
		    CheckKeys();
	}

	public void CheckKeys()
	{
        float hAxis;

        hAxis = Input.GetAxis(axis);
        if (Input.GetAxis(axisJoy) != 0)
        {
            hAxis = Input.GetAxis(axisJoy);
        }

        if (Input.GetButtonDown(jump))
        {
            playerBody.Jump2();
        }

        if (Input.GetButtonDown(attack))
        {
            playerBody.Attack();
        }

        if (Input.GetButtonDown(fire))
        {
            playerBody.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerBody.FlyJump(false);
        }

        playerBody.Move(hAxis);
    }
}
