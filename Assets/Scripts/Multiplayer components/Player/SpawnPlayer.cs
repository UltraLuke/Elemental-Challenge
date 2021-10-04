using UnityEngine;
using Photon.Pun;
using System;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    //public MainCamera mainCamera;
    //public string[] playerPrefNames;

    private GameObject SpawnCharacter(string character)
    {
        if (photonView.IsMine)
        {
            return PhotonNetwork.Instantiate("Players/" + character, transform.position, Quaternion.identity);
        }
        else
        {
            return null;
        }
    }
}
