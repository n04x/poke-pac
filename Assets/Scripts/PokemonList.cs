using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonList : MonoBehaviour
{
    private List<Pokemon> pokemon_list = new List<Pokemon>();

    // Update is called once per frame
    void Update()
    {
        GameOver();    
    }

    void GameOver() {
        if(pokemon_list.Count == 0) {
            return;
        }
        foreach (Pokemon p in pokemon_list)
        {
            if(!p.is_dead) {
                return;
            }
        }
        // GameObject.FindObjectOfType<Stats>().pellet_count = 0;
    }
    
    public void AddPokemon(Pokemon p) {
        if(!pokemon_list.Contains(p)) {
            pokemon_list.Add(p);
        }
    }

    public ICollection<Pokemon> GetPokemon() {
        return pokemon_list;
    }
}
