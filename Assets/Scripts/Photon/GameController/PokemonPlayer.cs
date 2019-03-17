using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PokemonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject my_pokemon;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawn_pos_pick = Random.Range(0, GameSetup.GS.spawn_positions.Length);
        if(PV.IsMine)
        {
            Debug.Log("The coordinate for our spawn pos: " + GameSetup.GS.spawn_positions[spawn_pos_pick]);
            Debug.Log("The spawn pos number is: " + spawn_pos_pick);
            my_pokemon = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PokemonAvatar"), 
                GameSetup.GS.spawn_positions[spawn_pos_pick].position, GameSetup.GS.spawn_positions[spawn_pos_pick].rotation, 0);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
