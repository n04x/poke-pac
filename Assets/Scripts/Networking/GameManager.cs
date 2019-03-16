using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Tooltip("the prefab that represent the player!")]
    public GameObject player_prefab;

    [SerializeField] private Transform[] spawn_position;
    private void Start() {
        if(player_prefab == null) {
            Debug.LogError("<Color=Red><a>Missing</a></Color> Player Prefab. Please set it up in GameObject Game Manager'", this);
        }
        else
        {
            if(PikachuManager.local_pikachu_instance == null) {
                int player_count = PhotonNetwork.PlayerList.Length;
                PhotonNetwork.Instantiate(this.player_prefab.name, spawn_position[player_count - 1].position, Quaternion.identity, 0);
            }
        }
    }

    // public void OnJoinedRoom() {
        
    // }
    public void LeaveRoom() {
        PhotonNetwork.LeaveRoom();
    }

    void LoadGame() {
        if(!PhotonNetwork.IsMasterClient) {
            Debug.LogError("PhotonNetwork: Trying to laod a level but we are not the master client!");
        }
        PhotonNetwork.LoadLevel("Game " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    public override void OnLeftRoom() {
        SceneManager.LoadScene(0);  // the connect to play area.
    }

    public override void OnPlayerEnteredRoom(Player other) {
        if(PhotonNetwork.IsMasterClient) {
            LoadGame();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        if(PhotonNetwork.IsMasterClient) {
            LoadGame();
        }
    }
}   
