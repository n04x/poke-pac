using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarPokemonSetup : MonoBehaviour
{
    private PhotonView PV;
    public int pokemon_value;
    public GameObject my_pokemon;
    public int scores;

    public int pokemon_health;
    public int pokemon_damage;
    // Start is called before the first frame update
    void Start()
    {
        GengarMovement.g.pokemon_list.Add(gameObject);
        PV = GetComponent<PhotonView>();
        if(PV.IsMine)
        {
            PV.RPC("RPC_AddPokemon", RpcTarget.AllBuffered, PokemonInfo.PI.selected_pokemon);
        }
    }

    [PunRPC] void RPC_AddPokemon(int which_pokemon)
    {
        pokemon_value = which_pokemon;
        my_pokemon = Instantiate(PokemonInfo.PI.all_pokemons[which_pokemon], transform.position, transform.rotation, transform);
    }
}
