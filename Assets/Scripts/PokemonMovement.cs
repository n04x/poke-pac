using Photon.Pun;
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
    private float speed = 5;
    public List<GameObject> evolve_pokemon;
    public Text scores;
    public string pokemon_name;
    public bool game_over;
    public bool master_ball_eaten;
    public float evolve_duration;
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
        master_ball_eaten = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
        {
            if (master_ball_eaten)
            {
                evolve_duration -= Time.deltaTime;
                if(evolve_duration < 0)
                {
                    //Devolve();
                    PV.RPC("Devolve", RpcTarget.All);
                }
            }
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

    void Evolve()
    {
        Debug.LogWarning("Inside the Evolve loop");
        master_ball_eaten = true;
        speed = 7.0f;
        evolve_pokemon[0].SetActive(false);
        evolve_pokemon[1].SetActive(true);
        evolve_duration = 7.5f;
    }
    
    [PunRPC] void Devolve()
    {
        Debug.LogWarning("Inside the Devolve loop");
        master_ball_eaten = false;
        speed = 5.0f;
        evolve_pokemon[0].SetActive(true);
        evolve_pokemon[1].SetActive(false);
        evolve_duration = 5.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // COLLISION WITH POKEBALL
        if(collision.gameObject.tag == "pokeball")
        {
            //collision.gameObject.SetActive(false);
            collision.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<SphereCollider>().enabled = false;
            avatar_pokemon_setup.scores++;
            GameSetup.GS.pokeballs_count--;
        }
        // COLLISION WITH MASTERBALL
        if(collision.gameObject.tag == "masterball" && !master_ball_eaten)
        {
            collision.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<SphereCollider>().enabled = false;
            collision.gameObject.GetComponent<MasterBallBehaviour>().spawned = false;
            Evolve();
        }

        // COLLISION BETWEEN TWO PLAYER, TWO POSSIBLE SCENARIO
        if(collision.gameObject.tag == "Player" && !master_ball_eaten)
        {
            float force = 300;
            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir * force);
        }
        else if(collision.gameObject.tag == "Player" && master_ball_eaten)
        {
            if(!collision.gameObject.GetComponent<PokemonMovement>().master_ball_eaten)
            {
                Destroy(collision.gameObject);
            }
        }
        
        // COLLISION WITH GENGAR, TWO POSSIBLE SCENARIO
        if (collision.gameObject.tag == "Gengar" && !master_ball_eaten)
        {
            GetComponent<Transform>().position = avatar_pokemon_setup.start_position.position;
            avatar_pokemon_setup.scores -= 10;
        }
        else if(collision.gameObject.tag == "Gengar" && master_ball_eaten)
        {
            collision.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else
        {
            return;
        }
    }
}
