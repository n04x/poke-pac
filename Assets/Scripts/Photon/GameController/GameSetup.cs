using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;

    public Transform[] spawn_positions;
    public Text scores;
    public Text timer;
    public float time_left = 240.0f;    // 4 minutes.
    private float minutes;
    private float seconds;

    private void Update() {
        GetTimer();
        timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
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
