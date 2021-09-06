using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Xml.Serialization;

public class ArchiveManager : MonoBehaviour
{
    public AudioClip levelClip;
    public GameObject protectPanel;
    public GameObject adaptPanel;
    public GameObject dispersePanel;
    public PlayerSaveData playerSaveData;
    public Button[] buttons;
    public AudioClip buttonClip;
    private void Awake()
    {
        string savePath = Application.persistentDataPath;
        if (File.Exists(savePath + "/player.save"))
        {
            var serializer = new XmlSerializer(typeof(PlayerSaveData));
            var stream = new FileStream(savePath + "/player.save", FileMode.Open);
            playerSaveData = serializer.Deserialize(stream) as PlayerSaveData;
            stream.Close();
        }
    }
    void Start()
    {
        SoundManager.instance.PlayMusic(levelClip);
        buttons[1].interactable = playerSaveData.biomeVisited[0];
        buttons[2].interactable = playerSaveData.dispersedTalk[0];
    }
    private void OnEnable()
    {
        buttons[1].interactable = playerSaveData.biomeVisited[0];
        buttons[2].interactable = playerSaveData.dispersedTalk[0];
    }
    public void EnablePanel(int no)
    {
        if (no == 0)
        {
            protectPanel.SetActive(true);
            adaptPanel.SetActive(false);
            dispersePanel.SetActive(false);
        }
        else if (no == 1)
        {
            protectPanel.SetActive(false);
            adaptPanel.SetActive(true);
            dispersePanel.SetActive(false);
        }
        else if (no == 2)
        {
            protectPanel.SetActive(false);
            adaptPanel.SetActive(false);
            dispersePanel.SetActive(true);
        }
    }
    public void ReturnToMain()
    {
        SoundManager.instance.PlaySfx(buttonClip);
        SceneManager.LoadScene("Main_Menu");
    }
}
