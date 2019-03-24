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
    #region Script Variables
    private float minutes;
    private float seconds;
    private PhotonView PV;

    const int POKEBALLS = 312;

    public static GameSetup GS;
    public Transform[] spawn_positions;
    public Text scores;
    public Text timer;
    public Text game_over_text;
    public float time_left = 240.0f;    // 4 minutes.
    public bool pokepuff_eaten = false;
    public int pokeballs_count;

    #endregion

    #region MonoBehaviour functions
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        pokeballs_count = POKEBALLS;
        // start the game be disable pokepuffs.
    }

    private void Update() {
        GetTimer();
        timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        if(PhotonNetwork.IsMasterClient && time_left < 0)
        {
            PV.RPC("GameOver", RpcTarget.All);
        }
        if(PhotonNetwork.IsMasterClient && pokepuff_eaten) {
            PV.RPC("MazeReset", RpcTarget.All);
            pokepuff_eaten = false;
        }
        if(PhotonNetwork.IsMasterClient && pokeballs_count <= 0)
        {
            PV.RPC("MazeReset", RpcTarget.All);
        }
    }

    void GetTimer()
    {
        time_left -= Time.deltaTime;
        minutes = Mathf.Floor(time_left / 60);
        seconds = time_left % 60;
        if (seconds > 59)
        {
            seconds = 59;
        }
        if (minutes < 0)
        {
            minutes = 0.0f;
        }

    }
    #endregion

    #region PunRPC Functions
    [PunRPC] public void MazeReset()
    {
        GameObject[] pokeballs = GameObject.FindGameObjectsWithTag("pokeball");
        foreach(GameObject pokeball in pokeballs)
        {
            pokeball.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            pokeball.GetComponent<SphereCollider>().enabled = true;
        }
        pokeballs_count = POKEBALLS;
    }

    [PunRPC] void GameOver()
    {
        time_left = 0.0f;
        PokemonListBehaviour pokemons_list = GameObject.Find("Pokemon List").GetComponent<PokemonListBehaviour>();
        ICollection<AvatarPokemonSetup> pokemons = pokemons_list.GetPokemon();
        int winner_score = 0;
        AvatarPokemonSetup winner_pokemon = null;
        foreach (AvatarPokemonSetup p in pokemons)
        {
            p.GetComponentInChildren<PokemonMovement>().game_over = true;
            if (p.scores > winner_score)
            {
                winner_score = p.scores;
                winner_pokemon = p;
            }
        }
        game_over_text.text = "Player " + winner_pokemon.player_name + " win!";
    }
    #endregion

    #region MonoBehaviour PunCallbacks
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

    #endregion

   
}
