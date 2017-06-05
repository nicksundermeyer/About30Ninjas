using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public Slider volumeSlider;
    public Toggle controllerToggle;

    private GameModel saveManager;
    private bool twoControllers;

    void Start()
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<GameModel>();
        volumeSlider.value = saveManager.volume;
    }

    void Update()
    {
        saveManager.volume = volumeSlider.value;
        AudioListener.volume = saveManager.volume;
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

    public void Load()
    {
        volumeSlider.value = saveManager.volume;

        if (controllerToggle.isOn != saveManager.twoControllers)
        {
            controllerToggle.isOn = !controllerToggle.isOn;
        }
    }
}
