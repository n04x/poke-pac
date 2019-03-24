using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GengarMovement : MonoBehaviour
{
    #region Script Variables
    private PhotonView PV;
    private NavMeshAgent nav_mesh_agent;
    private MeshCollider mesh_collider;
    private PokemonListBehaviour pokemon_list;

    Vector3 syncPos = Vector3.zero;
    Quaternion syncRot = Quaternion.identity;

    public Transform temp_target;

    #endregion


    #region MonoBehaviour Functions
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        nav_mesh_agent = GetComponent<NavMeshAgent>();
        mesh_collider = GetComponent<MeshCollider>();
        pokemon_list = GameObject.Find("Pokemon List").GetComponent<PokemonListBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        AvatarPokemonSetup nearest_pokemon = FindClosestPokemon();
        if(!PV.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, syncPos, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, syncRot, 0.1f);
            return;
        }
        else
        {
            PV.RPC("UpdateGengarTransform", RpcTarget.Others, transform.position, transform.rotation);
        }
        
        nav_mesh_agent.SetDestination(nearest_pokemon.transform.position);
    }

    private AvatarPokemonSetup FindClosestPokemon()
    {
        if(pokemon_list == null)
        {
            pokemon_list = GameObject.Find("Pokemon List").GetComponent<PokemonListBehaviour>();
        }
        ICollection<AvatarPokemonSetup> pokemons = pokemon_list.GetPokemon();
        float distance = Int32.MaxValue;    // assign a ridiculous distance to be sure that will be override.
        AvatarPokemonSetup closest_pokemon = null;
        foreach(AvatarPokemonSetup p in pokemons)
        {
            float temp_d = Vector3.Distance(transform.position, p.transform.position);
            if(temp_d < distance)
            {
                distance = temp_d;
                closest_pokemon = p;
            }
        }
        return closest_pokemon;
    }

    #endregion

    #region PunRPC Functions
    [PunRPC] void UpdateGengarTransform(Vector3 pos, Quaternion rot)
    {
        syncPos = pos;
        syncRot = rot;
    }

    #endregion
}
