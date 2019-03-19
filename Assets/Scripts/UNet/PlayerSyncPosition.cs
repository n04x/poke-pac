﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSyncPosition : NetworkBehaviour
{
    [SyncVar] private Vector3 syncPos;
    [SyncVar] private float syncRot;

    [SerializeField] Transform myTransform;
    [SerializeField] float lerpRate = 15;

    // Start is called before the first frame update
    private void Update()
    {
        TransmitPosition();
        LerpPosition();
    }

    void LerpPosition()
    {
        if(!isLocalPlayer)
        {
            Vector3 newRotation = new Vector3(0, syncRot, 0);
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
            myTransform.rotation = Quaternion.Lerp(myTransform.rotation, Quaternion.Euler(newRotation), Time.deltaTime * lerpRate);

        }
    }

    [Command] void CmdProvidePositionToServer(Vector3 pos, float rot)
    {
        syncPos = pos;
        syncRot = rot;
    }

    [ClientCallback] void TransmitPosition()
    {
        if(isLocalPlayer)
        {
            CmdProvidePositionToServer(myTransform.position, myTransform.localEulerAngles.y);
        }
    }
}
