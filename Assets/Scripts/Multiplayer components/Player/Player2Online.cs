using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player2Online : MonoBehaviourPun, IPunObservable
{

    public PlayerBodyOnline playerBody;
    public string axis = "Horizontal1";
    public string axisJoy = "HorizontalStick1";
    public string jump = "Jump1";
    public string attack = "Attack1";
    public string fire = "Fire1";

    public bool specialActive;
    public bool isPaused;

    void Start ()
	{
        if (!photonView.IsMine) return;

        playerBody = GetComponent<PlayerBodyOnline>();
        isPaused = false;
    }

	void Update ()
	{
        if (!photonView.IsMine) return;

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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Estoy escribiendo?
        if (stream.IsWriting)
        {
            //Envío
        }
        else
        {
            //Recibo
        }
    }
}
