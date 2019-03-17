using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PikachuManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene.")]
    public static GameObject local_pikachu_instance;

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField] public GameObject player_ui_prefab;
    public int scores;
    // Start is called before the first frame update
    void Awake() {
        if(photonView.IsMine) {
            PikachuManager.local_pikachu_instance = this.gameObject;

        }
        DontDestroyOnLoad(this.gameObject);    
    }

    void Start() {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, loadingMode) =>
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        };
        GameObject go = Instantiate(player_ui_prefab);
        // go.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);   
        go.GetComponent<PikachuUI>().SetTarget(this); 
    }
    void CalledOnLevelWasLoaded(int level) {
        GameObject go = Instantiate(this.player_ui_prefab);
        go.GetComponent<PikachuUI>().SetTarget(this);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        
    }
}
