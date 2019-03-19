using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerDeath : NetworkBehaviour
{
    private PokemonBehaviour pokemon_behaviour_script;

    // Start is called before the first frame update
    void Start()
    {
        pokemon_behaviour_script = GetComponent<PokemonBehaviour>();
        pokemon_behaviour_script.EventDie += DisablePlayer;
    }

    void OnDisable()
    {
        pokemon_behaviour_script.EventDie -= DisablePlayer;        
    }

    void DisablePlayer()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponentInChildren<MeshRenderer>().enabled = false;

        pokemon_behaviour_script.is_dead = true;
    }

}
