using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionBuff : MonoBehaviour
{
    public List<GameObject> evolution_go;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<PokemonMovement>().evolve_pokemon = evolution_go;
    }
    private void Update()
    {
        foreach(Transform child in transform)
        {
            if(child.tag == "evolve" || child.tag == "default")
            {
                AddEvolution(child.gameObject);
            }
        }
        GetComponentInParent<PokemonMovement>().evolve_pokemon = evolution_go;
    }

    void AddEvolution(GameObject go)
    {
        if(!evolution_go.Contains(go))
        {
            evolution_go.Add(go);
        }
    }
}
