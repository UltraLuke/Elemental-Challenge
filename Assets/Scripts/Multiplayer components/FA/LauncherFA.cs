using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LauncherFA : MonoBehaviourPunCallbacks
{
    public MyServer serverPrefab;
    public ControllerFA controllerPrefab;

    public GameObject mainScreen, connectedScreen;

    public void BtcConnect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;

        PhotonNetwork.JoinOrCreateRoom("SalaFullAuth", options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        PhotonNetwork.Instantiate(serverPrefab.name, Vector3.zero, Quaternion.identity);
    }

    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(controllerPrefab.name, Vector3.zero, Quaternion.identity);
        }
    }
}
