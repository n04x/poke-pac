using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Lobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private byte max_player = 4;
    [SerializeField] private GameObject control_panel;
    [SerializeField] private GameObject connection_progress;
    bool is_connecting;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;    
    }

    private void Start()
    {
        connection_progress.SetActive(false);
        control_panel.SetActive(true);
    }
    public void Connect()
    {
        is_connecting = true;
        connection_progress.SetActive(true);
        control_panel.SetActive(false);

        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        } else
        {
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        if(is_connecting) {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connection_progress.SetActive(false);
        control_panel.SetActive(true);

        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = max_player });
    }
    
    public override void OnJoinedRoom() {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1) {
            PhotonNetwork.LoadLevel("Game 1");
        }
    }
}
