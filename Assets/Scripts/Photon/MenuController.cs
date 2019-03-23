using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void OnClickPokemonPick(int which_pokemon)
    {
        if(PokemonInfo.PI != null)
        {
            PokemonInfo.PI.selected_pokemon = which_pokemon;
            PlayerPrefs.SetInt("MyPokemon", which_pokemon);
        }
    }
}
