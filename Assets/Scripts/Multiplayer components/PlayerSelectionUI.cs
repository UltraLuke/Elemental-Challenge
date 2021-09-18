using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSelectionUI : MonoBehaviourPunCallbacks
{
    public GameObject onlineLobbyScreen;
    public GameObject playerSelectionUIScreen;

    //bool _selfMade;

    //private void Start()
    //{
    //    _selfMade = false;
    //}

    //public void BtnLeftRoom()
    //{
    //    _selfMade = true;
    //    PhotonNetwork.LeaveRoom();
    //}

    //public override void OnLeftRoom()
    //{
    //    Debug.Log("Room left");

    //    if (_selfMade)
    //        onlineLobbyScreen.SetActive(true);
    //    else
    //        disconnectedFromRoom.SetActive(true);

    //    playerSelectionUIScreen.SetActive(false);
    //}
}
