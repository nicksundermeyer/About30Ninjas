using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class GameModel : MonoBehaviour 
{
    public static GameModel control;

    public bool twoControllers;
    public float volume;

    void Awake()
    {
        volume = 0.5f;

        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        Debug.Log("Saved");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenWrite(Application.persistentDataPath + "/" + "save" + ".dat");
        Debug.Log(twoControllers);
        GameData data = new GameData();
        data.twoControllers = twoControllers;
        data.volume = volume;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        Debug.Log("Loaded");
        if (File.Exists(Application.persistentDataPath + "/" + "save" + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/" + "save" + ".dat");
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            twoControllers = data.twoControllers;
            volume = data.volume;
        }
    }

    public void setTwoControllers()
    {
        twoControllers = !twoControllers;
        Debug.Log(twoControllers);
    }
}

[Serializable]
class GameData
{
    public bool twoControllers;
    public float volume;
}
