using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class GengarAI : NetworkBehaviour
{
    public Transform target;
    [SyncVar] Vector3 syncPos = Vector3.zero;
    [SyncVar] Quaternion syncRot = Quaternion.identity;
    NavMeshAgent nav_agent;
    [SerializeField] Transform gengar_transform;
    [SerializeField] float lerpRate = 15;
    [SerializeField] private PokemonList pokemon_list;

    private void Start()
    {
        nav_agent = GetComponent<NavMeshAgent>();
        //nav_agent.destination = target.position;
    }
    private void Update()
    {
        ////Pokemon nearest_pokemon = GetNearestPokemon();
        //if(nearest_pokemon != null)
        //{
        //    if(IsPokemonHit(nearest_pokemon))
        //    {
        //        nearest_pokemon.KillPokemon();
        //    }
        //    else
        //    {
        //        nav_agent.SetDestination(nearest_pokemon.transform.position);
        //    }
        //}
        TransmitGengarPosition();
        LerpGengarPosition();
    }

    private bool IsPokemonHit(Pokemon nearest_pokemon)
    {
        throw new NotImplementedException();
    }

    //private Pokemon GetNearestPokemon()
    //{
    //    if(pokemon_list == null)
    //    {
    //        pokemon_list = GameObject.Find("Pokemon List").GetComponent<PokemonList>();
    //    }
    //}

    void LerpGengarPosition()
    {
        if (!isLocalPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime * lerpRate);
            transform.rotation = Quaternion.Lerp(transform.rotation, syncRot, Time.deltaTime * lerpRate);
        }
    }
    [Command]
    void CmdTransmitGengarPosition(Vector3 pos, Quaternion rot)
    {
        //RpcGengarSetDestination(target.position);
        //nav_agent.SetDestination(target.position);
        syncPos = pos;
        syncRot = rot;
    }

    [ClientCallback]
    void TransmitGengarPosition()
    {
        CmdTransmitGengarPosition(gengar_transform.position, gengar_transform.rotation);
    }

    [ClientRpc]
    public void RpcGengarSetDestination(Vector3 target)
    {
        nav_agent.SetDestination(target);
    }
}
