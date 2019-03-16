using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonList : MonoBehaviour
{
    private List<Pikachu> pokemon_list = new List<Pikachu>();

    // Update is called once per frame
    void Update()
    {
        // GameOver();    
    }

    // void GameOver() {
    //     if(pokemon_list.Count == 0) {
    //         return;
    //     }
    //     foreach (Pikachu p in pokemon_list)
    //     {
    //         if(!p.is_dead) {
    //             return;
    //         }
    //     }
    //     // GameObject.FindObjectOfType<Stats>().pellet_count = 0;
    // }
    
    public void AddPokemon(Pikachu p) {
        if(!pokemon_list.Contains(p)) {
            pokemon_list.Add(p);
        }
    }

    public ICollection<Pikachu> GetPokemon() {
        return pokemon_list;
    }
}
