using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSyncPosition : NetworkBehaviour
{
    [SyncVar] private Vector3 syncPos;
    [SyncVar] private Quaternion syncRot;

    [SerializeField] Transform myTransform;
    [SerializeField] float lerpRate = 15;

    // Start is called before the first frame update
    private void FixedUpdate()
    {
        TransmitPosition();
        LerpPosition();
    }

    void LerpPosition()
    {
        if(!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
            myTransform.rotation = Quaternion.Lerp(myTransform.rotation, syncRot, Time.deltaTime * lerpRate);

        }
    }

    [Command] void CmdProvidePositionToServer(Vector3 pos, Quaternion rot)
    {
        syncPos = pos;
        syncRot = rot;
    }

    [ClientCallback] void TransmitPosition()
    {
        if(isLocalPlayer)
        {
            CmdProvidePositionToServer(myTransform.position, myTransform.rotation);
        }
    }
}
