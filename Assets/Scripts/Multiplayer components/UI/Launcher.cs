using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject mainScreen, connectedScreen;

    public void BtnConnect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public void BtnDisconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        connectedScreen.SetActive(true);
        mainScreen.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Connection failed: " + cause.ToString());
        mainScreen.SetActive(true);
        connectedScreen.SetActive(false);
    }
}
