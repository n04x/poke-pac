using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviour
{
    public static PhotonLobby lobby;

    public GameObject start_button;
    public GameObject search_button;

    private void Awake() {
        lobby = this;   // create the singleton, lives within the Menu scene.
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
