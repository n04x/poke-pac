using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PikachuUI : MonoBehaviour
{
    [Tooltip("UI Text to display player's name")]
    [SerializeField] private Text player_name_text;

    [Tooltip("UI Text to display player's score")]
    [SerializeField] private Text player_score_text;

    [Tooltip("UI Text to display timer")]
    [SerializeField] private Text timer_text;

    private PikachuManager target_player;

    void Awake() {
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }
    void Update() {
        if(target_player == null) {
            Destroy(this.gameObject);
            return;
        }

        player_score_text.text = "Score: " + target_player.scores;

    }
    public void SetTarget(PikachuManager t) {
        if(t == null) {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        target_player = t;
        if(player_name_text != null) {
            player_name_text.text = target_player.photonView.Owner.NickName;
        }
    }
}
