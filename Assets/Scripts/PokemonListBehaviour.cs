using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonListBehaviour : MonoBehaviour
{
    private List<AvatarPokemonSetup> pokemon_avatar_list = new List<AvatarPokemonSetup>();

    public void AddPokemon(AvatarPokemonSetup p)
    {
        if(!pokemon_avatar_list.Contains(p))
        {
            pokemon_avatar_list.Add(p);
        }
    }

    public ICollection<AvatarPokemonSetup> GetPokemon()
    {
        return pokemon_avatar_list;
    }
}
