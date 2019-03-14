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
                PhotonNetwork.Instantiate(this.player_prefab.name, new Vector3(0f, 1f, 0f), Quaternion.identity, 0);
            }
        }
    }

    public void LeaveRoom() {
        PhotonNetwork.LeaveRoom();
    }

    void LoadGame() {
        if(!PhotonNetwork.IsMasterClient) {
            Debug.LogError("PhotonNetwork: Trying to laod a level but we are not the master client!");
        }
        PhotonNetwork.LoadLevel("Game");
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
