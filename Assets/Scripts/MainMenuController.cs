using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {


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
}
