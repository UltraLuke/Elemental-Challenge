using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    //public MainCamera mainCamera;
    public string playerPrefName;

    private void Start()
    {
        PhotonNetwork.Instantiate("Players/" + playerPrefName, transform.position, Quaternion.identity);
    }
}
