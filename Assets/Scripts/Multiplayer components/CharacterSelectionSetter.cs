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

    void PlayerReady(bool ready)
    {
        if (!ready)
        {
            _playersReady = _playersReady <= 0 ? 0 : _playersReady - 1;
            return;
        }

        _playersReady++;
        if(_playersReady == _maxPlayers && PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Empezar el juego");
            //EMPEZAR JUEGO
        }
    }
}
