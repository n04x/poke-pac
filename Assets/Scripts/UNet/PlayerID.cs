using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerID : NetworkBehaviour
{
    [SyncVar] private string player_unique_id;
    private NetworkInstanceId player_net_id;
    private Transform player_transform;

    public override void OnStartLocalPlayer()
    {
        GetNetID();
        SetID();
    }
    // Start is called before the first frame update
    void Awake()
    {
        player_transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player_transform.name == "" || player_transform.name == "PokemonAvatar(Clone)")
        {
            SetID();
        }
    }

    [Client] void GetNetID()
    {
        player_net_id = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyId(MakeUniqueId());
    }

    void SetID()
    {
        if(!isLocalPlayer)
        {
            player_transform.name = player_unique_id;
        }
        else
        {
            player_transform.name = MakeUniqueId();
        }
    }

    string MakeUniqueId()
    {
        string unique_name = "Pokemon " + player_net_id.ToString();
        return unique_name;
    }

    [Command] void CmdTellServerMyId(string name)
    {
        player_unique_id = name;
    }
}
