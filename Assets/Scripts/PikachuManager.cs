using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PikachuManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene.")]
    public static GameObject local_pikachu_instance;
    // Start is called before the first frame update
    void Awake() {
        if(photonView.IsMine) {
            PikachuManager.local_pikachu_instance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);    
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

    }
}
