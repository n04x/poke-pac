using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PokemonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject my_pokemon;
    List<int> spawn_indexes = new List<int> { 0, 1, 2, 3 };
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawn_pos_pick = (PV.ViewID / 1000) - 1;
        if (PV.IsMine)
        {
            Debug.LogWarning("The coordinate for our spawn pos: " + GameSetup.GS.spawn_positions[spawn_pos_pick]);
            Debug.LogWarning("The spawn pos number is: " + spawn_pos_pick);
            my_pokemon = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PokemonAvatar"), 
                GameSetup.GS.spawn_positions[spawn_pos_pick].position, GameSetup.GS.spawn_positions[spawn_pos_pick].rotation, 0);
        }
        PV.RPC("UpdateSpawnIndexes", RpcTarget.AllBuffered, spawn_indexes);
    }

    [PunRPC] void UpdateSpawnIndexes(List<int> si)
    {
        spawn_indexes.RemoveAt(0);
        spawn_indexes = si;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
