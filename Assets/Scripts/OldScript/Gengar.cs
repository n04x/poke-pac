using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Photon.Pun;

public class Gengar : MonoBehaviourPun
{
    private NavMeshAgent nav_mesh_agent;
    private MeshCollider mesh_collider;
    // private PokemonList pokemon_list;
    public List<GameObject> pikachu_list;
    void Start() {
        nav_mesh_agent = GetComponent<NavMeshAgent>();
        mesh_collider = GetComponentInChildren<MeshCollider>();
        // pikachu_list = GameObject.Find("Player").GetComponent<Pikachu>();
        pikachu_list .Add(GameObject.FindGameObjectWithTag("Player"));
    }

    void Update() {
        if(pikachu_list == null) {
            pikachu_list.Add(GameObject.FindGameObjectWithTag("Player"));
        } else
        {
            GameObject closest_target = GetPikachu();
            nav_mesh_agent.SetDestination(closest_target.transform.position);
        }
    }

    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
    //     if(stream.IsWriting) {
    //         stream.SendNext(transform.position);
    //         stream.SendNext(transform.rotation);
    //     } 
    //     else
    //     {
    //         this.transform.position = (Vector3)stream.ReceiveNext();
    //         this.transform.rotation = (Quaternion)stream.ReceiveNext();
    //     }
    // }
    // public void AddPikachu(Pikachu p) {
    //     Debug.Log("pikachu has been added");
    //     pikachu_list.Add(p);
    // }
    GameObject GetPikachu() {
        float distance = 100000f;
        GameObject closest_pikachu = null;
        foreach (GameObject p in pikachu_list)
        {
            if(p != null) {
                float d = Vector3.Distance(transform.position, p.transform.position);
                if(d < distance) {
                distance = d;
                closest_pikachu = p;
                }
            }
            
        }
        return closest_pikachu;
    }
}
