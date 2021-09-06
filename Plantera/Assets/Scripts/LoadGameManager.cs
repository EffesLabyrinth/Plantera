using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Serialization;

public class LoadGameManager : MonoBehaviour
{
    public Text nameText;
    public GameObject confirmPanel;
    PlayerSaveData playerSaveData;
    public AudioClip levelClip;
    public AudioClip buttonClip;
    void Start()
    {
        PlayerManager.instance.playerController.isMovable = false;
        string savePath = Application.persistentDataPath;
        if (System.IO.File.Exists(savePath + "/player.save"))
        {
            var serializer = new XmlSerializer(typeof(PlayerSaveData));
            var stream = new FileStream(savePath + "/player.save", FileMode.Open);
            playerSaveData = serializer.Deserialize(stream) as PlayerSaveData;
            stream.Close();
            nameText.text = playerSaveData.playerName;
        }
        SoundManager.instance.PlayMusic(levelClip);
    }
    public void BackToMainMenu()
    {
        SoundManager.instance.PlaySfx(buttonClip);
        SceneManager.LoadScene("Main_Menu");
    }
    public void LoadConfirmationPanel(bool value)
    {
        SoundManager.instance.PlaySfx(buttonClip);
        confirmPanel.SetActive(value);
    }
    public void LoadGame()
    {
        SoundManager.instance.PlaySfx(buttonClip);
        PlayerManager.instance.playerSaveData = playerSaveData;
        PlayerManager.instance.playerSaveData.isFromLoad = true;
        SceneManager.LoadScene(playerSaveData.currentScene);
    }
}
