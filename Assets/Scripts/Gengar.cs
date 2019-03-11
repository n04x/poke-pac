using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Gengar : MonoBehaviour
{
    private NavMeshAgent nav_mesh_agent;
    private MeshCollider mesh_collider;
    //private PlayerList player_list;

    void Start() {
        nav_mesh_agent = GetComponent<NavMeshAgent>();
        mesh_collider = GetComponentInChildren<MeshCollider>();
        // player_list = GameObject.Find("PlayerList").GetComponent<PlayerList>();
    }

    void Update() {
            
    }
}
