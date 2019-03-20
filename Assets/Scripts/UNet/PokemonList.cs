using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonList : MonoBehaviour
{
    [SerializeField] private List<Pokemon> pokemon_list = new List<Pokemon>();
    // Update is called once per frame
    void Update()
    {
        CheckForGameOver();
    }

    private void CheckForGameOver()
    {
        // TODO: Game Over Condition
    }

    public void AddPokemonPlayer(Pokemon p)
    {
        if(!pokemon_list.Contains(p))
        {
            pokemon_list.Add(p);
        }
    }

    public ICollection<Pokemon> GetPokemonPlayer()
    {
        return pokemon_list;
    }
}
