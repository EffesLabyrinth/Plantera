using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

public class LevelSaveState : MonoBehaviour
{
    public GameObject[] entities;
    public LevelState levelState;
    public void SaveLevelState()
    {
        levelState.setEntitiesSize(entities.Length);
        for (int i = 0; i < entities.Length; i++)
        {
            levelState.entitiesState[i] = entities[i].activeSelf;
        }
        string savePath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(LevelState));
        var stream = new FileStream(savePath + "/" + SceneManager.GetActiveScene().name + ".tempsave",FileMode.Create);
        serializer.Serialize(stream, levelState);
        stream.Close();
    }
    public void LoadLevelState()
    {
        string savePath = Application.persistentDataPath;
        if (File.Exists(savePath + "/" + SceneManager.GetActiveScene().name + ".tempsave"))
        {
            var serializer = new XmlSerializer(typeof(LevelState));
            var stream = new FileStream(savePath + "/" + SceneManager.GetActiveScene().name + ".tempsave", FileMode.Open);
            levelState = serializer.Deserialize(stream) as LevelState;
            stream.Close();

            for (int i = 0; i < entities.Length; i++)
            {
                entities[i].SetActive(levelState.entitiesState[i]);
            }
        }
        /*
        else if (File.Exists(savePath + "/" + SceneManager.GetActiveScene().name + ".save"))
        {
            var serializer = new XmlSerializer(typeof(LevelState));
            var stream = new FileStream(savePath + "/" + SceneManager.GetActiveScene().name + ".save", FileMode.Open);
            levelState = serializer.Deserialize(stream) as LevelState;
            stream.Close();

            for (int i = 0; i < entities.Length; i++)
            {
                entities[i].SetActive(levelState.entitiesState[i]);
            }
        }*/
    }
    public void DeleteLevelState()
    {
        /*
        if (System.IO.File.Exists(savePath + "/" + levelState.saveName + ".save"))
        {
            File.Delete(savePath + "/" + levelState.saveName + ".save");
        }*/
        string savePath = Application.persistentDataPath;
        DirectoryInfo dir = new DirectoryInfo(savePath);
        FileInfo[] info = dir.GetFiles("*.tempsave");
        foreach (FileInfo f in info)
        {
            File.Delete(f.FullName);
        }
    }
    private void OnApplicationQuit()
    {
        DeleteLevelState();
    }
}
[System.Serializable]
public class LevelState
{
    public bool[] entitiesState;
    public void setEntitiesSize(int size)
    {
        entitiesState = new bool[size];
    }
}
