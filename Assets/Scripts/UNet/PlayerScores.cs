using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class PlayerScores : NetworkBehaviour
{
    [SyncVar (hook = "OnScoreChange")] private int score;
    private Text score_text;
    // Start is called before the first frame update
    void Start()
    {
        score_text = GameObject.Find("Score Text").GetComponent<Text>();
        SetScoreText();
    }

    void SetScoreText()
    {
        if(isLocalPlayer)
        {
            score_text.text = "Score " + score.ToString();
        }
    }

    public void IncreaseScore()
    {
        score++;
    }

    void OnScoreChange(int s)
    {
        score = s;
        SetScoreText();
    }
}
