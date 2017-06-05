using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckWinner : MonoBehaviour {
    
    public Text winText;

    void Awake()
    {
        Debug.Log("test");
        Debug.Log(ScoreManager.player1 + " " + ScoreManager.player2);
        if (ScoreManager.player1 > ScoreManager.player2)
        {
            winText.text = "Player 1 Wins!";
        }
        else
        {
            winText.text = "Player 2 Wins!";
        }
    }
}
