    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonInfo : MonoBehaviour
{
    public static PokemonInfo PI;

    public int selected_pokemon;

    public GameObject[] all_pokemons;

    private void OnEnable()
    {
        if (PokemonInfo.PI == null)
        {
            PokemonInfo.PI = this;
        }
        else
        {
            if(PokemonInfo.PI != this)
            {
                Destroy(PokemonInfo.PI.gameObject);
                PokemonInfo.PI = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("MyPokemon"))
        {
            selected_pokemon = PlayerPrefs.GetInt("MyPokemon");
        }
        else
        {
            selected_pokemon = 0;
            PlayerPrefs.SetInt("MyPokemon", selected_pokemon);
        }
    }
}
