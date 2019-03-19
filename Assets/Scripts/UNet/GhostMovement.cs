using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class GhostMovement : NetworkBehaviour
{
    public Transform target;
    Vector3 syncPos = Vector3.zero;
    Quaternion syncRot = Quaternion.identity;
    [SerializeField] Transform gengar_transform;
    [SerializeField] float lerpRate = 15;

    private void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
    }
    private void Update()
    {
        TransmitGengarPosition();
        LerpGengarPosition();
    }

    void LerpGengarPosition()
    {
        if(!isLocalPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime * lerpRate);
            transform.rotation = Quaternion.Lerp(transform.rotation, syncRot, Time.deltaTime * lerpRate);
        }
    }
    [Command] void CmdTransmitGengarPosition(Vector3 pos, Quaternion rot)
    {
        syncPos = pos;
        syncRot = rot;
    }

    [ClientCallback] void TransmitGengarPosition()
    {
        if(isLocalPlayer)
        {
            CmdTransmitGengarPosition(gengar_transform.position, gengar_transform.rotation);
        }
    }
}
