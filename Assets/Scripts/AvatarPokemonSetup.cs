using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarPokemonSetup : MonoBehaviour
{
    private PhotonView PV;
    public int pokemon_value;
    public GameObject my_pokemon;
    public int scores;
    public string player_name;
    public Transform start_position;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine)
        {
            int spawn_pos_pick = (PV.ViewID / 1000) - 1;
            start_position = GameSetup.GS.spawn_positions[spawn_pos_pick]; //keep it as starting position when killed by Gengar.
            PV.RPC("RPC_AddPokemon", RpcTarget.AllBuffered, PokemonInfo.PI.selected_pokemon);
            
        }
    }

    void Update()
    {
        CheckNegativeScore();        
    }

    private void CheckNegativeScore()
    {
        if(scores < 0)
        {
            scores = 0;
        }
    }

    [PunRPC] void RPC_AddPokemon(int which_pokemon)
    {
        pokemon_value = which_pokemon;
        my_pokemon = Instantiate(PokemonInfo.PI.all_pokemons[which_pokemon], transform.position, transform.rotation, transform);
    }
}
