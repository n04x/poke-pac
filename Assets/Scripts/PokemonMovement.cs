﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonMovement : MonoBehaviour
{
    private PhotonView PV;
    //private CharacterController CC;
    private AvatarPokemonSetup avatar_pokemon_setup;
    private PokemonListBehaviour pokemon_list;
    public float speed;
    public Text scores;
    public string pokemon_name;
    public bool game_over;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        pokemon_name = PV.Owner.NickName;
        avatar_pokemon_setup = GetComponent<AvatarPokemonSetup>();
        pokemon_list = GameObject.Find("Pokemon List").GetComponent<PokemonListBehaviour>();
        scores = GameSetup.GS.scores;
        avatar_pokemon_setup.player_name = pokemon_name;
        pokemon_list.AddPokemon(avatar_pokemon_setup);
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
        {
            if(!game_over)
            {
                InputMovement();
            }
            avatar_pokemon_setup.player_name = pokemon_name;
            scores.text = "Score: " + avatar_pokemon_setup.scores.ToString();
        }
    }

    void InputMovement()
    {
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            this.GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.forward * speed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
            this.GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
            this.GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
            this.GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.right * speed * Time.deltaTime);
        }
        if(transform.position.z > 9.0f) 
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
        }
        if(transform.position.z < -10.5f) 
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, 8.5f);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "pokeball")
        {
            //collision.gameObject.SetActive(false);
            collision.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<SphereCollider>().enabled = false;
            avatar_pokemon_setup.scores++;
            GameSetup.GS.pokeballs_count--;
        }
        if(collision.gameObject.tag == "masterball")
        {
            collision.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<SphereCollider>().enabled = false;
            
        }
        if(collision.gameObject.tag == "Player")
        {
            Debug.LogWarning("CONTACT WITH THE PLAYER");
            float force = 300;
            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir * force);
        }
    }
}