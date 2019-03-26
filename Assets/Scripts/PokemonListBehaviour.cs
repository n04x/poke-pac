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

    public void RemovePokemon(AvatarPokemonSetup p)
    {
        for(int i = 0; i < pokemon_avatar_list.Count; i++)
        {
            if (p.player_name == pokemon_avatar_list[i].player_name && p.start_position == pokemon_avatar_list[i].start_position)
            {
                pokemon_avatar_list.RemoveAt(i);
            }
        }
        foreach(AvatarPokemonSetup pl in pokemon_avatar_list)
        {
 
        }
    }
}
