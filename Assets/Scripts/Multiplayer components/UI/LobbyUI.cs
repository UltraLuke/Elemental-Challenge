using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyUI : MonoBehaviourPunCallbacks
{
    public InputField lobbyInputField;
    public GameObject onlineLobbyScreen, waitingPlayersScreen, searchingPlayersScreen, notFoundScreen;
    public GameObject characterSelectionScreen;
    public GameObject playerDisconnected;
    public Text errorMessage;

    //bool _selfDisconnected;
    //bool _onPlayerSelectionScreen;
    int _maxPlayerCount = 2;
    int _currentPlayerCount = 0;

    public void BtnCreateRoom()
    {
        RoomOptions option = new RoomOptions();
        option.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(lobbyInputField.text, option);
    }
    public void BtnLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void BtnJoinRoom()
    {
        if (lobbyInputField.text == "")
            errorMessage.enabled = true;
        else
        {
            PhotonNetwork.JoinRoom(lobbyInputField.text);
            onlineLobbyScreen.SetActive(false);
            searchingPlayersScreen.SetActive(true);
            errorMessage.enabled = false;
        }
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("Tutorial Online");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created");
        onlineLobbyScreen.SetActive(false);
        waitingPlayersScreen.SetActive(true);
    }
    public override void OnLeftRoom()
    {
        Debug.Log("Room left");
        onlineLobbyScreen.SetActive(true);
        waitingPlayersScreen.SetActive(false);
        searchingPlayersScreen.SetActive(false);
        playerDisconnected.SetActive(false);
        characterSelectionScreen.SetActive(false);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed create room " + returnCode + " Message: " + message);
    }


    public override void OnJoinedRoom()
    {
        //PhotonNetwork.LoadLevel("Tutorial Online");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New player entered the room");

        photonView.RPC("RPCStartGame", RpcTarget.All);
        //PlayerSettings.charactersSelected = new int[]{ 0, 1};
        //PhotonNetwork.LoadLevel("Tutorial Online");
        //RPCPlSel();
        //photonView.RPC("RPCPlSel", RpcTarget.Others);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left the room");
        playerDisconnected.SetActive(true);
        characterSelectionScreen.SetActive(false);
        waitingPlayersScreen.SetActive(false);
        searchingPlayersScreen.SetActive(false);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room. " + returnCode + " Message: " + message);
        notFoundScreen.SetActive(true);
        searchingPlayersScreen.SetActive(false);
    }

    [PunRPC]
    void RPCPlSel()
    {
        characterSelectionScreen.SetActive(true);
        searchingPlayersScreen.SetActive(false);
        waitingPlayersScreen.SetActive(false);
        onlineLobbyScreen.SetActive(false);
    }
    [PunRPC]
    void RPCStartGame()
    {
        PlayerSettings.charactersSelected = new int[] { 0, 1 };
        PhotonNetwork.LoadLevel("Tutorial Online");
    }
}