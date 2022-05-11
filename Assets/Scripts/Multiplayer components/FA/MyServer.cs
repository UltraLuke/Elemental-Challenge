using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MyServer : MonoBehaviourPun
{
    public static MyServer Instance; //SINGLETON

    Player _server; //Referencia del Host Real (y no de los avatares)

    //Prefab del Model a instanciar cuando se conecte un jugador
    public CharacterFA characterPrefab;

    Dictionary<Player, CharacterFA> _dicModels = new Dictionary<Player, CharacterFA>();

    public int PackagePerSecond { get; private set; }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if(Instance == null)
        {
            if (photonView.IsMine)
            {
                photonView.RPC("SetServer", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer);
            }
        }
    }

    [PunRPC]
    void SetServer(Player serverPlayer)
    {
        if (Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        _server = serverPlayer;

        PackagePerSecond = 60;

        var playerLocal = PhotonNetwork.LocalPlayer;

        if(playerLocal != _server)
        {
            photonView.RPC("AddPlayer", _server, playerLocal);
        }
    }

    [PunRPC]
    void AddPlayer(Player player)
    {
        //START COROUTINE
        CharacterFA newCharacter = PhotonNetwork.Instantiate(characterPrefab.name, Vector3.zero, Quaternion.identity).GetComponent<CharacterFA>();
        _dicModels.Add(player, newCharacter);


    }

    #region REQUESTS (SERVERS AVATARES)

    //Esto lo recibe del Controller y llama por RPC a la funcion MOVE del host real
    public void RequestMove(Player player, Vector3 dir)
    {
        photonView.RPC("Move", _server, player, dir);
    }

    #endregion

    #region SERVER ORIGINAL

    [PunRPC]
    void Move(Player player, Vector3 dir)
    {

        if (_dicModels.ContainsKey(player))
        {
            //_dicModels[player].Move(dir);
        }
    }

    #endregion
}
