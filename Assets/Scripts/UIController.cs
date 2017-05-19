using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public Slider player1Health;
    public Slider player2Health;
    public Text winText;
    public Button restartButton;

    private GameObject player1;
    private GameObject player2;
    private bool isPause;

	// Use this for initialization
	void Start () {
        isPause = false;

        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        restartButton.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
        }

        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        player1Health.value = player1.GetComponent<PlayerController>().health / 100;
        player2Health.value = player2.GetComponent<PlayerController>().health / 100;

        if (player1Health.value <= 0)
        {
            isPause = true;
            winText.text = "Player 2 Wins!";
            restartButton.gameObject.SetActive(true);
        }
        else if(player2Health.value <= 0)
        {
            isPause = true;
            winText.text = "Player 1 Wins!";
            restartButton.gameObject.SetActive(true);
        }
    }

    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
