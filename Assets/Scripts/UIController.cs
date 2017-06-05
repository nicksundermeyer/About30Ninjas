using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public Slider player1Health;
    public Slider player2Health;
    public Text winText;
    public GameObject WinScreen;
    public ScoreManager scoreManager;

    private GameObject player1;
    private GameObject player2;
    private bool isPause;

	// Use this for initialization
	void Start () {
        isPause = false;

        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
        }

        player1Health.value = player1.GetComponent<PlayerController>().health / 100;
        player2Health.value = player2.GetComponent<PlayerController>().health / 100;

        if (player1Health.value <= 0)
        {
            if(!isPause)
                ScoreManager.player2++;

            isPause = true;
            winText.text = "Point to Player 2";
            WinScreen.SetActive(true);
        }
        else if(player2Health.value <= 0)
        {
            if(!isPause)
                ScoreManager.player1++;

            isPause = true;
            winText.text = "Point to Player 1";
            WinScreen.SetActive(true);
        }
    }

    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // function to load scene by index
    public void LoadByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    // function to quit game
    public void Quit()
    {
        Application.Quit();
    }

    public void changeVolume()
    {

    }

    public void checkController()
    {
        
    }
}
