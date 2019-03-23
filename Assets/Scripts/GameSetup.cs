using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.IO;
using System;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;

    private PhotonView PV;
    public Transform[] spawn_positions;
    public Text scores;
    public Text timer;
    public Text game_over_text;
    public float time_left = 240.0f;    // 4 minutes.

    private float minutes;
    private float seconds;
    public bool pokepuff_eaten = false;

    public int pokeballs_count;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        pokeballs_count = 312;
        // start the game be disable pokepuffs.
    }

    private void Update() {
        GetTimer();
        timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        if(PhotonNetwork.IsMasterClient && time_left < 0)
        {
            PV.RPC("GameOver", RpcTarget.All);
            //GameOver();
        }
        if(PhotonNetwork.IsMasterClient && pokepuff_eaten) {
            PV.RPC("MazeReset", RpcTarget.All);
            pokepuff_eaten = false;
        }
        if(PhotonNetwork.IsMasterClient && pokeballs_count <= 0)
        {
            //MazeReset();
            PV.RPC("MazeReset", RpcTarget.All);
        }
    }

    [PunRPC] public void MazeReset()
    {
        GameObject[] pokeballs = GameObject.FindGameObjectsWithTag("pokeball");
        foreach(GameObject pokeball in pokeballs)
        {
            pokeball.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            pokeball.GetComponent<SphereCollider>().enabled = true;
        }
        pokeballs_count = 15;

        GameObject[] masterballs = GameObject.FindGameObjectsWithTag("masterball");
        foreach(GameObject masterball in masterballs)
        {
            masterball.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            masterball.GetComponent<SphereCollider>().enabled = true;
        }
    }

    private void OnEnable()
    {
        if(GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }

    public void DisconnectPokemon() 
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while(PhotonNetwork.IsConnected) 
        {
            yield return null;
        }
        SceneManager.LoadScene(MultiplayerSetting.mp_setting.menu_scene);
    }

    [PunRPC] void GameOver()
    {
        time_left = 0.0f;
        PokemonListBehaviour pokemons_list = GameObject.Find("Pokemon List").GetComponent<PokemonListBehaviour>();
        ICollection<AvatarPokemonSetup> pokemons = pokemons_list.GetPokemon();
        int winner_score = 0;
        string winner_name = null;
        AvatarPokemonSetup winner_pokemon = null;
        foreach(AvatarPokemonSetup p in pokemons)
        {
            p.GetComponentInChildren<PokemonMovement>().game_over = true;
            if(p.scores > winner_score)
            {
                winner_score = p.scores;
                winner_pokemon = p;
            }
        }
        game_over_text.text = "Player " + winner_pokemon.player_name + " win!";
    }
    void GetTimer() {
        time_left -= Time.deltaTime;
        minutes = Mathf.Floor(time_left / 60);
        seconds = time_left % 60;
        if(seconds > 59) {
            seconds = 59;
        }
        if(minutes < 0) {
            minutes = 0.0f;
        }

    }
}
