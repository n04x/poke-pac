using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    //private CharacterController CC;
    private AvatarPokemonSetup avatar_pokemon_setup;
    public float speed;
    public Text scores;
    // Start is called before the first frame update
    void Start()
    {
        avatar_pokemon_setup = GetComponent<AvatarPokemonSetup>();
        //scores = GameSetup.GS.scores;
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
            InputMovement();
            //scores.text = "Score: " + avatar_pokemon_setup.scores.ToString();
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
            collision.gameObject.SetActive(false);
            avatar_pokemon_setup.scores++;
        } else
        {
            return;
        }
    }
}
