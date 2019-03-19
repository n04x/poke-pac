using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GengarMovement : MonoBehaviour
{
    public static GengarMovement g;
    private PhotonView PV;
    private NavMeshAgent nav_mesh_agent;
    private MeshCollider mesh_collider;
    Vector3 syncPos = Vector3.zero;
    Quaternion syncRot = Quaternion.identity;
    public List<GameObject> pokemon_list;


    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        nav_mesh_agent = GetComponent<NavMeshAgent>();
        mesh_collider = GetComponent<MeshCollider>();
        //pokemon_list.Add(GameObject.FindGameObjectWithTag("Player"));
    }

    // Update is called once per frame
    void Update()
    {
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
        if(pokemon_list.Count < 2)
        {
            pokemon_list.Add(GameObject.FindGameObjectWithTag("Player"));
        }
        GameObject closest_target = GetPokemon();
        nav_mesh_agent.SetDestination(closest_target.transform.position);
    }

    [PunRPC] void UpdateGengarTransform(Vector3 pos, Quaternion rot)
    {
        syncPos = pos;
        syncRot = rot;
    }
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(transform.position);
    //        stream.SendNext(transform.rotation);
    //    }
    //    else
    //    {
    //        this.transform.position = (Vector3)stream.ReceiveNext();
    //        this.transform.rotation = (Quaternion)stream.ReceiveNext();
    //    }
    //}

    private GameObject GetPokemon()
    {
        float distance = 100000f;
        GameObject closest_pokemon = null;
        foreach(GameObject p in pokemon_list)
        {
            if(p != null)
            {
                float d = Vector3.Distance(transform.position, p.transform.position);
                if(d < distance)
                {
                    distance = d;
                    closest_pokemon = p;
                }
            }
        }

        return closest_pokemon;
    }

    [PunRPC] public void AddPokemon(GameObject p)
    {
        pokemon_list.Add(p);
    }

    [PunRPC] void RPC_GengarPosition()
    {

    }
}
