using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gengar : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent nav_mesh_agent;
    private MeshCollider mesh_collider;
    //private PlayerList player_list;

    void Start() {
        nav_mesh_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        mesh_collider = GetComponentInChildren<MeshCollider>();

    }
}
