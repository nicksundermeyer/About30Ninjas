using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    // function to load scene by index
    public void LoadByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
