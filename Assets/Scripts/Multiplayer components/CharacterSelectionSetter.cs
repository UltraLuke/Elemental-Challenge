using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CharacterSelectionSetter : MonoBehaviourPunCallbacks
{
    public PlayerSelectionUI[] charSelectors;
    public Button readyBtn, notReadyBtn;

    int _playersReady;
    int _maxPlayers = 2;
    int[] _charactersSelected = new int[2];

    public void OnEnable()
    {
        _playersReady = 0;
        readyBtn.gameObject.SetActive(true);
        notReadyBtn.gameObject.SetActive(false);

        int index;
        if (PhotonNetwork.IsMasterClient)
            index = 0;
        else
            index = 1;

        //for (int i = 0; i < charSelectors.Length; i++)
        //{
        //    if (i == index)
        //        charSelectors[i].StartWithCharacter(index);
        //}

        for (int i = 0; i < charSelectors.Length; i++)
        {
            if(i == index)
            {
                charSelectors[index].StartWithCharacter(index, PlayerReady);
            }
            else
            {
                charSelectors[i].DisableSelector(PlayerReady);
            }
        }
    }

    void PlayerReady(bool ready, int character, bool master)
    {
        int playerNum = master ? 0 : 1;

        if (!ready)
        {
            _playersReady = _playersReady <= 0 ? 0 : _playersReady - 1;
            return;
        }
        else
        {
            _playersReady++;
            _charactersSelected[playerNum] = character;
        }

        if(_playersReady == _maxPlayers && PhotonNetwork.IsMasterClient)
        {
            PlayerSettings.charactersSelected = _charactersSelected;
            Debug.Log("Empezar el juego");
            FindObjectOfType<LobbyUI>().StartGame();
            //EMPEZAR JUEGO
        }
    }
}
