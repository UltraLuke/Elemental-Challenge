using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerSelectionUI : MonoBehaviourPunCallbacks
{
    public GameObject[] characters;
    public GameObject leftArrow, rightArrow;
    public GameObject youIndicator;
    public GameObject checkMark;

    int _currentIndex;
    int _maxIndex;
    bool _updateUI = false;

    bool _myCharacter;
    bool _isReady;

    Action<bool> _readySetter;

    public bool IsReady { get => _isReady; }

    private void Update()
    {
        if (_updateUI && _myCharacter)
        {
            photonView.RPC("RPCUpdateCharacter", RpcTarget.AllBuffered, _currentIndex);
            photonView.RPC("RPCReady", RpcTarget.AllBuffered, false);
            _updateUI = false;
        }
    }
    public void StartWithCharacter(int character, Action<bool> readySetter)
    {
        _maxIndex = characters.Length - 1;
        _currentIndex = character;
        //checkMark.SetActive(false);

        Debug.Log(gameObject.name);
        Debug.Log(_currentIndex);

        //if (photonView.IsMine)
        //{
        //_currentIndex = 0;
        //characters[0].SetActive(true);
        //characters[1].SetActive(false);
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);
        youIndicator.SetActive(true);
        //}
        //else
        //{
        //_currentIndex = 1;
        //characters[1].SetActive(true);
        //characters[0].SetActive(false);
        //    leftArrow.SetActive(false);
        //    rightArrow.SetActive(false);
        //    youIndicator.SetActive(false);
        //}

        //photonView.RPC("RPCUpdateCharacter", RpcTarget.OthersBuffered, _currentIndex);
        //RPCUpdateCharacter();
        //photonView.RPC("RPCUpdateCharacter", RpcTarget.OthersBuffered);
        _updateUI = true;
        _myCharacter = true;
        _readySetter = readySetter;
    }

    public void DisableSelector(Action<bool> readySetter)
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        youIndicator.SetActive(false);
        //checkMark.SetActive(false);
        _myCharacter = false;
        _readySetter = readySetter;
    }

    public void SetReady(bool ready)
    {
        if (_myCharacter)
        {
            photonView.RPC("RPCReady", RpcTarget.All, ready);
        }
    }

    //private void Start()
    //{

    //    if (photonView.IsMine)
    //    {
    //        _currentIndex = 0;
    //        characters[0].SetActive(true);
    //        characters[1].SetActive(false);
    //        leftArrow.SetActive(true);
    //        rightArrow.SetActive(true);
    //        youIndicator.SetActive(true);
    //    }
    //    else
    //    {
    //        _currentIndex = 1;
    //        characters[1].SetActive(true);
    //        characters[0].SetActive(false);
    //        leftArrow.SetActive(false);
    //        rightArrow.SetActive(false);
    //        youIndicator.SetActive(false);
    //    }
    //}

    public void BtnLeft()
    {
        if (_currentIndex == 0)
            _currentIndex = _maxIndex;
        else
            _currentIndex--;

        //RPCUpdateCharacter();
        photonView.RPC("RPCUpdateCharacter", RpcTarget.AllBuffered, _currentIndex);
        //photonView.RPC("RPCUpdateCharacter", RpcTarget.OthersBuffered, _currentIndex);
    }

    public void BtnRight()
    {
        if (_currentIndex == _maxIndex)
            _currentIndex = 0;
        else
            _currentIndex++;

        //RPCUpdateCharacter();
        //photonView.RPC("RPCUpdateCharacter", RpcTarget.OthersBuffered, _currentIndex);
        photonView.RPC("RPCUpdateCharacter", RpcTarget.AllBuffered, _currentIndex);
    }

    [PunRPC]
    void RPCUpdateCharacter(int index)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (i == index)
                characters[i].SetActive(true);
            else
                characters[i].SetActive(false);
        }
    }

    [PunRPC]
    void RPCReady(bool state)
    {
        _readySetter(state);
        checkMark.SetActive(state);
    }


}
