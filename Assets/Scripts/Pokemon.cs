using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Pokemon : MonoBehaviour
{
    float speed = 5.0f;

    public GameObject pokemon_prefabs { get; set; }

    private GameObject pokemon;
    private GameObject pokemon_camera;
    private Timer timer;
    private Vector3 start_pos;
    private PokemonList pokemon_list;
    
    public bool is_dead;
    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer();
        timer.AutoReset = true;
        timer.Stop();
        // timer.Elapsed += ResetSpeed;

        pokemon_list = GameObject.Find("pokemon_list").GetComponent<PokemonList>();
        is_dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(is_dead) {
            is_dead = false;
        }
    }
}
