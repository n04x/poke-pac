using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(InputField))]
public class PlayerInputField : MonoBehaviour
{
    const string player_name_pref_key = "PlayerName";

    // Start is called before the first frame update
    void Start()
    {
        string default_username = string.Empty;
        InputField input_field = this.GetComponent<InputField>();

        if(input_field != null)
        {
            if(PlayerPrefs.HasKey(player_name_pref_key))
            {
                default_username = PlayerPrefs.GetString(player_name_pref_key);
                input_field.text = default_username;
            }
        }

        PhotonNetwork.NickName = default_username;
    }

    public void SetPlayerName(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty!");
            return;
        }
        PhotonNetwork.NickName = value;

        PlayerPrefs.SetString(player_name_pref_key, value);

    }
}
