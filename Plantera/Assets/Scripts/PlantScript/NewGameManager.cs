using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Serialization;

public class NewGameManager : MonoBehaviour
{
    public InputField inputField;
    public Button startButton;
    public GameObject confirmPanel;
    public AudioClip levelClip;
    public AudioClip buttonClip;
    private void Start()
    {
        inputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        PlayerManager.instance.playerController.isMovable = false;
        SoundManager.instance.PlayMusic(levelClip);
    }
    public void ValueChangeCheck()
    {
        if (inputField.text != "") startButton.interactable = true;
        else startButton.interactable = false;
    }
    public void BackToMainMenu()
    {
        SoundManager.instance.PlaySfx(buttonClip);
        SceneManager.LoadScene("Main_Menu");
    }
    public void CharacterCreationConfirmationPanel(bool value)
    {
        SoundManager.instance.PlaySfx(buttonClip);
        confirmPanel.SetActive(value);
    }
    public void StartGame()
    {
        SoundManager.instance.PlaySfx(buttonClip);
        PlayerManager.instance.playerSaveData.playerName = inputField.text;
        PlayerManager.instance.playerSaveData.currentScene = "Tutorial_1_Forest_1";
        PlayerManager.instance.playerSaveData.isFromLoad = true;
        bool[] bossKilled = { false, false, false, false, false };
        PlayerManager.instance.playerSaveData.bossKilled = bossKilled;
        bool[] unlockedPlant = { true, false, false, false, false, false,false};
        PlayerManager.instance.playerSaveData.unlockedPlant = unlockedPlant;
        bool[] gatheredPlant = { false, false, false, false, false, false, false };
        PlayerManager.instance.playerSaveData.gatheredPlant = gatheredPlant;
        bool[] biomeVisited = { false, false, false };
        PlayerManager.instance.playerSaveData.biomeVisited = biomeVisited;
        bool[] dispersedTalk = { false, false, false, false };
        PlayerManager.instance.playerSaveData.dispersedTalk = dispersedTalk;

        string savePath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(PlayerSaveData));
        var stream = new FileStream(savePath + "/player.save", FileMode.Create);
        serializer.Serialize(stream, PlayerManager.instance.playerSaveData);
        stream.Close();

        savePath = Application.persistentDataPath;
        DirectoryInfo dir = new DirectoryInfo(savePath);
        FileInfo[] info = dir.GetFiles("*.tempsave");
        foreach (FileInfo f in info)
        {
            File.Delete(f.FullName);
        }

        SceneManager.LoadScene("Tutorial_1_Forest_1");
    }
}
